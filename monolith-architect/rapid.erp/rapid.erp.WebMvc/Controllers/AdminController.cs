using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using rapid.erp.Core.FlashMessage;
using rapid.erp.Core.Helpers;
using rapid.erp.Core.Security.Manager;
using rapid.erp.Core.Security;
using rapid.erp.Core.Utility;
using rapid.erp.ViewModel.Security;

namespace rapid.erp.WebMvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        #region Global Variable Declaration
        private IMapper _mapper;
        private SignInManager<ApplicationUser> _signInManager;
        private RoleManager<ApplicationRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AdminController> _iLogger;
        private readonly IFlashMessage _iFlashMessage;
        #endregion

        #region Constructor
        public AdminController(ILogger<AdminController> iLogger, 
            IFlashMessage iFlashMessage,
            IMapper mapper,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _iLogger = iLogger;
            _iFlashMessage = iFlashMessage;

            _mapper = mapper;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;

        }
        #endregion

        #region Actions
        public IActionResult Index()
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

        public async Task<IActionResult> Role()
        {
            try
            {
                var roleList = await _roleManager.Roles.ToListAsync();
                var viewModelList = _mapper.Map<List<ApplicationRole>, List<ApplicationRoleViewModel>>(roleList);
                return View(viewModelList);
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        public IActionResult CreateRole()
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

        [HttpPost]
        public async Task<IActionResult> CreateRole(ApplicationRoleCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Id = model.Name;
                    model.IsActive = true;

                    var result = new AppResult();
                    var applicationRole = _mapper.Map<ApplicationRoleCreateViewModel, ApplicationRole>(model);
                    var identityResult = await _roleManager.CreateAsync(applicationRole);
                    if (identityResult.Succeeded == true)
                    {
                        return RedirectToAction("Role", "Admin");
                    }
                    else
                    {
                        AddErrorsFromResult(identityResult);
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "CreateRole[POST]"));
            }
            return RedirectToAction("Role", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            try
            {
                var applicationRole = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
                var identityResult = await _roleManager.DeleteAsync(applicationRole);
                if (identityResult.Succeeded == true)
                {
                    return RedirectToAction("Role");
                }
                else
                {
                    AddErrorsFromResult(identityResult);
                }

                return RedirectToAction("Role", "Admin");
            }
            catch (Exception ex)
            {
                _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "DeleteRole[POST]"));
            }
            return RedirectToAction("Role", "Admin");
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            try
            {
                foreach (IdentityError error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            catch (Exception ex)
            {
                _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "AddErrorsFromResult"));
            }
        }

        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception ex)
            {
                _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "Logout"));
            }
            return RedirectToAction("Index", "Login");

        }
        #endregion
    }
}