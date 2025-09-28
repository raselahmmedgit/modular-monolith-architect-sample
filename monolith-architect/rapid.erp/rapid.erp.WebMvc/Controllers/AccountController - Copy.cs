using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    //[Authorize]
    //public class AccountController : BaseController
    //{
    //    #region Global Variable Declaration
    //    private SignInManager<ApplicationUser> _signInManager;
    //    private readonly IApplicationRoleManager _iApplicationRoleManager;
    //    private readonly IApplicationUserManager _iApplicationUserManager;
    //    private readonly IConfiguration _iConfiguration;
    //    private readonly ILogger<AccountController> _iLogger;
    //    private readonly IFlashMessage _iFlashMessage;
    //    #endregion

    //    #region Constructor
    //    public AccountController(ILogger<AccountController> iLogger, IFlashMessage iFlashMessage, 
    //        SignInManager<ApplicationUser> signInManager,
    //        IApplicationRoleManager iApplicationRoleManager,
    //        IApplicationUserManager iApplicationUserManager,
    //        IConfiguration iConfiguration)
    //    {
    //        _iLogger = iLogger;
    //        _iFlashMessage = iFlashMessage;

    //        _signInManager = signInManager;
    //        _iApplicationRoleManager = iApplicationRoleManager;
    //        _iApplicationUserManager = iApplicationUserManager;
    //        _iConfiguration = iConfiguration;
            
    //    }
    //    #endregion

    //    #region Actions

    //    //
    //    // GET: /Account/Login
    //    [HttpGet]
    //    [AllowAnonymous]
    //    public IActionResult Login(string returnUrl = null)
    //    {
    //        try
    //        {
    //            LoginViewModel model = new LoginViewModel();
    //            model.ReturnUrl = returnUrl;
    //            return View(model);
    //        }
    //        catch (Exception ex)
    //        {
    //            return ErrorView(ex);
    //        }
    //    }

    //    //
    //    // POST: /Account/Login
    //    [HttpPost]
    //    [AllowAnonymous]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Login(LoginViewModel model)
    //    {
    //        try
    //        {
    //            _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestStart("Login[POST]", $"UserEmail: {model.Email}"));
    //            if (ModelState.IsValid)
    //            {
    //                var applicationUserViewModel = await _iApplicationUserManager.GetApplicationUserByUserNameOrEmailAsync(model.Email);
    //                if (applicationUserViewModel != null)
    //                {
    //                    await _signInManager.SignOutAsync();
    //                    // This doesn't count login failures towards account lockout
    //                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
    //                    var signInResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
    //                    if (signInResult.Succeeded)
    //                    {
    //                        //var data = await GetUserSignInOutHistoryLocal(model);
    //                        //await _userLogInOutHistoryManager.CreateLogInOutHistoryAsync(data);
    //                        _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestSuccess("Login[POST]", $"User logged in, UserEmail: {model.Email}"));
    //                        var isInRoleAsAdminResult = await _iApplicationUserManager.IsInRoleAsync(applicationUserViewModel, AppDbEnums.ApplicationRoleEnum.Admin.ToDescriptionAttr());
    //                        if (isInRoleAsAdminResult.Success)
    //                        {
    //                            if (string.IsNullOrEmpty(model.ReturnUrl))
    //                            {
    //                                return RedirectToAction("Index", "Admin");
    //                            }
    //                            return Redirect(model.ReturnUrl);
    //                        }
    //                        else
    //                        {
    //                            return RedirectToLocal(model.ReturnUrl);
    //                        }

    //                    }

    //                    if (signInResult.IsLockedOut)
    //                    {
    //                        _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestSuccess("Login[POST]", $"User account locked out, UserEmail: {model.Email}"));
    //                        return View("Lockout");
    //                    }
    //                    else
    //                    {
    //                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
    //                        _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestSuccess("Login[POST]", $"Invalid login attempt, UserEmail: {model.Email}"));
    //                        return View(model);
    //                    }
    //                }
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            ModelState.AddModelError(string.Empty, MessageHelper.Error);
    //            _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "Login[POST]"));
    //        }

    //        // If we got this far, something failed, redisplay form
    //        return View(model);
    //    }

    //    //
    //    // GET: /Account/Register
    //    [HttpGet]
    //    [AllowAnonymous]
    //    public IActionResult Register(string returnUrl = null)
    //    {
    //        try
    //        {
    //            var allowedRegister = Convert.ToBoolean(_iConfiguration["AppConfig:AllowedRegister"]);
    //            if (allowedRegister) {
    //                RegisterViewModel model = new RegisterViewModel();
    //                model.ReturnUrl = returnUrl;
    //                model.RoleName = AppDbEnums.ApplicationRoleEnum.Admin.ToDescriptionAttr();
    //                return View(model);
    //            }
    //            else {
    //                return ErrorView(new Exception(MessageHelper.UnhandledError));
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            return ErrorView(ex);
    //        }
    //    }

    //    //
    //    // POST: /Account/Register
    //    [HttpPost]
    //    [AllowAnonymous]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Register(RegisterViewModel model)
    //    {
    //        try
    //        {
    //            _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestStart("Register[POST]"));
    //            var allowedRegister = Convert.ToBoolean(_iConfiguration["AppConfig:AllowedRegister"]);
    //            if (allowedRegister)
    //            {
    //                if (ModelState.IsValid)
    //                {
    //                    string userName = ((model.Email).Split('@')[0]).Trim(); // you are get here username.

    //                    var result = await IsEmailExists(model.Email);
    //                    if (!result.Success)
    //                    {
    //                        ModelState.AddModelError(string.Empty, result.Error);
    //                        _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestSuccess("Register[POST]", result.Error));
    //                        return View(model);
    //                    }
                        
    //                    var applicationRoleViewModel = await _iApplicationRoleManager.GetApplicationRoleAsync(model.RoleName);

    //                    if (applicationRoleViewModel is not null)
    //                    {
    //                        var createResult = await _iApplicationUserManager.CreateApplicationUserWithRoleAsync(model);
    //                        if (createResult.Success)
    //                        {
    //                            var user = new ApplicationUser
    //                            {
    //                                UserName = model.Email,
    //                                Email = model.Email
    //                            };
    //                            await _signInManager.SignInAsync(user, isPersistent: false);
    //                            _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestSuccess("Register[POST]", $"User created a new account with password, UserEmail:{model.Email}"));

    //                            var applicationUserViewModel = await _iApplicationUserManager.GetApplicationUserByUserNameOrEmailAsync(model.Email);

    //                            var isInRoleAsAdminResult = await _iApplicationUserManager.IsInRoleAsync(applicationUserViewModel, AppDbEnums.ApplicationRoleEnum.Admin.ToDescriptionAttr());
    //                            if (isInRoleAsAdminResult.Success)
    //                            {
    //                                if (string.IsNullOrEmpty(model.ReturnUrl))
    //                                {
    //                                    return RedirectToAction("Index", "Admin");
    //                                }
    //                                return Redirect(model.ReturnUrl);
    //                            }
    //                            else
    //                            {
    //                                return RedirectToLocal(model.ReturnUrl);
    //                            }

    //                        }

    //                        _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestSuccess("Register[POST]", $"User creation failed, User Email:{model.Email}"));
    //                        AddErrors(result);
    //                    }

    //                }
    //            }
    //            else
    //            {
    //                return ErrorView(new Exception(MessageHelper.UnhandledError));
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            ModelState.AddModelError(string.Empty, MessageHelper.Error);
    //            _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "Register[POST]"));
    //        }

    //        // If we got this far, something failed, redisplay form
    //        return View(model);
    //    }


    //    //
    //    // GET: /Account/ForgotPassword
    //    [HttpGet]
    //    [AllowAnonymous]
    //    public IActionResult ForgotPassword()
    //    {
    //        try
    //        {
    //            var allowedRegister = Convert.ToBoolean(_iConfiguration["AppConfig:AllowedRegister"]);
    //            if (allowedRegister)
    //            {
    //                return View();
    //            }
    //            else
    //            {
    //                return ErrorView(new Exception(MessageHelper.UnhandledError));
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            return ErrorView(ex);
    //        }
    //    }

    //    //
    //    // POST: /Account/ForgotPassword
    //    [HttpPost]
    //    [AllowAnonymous]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    //    {
    //        try
    //        {
    //            _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestStart("ForgotPassword[POST]", $"UserEmail: {model.Email}"));
    //            var allowedRegister = Convert.ToBoolean(_iConfiguration["AppConfig:AllowedRegister"]);
    //            if (allowedRegister)
    //            {
    //                if (ModelState.IsValid)
    //                {
    //                    bool isValid = true;
                        
    //                    var applicationUserViewModel = await _iApplicationUserManager.GetApplicationUserByUserNameOrEmailAsync(model.Email);
    //                    if (applicationUserViewModel is null)
    //                    {
    //                        isValid = false;
    //                        // Don't reveal that the user does not exist
    //                        ModelState.AddModelError(string.Empty, "Invalid email.");
    //                        _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestSuccess("ForgotPassword[POST]", $"Invalid email, UserEmail: {model.Email}"));
    //                        return View(model);
    //                    }

    //                    //if (applicationUserViewModel is not null)
    //                    //{
    //                    //    var isEmailConfirmedResult = await _iApplicationUserManager.IsEmailConfirmedAsync(applicationUserViewModel);
    //                    //    if (isEmailConfirmedResult.Success == false)
    //                    //    {
    //                    //        isValid = false;
    //                    //        // Don't reveal that the user email is not confirmed
    //                    //        ModelState.AddModelError(string.Empty, "Email is not confirmed.");
    //                    //        _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestSuccess("ForgotPassword[POST]", $"Email is not confirmed, UserEmail: {model.Email}"));
    //                    //        return View(model);
    //                    //    }
    //                    //}

    //                    if (isValid)
    //                    {

    //                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
    //                        // Send an email with this link
    //                        var message = await GenareteForgotPasswordEmailTemplateAsync(applicationUserViewModel);
    //                        //await _emailSender.SendEmailBySendGridAsync(user.Id, model.Email, "Reset Password", message);
    //                        return View("ForgotPasswordConfirmation");

    //                    }

    //                }
    //            }
    //            else
    //            {
    //                return ErrorView(new Exception(MessageHelper.UnhandledError));
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            ModelState.AddModelError(string.Empty, MessageHelper.Error);
    //            _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "ForgotPassword[POST]"));
    //        }

    //        // If we got this far, something failed, redisplay form
    //        return View(model);
    //    }

    //    //
    //    // GET: /Account/ForgotPasswordConfirmation
    //    [HttpGet]
    //    [AllowAnonymous]
    //    public IActionResult ForgotPasswordConfirmation()
    //    {
    //        try
    //        {
    //            return View();
    //        }
    //        catch (Exception ex)
    //        {
    //            return ErrorView(ex);
    //        }
    //    }

    //    //
    //    // GET: /Account/ResetPassword
    //    [HttpGet]
    //    [AllowAnonymous]
    //    public IActionResult ResetPassword(string userId, string email, string code = null)
    //    {
    //        try
    //        {
    //            var allowedRegister = Convert.ToBoolean(_iConfiguration["AppConfig:AllowedRegister"]);
    //            if (allowedRegister)
    //            {
    //                if (userId == null || email == null || code == null)
    //                {
    //                    return View("Error");
    //                }
    //                else
    //                {
    //                    ResetPasswordViewModel model = new ResetPasswordViewModel() { Code = HttpUtility.HtmlDecode(code).Trim(), Email = HttpUtility.HtmlDecode(email).Trim() };
    //                    return View(model);
    //                }
    //            }
    //            else
    //            {
    //                return ErrorView(new Exception(MessageHelper.UnhandledError));
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            return ErrorView(ex);
    //        }
    //    }

    //    //
    //    // POST: /Account/ResetPassword
    //    [HttpPost]
    //    [AllowAnonymous]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    //    {
    //        try
    //        {
    //            _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestStart("ResetPassword[POST]", $"UserEmail: {model.Email}"));
    //            var allowedRegister = Convert.ToBoolean(_iConfiguration["AppConfig:AllowedRegister"]);
    //            if (allowedRegister)
    //            {
    //                if (ModelState.IsValid)
    //                {
    //                    bool isValid = true;

    //                    var applicationUserViewModel = await _iApplicationUserManager.GetApplicationUserByUserNameOrEmailAsync(model.Email);
    //                    if (applicationUserViewModel is null)
    //                    {
    //                        isValid = false;
    //                        // Don't reveal that the user does not exist
    //                        ModelState.AddModelError(string.Empty, "Invalid email.");
    //                        _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestSuccess("ResetPassword[POST]", $"Invalid email, UserEmail: {model.Email}"));
    //                        return View(model);
    //                    }

    //                    //if (applicationUserViewModel is not null)
    //                    //{
    //                    //    var isEmailConfirmedResult = await _iApplicationUserManager.IsEmailConfirmedAsync(applicationUserViewModel);
    //                    //    if (isEmailConfirmedResult.Success == false)
    //                    //    {
    //                    //        isValid = false;
    //                    //        // Don't reveal that the user email is not confirmed
    //                    //        ModelState.AddModelError(string.Empty, "Email is not confirmed.");
    //                    //        _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestSuccess("ResetPassword[POST]", $"Email is not confirmed, UserEmail: {model.Email}"));
    //                    //        return View(model);
    //                    //    }
    //                    //}

    //                    if (isValid)
    //                    {
    //                        var result = await _iApplicationUserManager.ResetPasswordAsync(applicationUserViewModel, model.Code, model.Password);
    //                        if (result.Success)
    //                        {
    //                            return View("ResetPasswordConfirmation");
    //                        }
    //                    }

    //                }
    //            }
    //            else
    //            {
    //                return ErrorView(new Exception(MessageHelper.UnhandledError));
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            ModelState.AddModelError(string.Empty, MessageHelper.Error);
    //            _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "ResetPassword[POST]"));
    //        }

    //        // If we got this far, something failed, redisplay form
    //        return View(model);
    //    }

    //    //
    //    // GET: /Account/ResetPasswordConfirmation
    //    [HttpGet]
    //    [AllowAnonymous]
    //    public IActionResult ResetPasswordConfirmation()
    //    {
    //        return View();
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> LogOut()
    //    {
    //        try
    //        {
    //            _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestStart("LogOff", $"User:{User.Identity.Name}"));
    //            await _signInManager.SignOutAsync();
    //            _iLogger.LogInformation(LogMessageHelper.LogFormattedMessageForRequestSuccess("LogOff", $"User logged out"));
    //        }
    //        catch (Exception ex)
    //        {
    //            _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "LogOff"));
    //        }
    //        return RedirectToAction("Login", "Account");
    //    }

    //    private async Task<AppResult> IsEmailExists(string userNameOrEmail)
    //    {
    //        try
    //        {
    //            var isExists = await _iApplicationUserManager.GetApplicationUserByUserNameOrEmailAsync(userNameOrEmail);

    //            if (isExists != null)
    //            {
    //                string isEmailExistsMessage = string.Format(MessageHelper.IsEmailExists, userNameOrEmail);
    //                return AppResult.Fail(isEmailExistsMessage);
    //            }
    //            else
    //            {
    //                return AppResult.Ok();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            ModelState.AddModelError(string.Empty, MessageHelper.Error);
    //            _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "IsEmailExists"));
    //            return AppResult.Fail(MessageHelper.Error);
    //        }
    //    }

    //    #region Helpers

    //    private void AddErrors(AppResult result)
    //    {
    //        ModelState.AddModelError(string.Empty, result.Error);
    //    }

    //    private async Task<ApplicationUserViewModel> GetCurrentUserAsync()
    //    {
    //        return await _iApplicationUserManager.GetApplicationUserByUserNameOrEmailAsync(HttpContext.User.GetLoggedInUserEmail());
    //    }

    //    private IActionResult RedirectToLocal(string returnUrl)
    //    {
    //        if (Url.IsLocalUrl(returnUrl))
    //        {
    //            return Redirect(returnUrl);
    //        }
    //        else
    //        {
    //            return RedirectToAction(nameof(HomeController.Index), "Home");
    //        }
    //    }

    //    private async Task<string> GenareteForgotPasswordEmailTemplateAsync(ApplicationUserViewModel user)
    //    {
    //        string htmlTemplate = string.Empty;

    //        var passwordResetToken = await _iApplicationUserManager.GeneratePasswordResetTokenAsync(user);
    //        string link = Url.Action("ResetPassword", "Account", new { userId = user.Id, email = user.Email, code = passwordResetToken }, protocol: HttpContext.Request.Scheme);

    //        string title = "Please reset your password by clicking here:";
    //        string linkText = "Forgot Password";

    //        //htmlTemplate = "Please reset your password by clicking here: <a target='_blank' href=\"" + link + "\">link</a>";
    //        htmlTemplate = EmailTemplateHelper.GetEmailTemplate(title, link, linkText);

    //        return htmlTemplate;
    //    }

    //    #endregion

    //    #endregion
    //}
}