using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using rapid.erp.Core.FlashMessage;
using rapid.erp.Core.Helpers;
using rapid.erp.Core.Security.Manager;
using rapid.erp.Core.Security;
using rapid.erp.Core.Utility;
using rapid.erp.ViewModel.Security;

namespace rapid.erp.WebMvc.Controllers
{
    //[Authorize(Roles = "Admin")]
    //public class AdminController : BaseController
    //{
    //    #region Global Variable Declaration
    //    private SignInManager<ApplicationUser> _signInManager;
    //    private readonly IApplicationRoleManager _iApplicationRoleManager;
    //    private readonly IApplicationUserManager _iApplicationUserManager;
    //    private readonly ILogger<AdminController> _iLogger;
    //    private readonly IFlashMessage _iFlashMessage;
    //    #endregion

    //    #region Constructor
    //    public AdminController(ILogger<AdminController> iLogger, 
    //        IFlashMessage iFlashMessage,
    //        SignInManager<ApplicationUser> signInManager,
    //        IApplicationRoleManager iApplicationRoleManager,
    //        IApplicationUserManager iApplicationUserManager)
    //    {
    //        _iLogger = iLogger;
    //        _iFlashMessage = iFlashMessage;

    //        _signInManager = signInManager;
    //        _iApplicationRoleManager = iApplicationRoleManager;
    //        _iApplicationUserManager = iApplicationUserManager;
            
    //    }
    //    #endregion

    //    #region Actions
    //    public IActionResult Index()
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

    //    public async Task<IActionResult> Role()
    //    {
    //        try
    //        {
    //            var viewModelList = await _iApplicationRoleManager.GetApplicationRolesAsync();
    //            return View(viewModelList);
    //        }
    //        catch (Exception ex)
    //        {
    //            return ErrorView(ex);
    //        }
    //    }

    //    public IActionResult CreateRole()
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

    //    [HttpPost]
    //    public async Task<IActionResult> CreateRole(ApplicationRoleCreateViewModel model)
    //    {
    //        try
    //        {
    //            if (ModelState.IsValid)
    //            {
    //                model.Id = model.Name;
    //                model.IsActive = true;

    //                var result = await _iApplicationRoleManager.CreateApplicationRoleAsync(model);
    //                if (result.Success)
    //                {
    //                    return RedirectToAction("Role", "Admin");
    //                }
    //                else
    //                { 
    //                    AddErrorsFromResult(result); 
    //                }
    //            }
    //            return View(model);
    //        }
    //        catch (Exception ex)
    //        {
    //            _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "CreateRole[POST]"));
    //        }
    //        return RedirectToAction("Role", "Admin");
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> DeleteRole(string id)
    //    {
    //        try
    //        {
    //            var result = await _iApplicationRoleManager.DeleteApplicationRoleAsync(id);
    //            if (result.Success)
    //            {
    //                return RedirectToAction("Role");
    //            }
    //            else
    //            {
    //                AddErrorsFromResult(result);
    //            }
    //            return RedirectToAction("Role", "Admin");
    //        }
    //        catch (Exception ex)
    //        {
    //            _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "DeleteRole[POST]"));
    //        }
    //        return RedirectToAction("Role", "Admin");
    //    }

    //    private void AddErrorsFromResult(AppResult result)
    //    {
    //        try
    //        {
    //            ModelState.AddModelError("", result.Error);
    //        }
    //        catch (Exception ex)
    //        {
    //            _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "AddErrorsFromResult"));
    //        }
    //    }

    //    public async Task<IActionResult> Logout()
    //    {
    //        try
    //        {
    //            await _signInManager.SignOutAsync();
    //        }
    //        catch (Exception ex)
    //        {
    //            _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "Logout"));
    //        }
    //        return RedirectToAction("Index", "Login");

    //    }
    //    #endregion
    //}
}