using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using rapid.erp.Core.Exceptions;
using rapid.erp.Core.FlashMessage;
using rapid.erp.Core.Helpers;
using rapid.erp.Core.Security;
using rapid.erp.Core.Utility;
using rapid.erp.IManager;
using rapid.erp.ViewModel.Admin;
using rapid.erp.ViewModel.Security;

namespace rapid.erp.WebMvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CompanyController : BaseController
    {
        #region Global Variable Declaration
        private readonly ICompanyManager _iCompanyManager;
        private readonly ILogger<AccountController> _iLogger;
        private readonly IFlashMessage _iFlashMessage;
        #endregion

        #region Constructor
        public CompanyController(ILogger<AccountController> iLogger, IFlashMessage iFlashMessage, ICompanyManager iCompanyManager)
        {
            _iLogger = iLogger;
            _iFlashMessage = iFlashMessage;

            _iCompanyManager = iCompanyManager;
        }
        #endregion

        #region Actions

        [ResponseCache(NoStore = true, Duration = 0)]
        public async Task<IActionResult> Index()
        {
            try
            {
                var viewModelList = await _iCompanyManager.GetCompanysAsync();
                return View(viewModelList);
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        [ResponseCache(NoStore = true, Duration = 0)]
        public async Task<IActionResult> Add()
        {
            try
            {
                var isAdded = await _iCompanyManager.GetCompanysAsync();
                if (isAdded.Any())
                {
                    this.FlashSuccess(MessageHelper.AlreadyAdded, "CompanyMessage");
                    return RedirectToAction("Index", "Company");
                }

                var model = new CompanyViewModel();
                if (model != null)
                {
                    return View("Add", model);
                }
                else
                {
                    this.FlashError(ExceptionHelper.ExceptionErrorMessageForNullObject(), "CompanyMessage");
                    return RedirectToAction("Index", "Company");
                }
            }
            catch (Exception ex)
            {
                _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "Add[GET]"));
            }

            this.FlashError(MessageHelper.UnhandledError, "CompanyMessage");
            return RedirectToAction("Index", "Company");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CompanyCreateViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = new AppResult();

                    result = await _iCompanyManager.CreateCompanyAsync(viewModel);

                    if (result.Success)
                    {
                        this.FlashSuccess(MessageHelper.Save, "CompanyMessage");
                        return RedirectToAction("Index", "Company");
                    }
                    else
                    {
                        this.FlashError(result.Message, "CompanyMessage");
                        return View(viewModel);
                    }
                }
                else
                {
                    this.FlashError(ExceptionHelper.ModelStateErrorFirstFormat(ModelState), "CompanyMessage");
                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "Add[POST]"));
            }

            this.FlashError(MessageHelper.UnhandledError, "CompanyMessage");
            return View(viewModel);
        }

        [ResponseCache(NoStore = true, Duration = 0)]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var model = await _iCompanyManager.GetCompanyAsync(id);

                if (model != null)
                {
                    return View("Edit", model);
                }
                else
                {
                    this.FlashError(ExceptionHelper.ExceptionErrorMessageForNullObject(), "CompanyMessage");
                    return RedirectToAction("Index", "Company");
                }
            }
            catch (Exception ex)
            {
                _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "Edit[GET]"));
            }

            this.FlashError(MessageHelper.UnhandledError, "CompanyMessage");
            return RedirectToAction("Index", "Company");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompanyEditViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = new AppResult();

                    result = await _iCompanyManager.UpdateCompanyAsync(viewModel);

                    if (result.Success)
                    {
                        this.FlashSuccess(MessageHelper.Save, "CompanyMessage");
                        return RedirectToAction("Index", "Company");
                    }
                    else
                    {
                        this.FlashError(result.Message, "CompanyMessage");
                        return View(viewModel);
                    }
                }
                else
                {
                    this.FlashError(ExceptionHelper.ModelStateErrorFirstFormat(ModelState), "CompanyMessage");
                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "Edit[POST]"));
            }

            this.FlashError(MessageHelper.UnhandledError, "CompanyMessage");
            return View(viewModel);
        }

        [ResponseCache(NoStore = true, Duration = 0)]
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var configViewModel = await _iCompanyManager.GetCompanyAsync(id);

                if (configViewModel != null)
                {
                    return View("Details", configViewModel);
                }
                else
                {
                    this.FlashError(ExceptionHelper.ExceptionErrorMessageForNullObject(), "CompanyMessage");
                    return RedirectToAction("Index", "Company");
                }
            }
            catch (Exception ex)
            {
                _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "Details[GET]"));
            }

            this.FlashError(MessageHelper.UnhandledError, "CompanyMessage");
            return RedirectToAction("Index", "Company");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CompanyViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = new AppResult();

                    result = await _iCompanyManager.CreateOrUpdateCompanyAsync(viewModel);

                    if (result.Success)
                    {
                        this.FlashSuccess(MessageHelper.Save, "CompanyMessage");
                        return RedirectToAction("Index", "Company");
                    }
                    else
                    {
                        this.FlashError(result.Message, "CompanyMessage");
                        return View(viewModel);
                    }
                }
                else
                {
                    this.FlashError(ExceptionHelper.ModelStateErrorFirstFormat(ModelState), "CompanyMessage");
                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "Save[POST]"));
            }

            this.FlashError(MessageHelper.UnhandledError, "CompanyMessage");
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var result = new AppResult();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    result = await _iCompanyManager.DeleteCompanyAsync(id);
                }
                else
                {
                    result = AppResult.Fail(MessageHelper.DeleteFail);
                }

                if (result.Success)
                {
                    this.FlashSuccess(MessageHelper.Delete, "CompanyMessage");
                    return RedirectToAction("Index", "Company");
                }
                else
                {
                    this.FlashError(result.Message, "CompanyMessage");
                    return RedirectToAction("Index", "Company");
                }
            }
            catch (Exception ex)
            {
                _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "Delete[POST]"));
                result = AppResult.Fail(MessageHelper.UnhandledError);
                return JsonResult(result);
            }
        }

        #endregion
    }
}