using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using rapid.erp.Core.Extensions;
using rapid.erp.Core.FlashMessage;
using rapid.erp.Core.Helpers;
using rapid.erp.Core.Security.Manager;
using rapid.erp.Core.Security;
using rapid.erp.Core.Utility;
using rapid.erp.ViewModel.Database;
using rapid.erp.ViewModel.Security;
using System.Web;

namespace rapid.erp.WebMvc.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        #region Global Variable Declaration
        private SignInManager<ApplicationUser> _signInManager;
        private RoleManager<ApplicationRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _iConfiguration;
        private readonly ILogger<AccountController> _iLogger;
        private readonly IFlashMessage _iFlashMessage;
        #endregion

        #region Constructor
        public AccountController(ILogger<AccountController> iLogger, IFlashMessage iFlashMessage, 
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IConfiguration iConfiguration)
        {
            _iLogger = iLogger;
            _iFlashMessage = iFlashMessage;

            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _iConfiguration = iConfiguration;
            
        }
        #endregion


        #region Actions

        //
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            try
            {
                LoginViewModel model = new LoginViewModel();
                model.ReturnUrl = returnUrl;
                return View(model);
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestStart("Login[POST]", $"UserEmail: {model.Email}"));
                if (ModelState.IsValid)
                {
                    ApplicationUser user = await _userManager.FindByNameAsync(model.Email);
                    if (user != null)
                    {
                        await _signInManager.SignOutAsync();
                        // This doesn't count login failures towards account lockout
                        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            //var data = await GetUserSignInOutHistoryLocal(model);
                            //await _userLogInOutHistoryManager.CreateLogInOutHistoryAsync(data);
                            _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestSuccess("Login[POST]", $"User logged in, UserEmail: {model.Email}"));
                            var isAdmin = await _userManager.IsInRoleAsync(user, AppDbEnums.ApplicationRoleEnum.Admin.ToDescriptionAttr());
                            if (isAdmin)
                            {
                                if (string.IsNullOrEmpty(model.ReturnUrl))
                                {
                                    return RedirectToAction("Index", "Admin");
                                }
                                return Redirect(model.ReturnUrl);
                            }
                            else
                            {
                                return RedirectToLocal(model.ReturnUrl);
                            }

                        }

                        if (result.IsLockedOut)
                        {
                            _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestSuccess("Login[POST]", $"User account locked out, UserEmail: {model.Email}"));
                            return View("Lockout");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                            _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestSuccess("Login[POST]", $"Invalid login attempt, UserEmail: {model.Email}"));
                            return View(model);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, MessageHelper.Error);
                _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "Login[POST]"));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            try
            {
                var allowedRegister = Convert.ToBoolean(_iConfiguration["AppConfig:AllowedRegister"]);
                if (allowedRegister)
                {
                    RegisterViewModel model = new RegisterViewModel();
                    model.ReturnUrl = returnUrl;
                    model.RoleName = AppDbEnums.ApplicationRoleEnum.Admin.ToDescriptionAttr().ToString();
                    return View(model);
                }
                else
                {
                    return ErrorView(new Exception(MessageHelper.UnhandledError));
                }
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestStart("Register[POST]"));
                var allowedRegister = Convert.ToBoolean(_iConfiguration["AppConfig:AllowedRegister"]);
                if (allowedRegister)
                {
                    if (ModelState.IsValid)
                    {
                        string userName = ((model.Email).Split('@')[0]).Trim(); // you are get here username.

                        var user = new ApplicationUser
                        {
                            UserName = model.Email,
                            Email = model.Email
                        };

                        var result = await IsEmailExists(user);
                        if (!result.Success)
                        {
                            ModelState.AddModelError(string.Empty, result.Message);
                            _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestSuccess("Register[POST]", result.Message));
                            return View(model);
                        }

                        var role = new ApplicationRole
                        {
                            Id = model.RoleName,
                            Name = model.RoleName,
                            IsActive = true
                        };
                        //IdentityResult resultRole = await _roleManager.CreateAsync(role);
                        var resultRoleName = await _roleManager.GetRoleNameAsync(role);

                        //if (resultRole.Succeeded)
                        if (!string.IsNullOrEmpty(resultRoleName))
                        {
                            var identityResult = await _userManager.CreateAsync(user, model.Password);
                            if (identityResult.Succeeded)
                            {
                                //await _userManager.AddToRoleAsync(user, model.RoleName);
                                await _userManager.AddToRoleAsync(user, resultRoleName);

                                await _signInManager.SignInAsync(user, isPersistent: false);
                                _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestSuccess("Register[POST]", $"User created a new account with password, UserEmail:{model.Email}"));

                                var isAdmin = await _userManager.IsInRoleAsync(user, AppDbEnums.ApplicationRoleEnum.Admin.ToDescriptionAttr());
                                if (isAdmin)
                                {
                                    if (string.IsNullOrEmpty(model.ReturnUrl))
                                    {
                                        return RedirectToAction("Index", "Admin");
                                    }
                                    return Redirect(model.ReturnUrl);
                                }
                                else
                                {
                                    return RedirectToLocal(model.ReturnUrl);
                                }

                            }

                            _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestSuccess("Register[POST]", $"User creation failed, UserEmail:{model.Email}"));
                            AddErrors(identityResult);
                        }

                    }
                }
                else
                {
                    return ErrorView(new Exception(MessageHelper.UnhandledError));
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, MessageHelper.Error);
                _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "Register[POST]"));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //
        // GET: /Account/ForgotPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            try
            {
                var allowedRegister = Convert.ToBoolean(_iConfiguration["AppConfig:AllowedRegister"]);
                if (allowedRegister)
                {
                    return View();
                }
                else
                {
                    return ErrorView(new Exception(MessageHelper.UnhandledError));
                }
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            try
            {
                _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestStart("ForgotPassword[POST]", $"UserEmail: {model.Email}"));
                var allowedRegister = Convert.ToBoolean(_iConfiguration["AppConfig:AllowedRegister"]);
                if (allowedRegister)
                {
                    if (ModelState.IsValid)
                    {
                        bool isValid = true;
                        var user = await _userManager.FindByNameAsync(model.Email);
                        if (user == null)
                        {
                            isValid = false;
                            // Don't reveal that the user does not exist
                            ModelState.AddModelError(string.Empty, "Invalid email.");
                            _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestSuccess("ForgotPassword[POST]", $"Invalid email, UserEmail: {model.Email}"));
                            return View(model);
                        }

                        //var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
                        //if (!isEmailConfirmed)
                        //{
                        //    isValid = false;
                        //    // Don't reveal that the user email is not confirmed
                        //    ModelState.AddModelError(string.Empty, "Email is not confirmed.");
                        //    _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestSuccess("ForgotPassword[POST]", $"Email is not confirmed, UserEmail: {model.Email}"));
                        //    return View(model);
                        //}

                        if (isValid)
                        {

                            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                            // Send an email with this link
                            var message = await GenareteForgotPasswordEmailTemplateAsync(user);
                            //await _emailSender.SendEmailBySendGridAsync(user.Id, model.Email, "Reset Password", message);
                            return View("ForgotPasswordConfirmation");

                        }

                    }
                }
                else
                {
                    return ErrorView(new Exception(MessageHelper.UnhandledError));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, MessageHelper.Error);
                _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "ForgotPassword[POST]"));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        //
        // GET: /Account/ResetPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string userId, string email, string code = null)
        {
            try
            {
                var allowedRegister = Convert.ToBoolean(_iConfiguration["AppConfig:AllowedRegister"]);
                if (allowedRegister)
                {
                    if (userId == null || email == null || code == null)
                    {
                        return View("Error");
                    }
                    else
                    {
                        ResetPasswordViewModel model = new ResetPasswordViewModel() { Code = HttpUtility.HtmlDecode(code).Trim(), Email = HttpUtility.HtmlDecode(email).Trim() };
                        return View(model);
                    }
                }
                else
                {
                    return ErrorView(new Exception(MessageHelper.UnhandledError));
                }
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            try
            {
                _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestStart("ResetPassword[POST]", $"UserEmail: {model.Email}"));
                var allowedRegister = Convert.ToBoolean(_iConfiguration["AppConfig:AllowedRegister"]);
                if (allowedRegister)
                {
                    if (ModelState.IsValid)
                    {
                        bool isValid = true;
                        var user = await _userManager.FindByNameAsync(model.Email);
                        if (user == null)
                        {
                            isValid = false;
                            // Don't reveal that the user does not exist
                            ModelState.AddModelError(string.Empty, "Invalid email.");
                            _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestSuccess("ResetPassword[POST]", $"Invalid email, UserEmail: {model.Email}"));
                            return View(model);
                        }

                        //var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
                        //if (!isEmailConfirmed)
                        //{
                        //    isValid = false;
                        //    // Don't reveal that the user email is not confirmed
                        //    ModelState.AddModelError(string.Empty, "Email is not confirmed.");
                        //    _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestSuccess("ResetPassword[POST]", $"Email is not confirmed, UserEmail: {model.Email}"));
                        //    return View(model);
                        //}

                        if (isValid)
                        {
                            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
                            if (result.Succeeded)
                            {
                                return View("ResetPasswordConfirmation");
                            }
                        }

                    }
                }
                else
                {
                    return ErrorView(new Exception(MessageHelper.UnhandledError));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, MessageHelper.Error);
                _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "ResetPassword[POST]"));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestStart("LogOff", $"User:{User.Identity.Name}"));
                await _signInManager.SignOutAsync();
                _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestSuccess("LogOff", $"User logged out"));
            }
            catch (Exception ex)
            {
                _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "LogOff"));
            }
            return RedirectToAction("Login", "Account");
        }

        private async Task<AppResult> IsEmailExists(ApplicationUser user)
        {
            try
            {
                var isExists = await _userManager.FindByEmailAsync(user.Email);

                if (isExists != null)
                {
                    string isEmailExistsMessage = string.Format(MessageHelper.IsEmailExists, user.Email);
                    return AppResult.Fail(isEmailExistsMessage);
                }
                else
                {
                    return AppResult.Ok();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, MessageHelper.Error);
                _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "IsEmailExists"));
                return AppResult.Fail(MessageHelper.Error);
            }
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        private async Task<string> GenareteForgotPasswordEmailTemplateAsync(ApplicationUser user)
        {
            string htmlTemplate = string.Empty;

            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            string link = Url.Action("ResetPassword", "Account", new { userId = user.Id, email = user.Email, code = passwordResetToken }, protocol: HttpContext.Request.Scheme);

            string title = "Please reset your password by clicking here:";
            string linkText = "Forgot Password";

            //htmlTemplate = "Please reset your password by clicking here: <a target='_blank' href=\"" + link + "\">link</a>";
            htmlTemplate = EmailTemplateHelper.GetEmailTemplate(title, link, linkText);

            return htmlTemplate;
        }

        #endregion

        #endregion
    }
}