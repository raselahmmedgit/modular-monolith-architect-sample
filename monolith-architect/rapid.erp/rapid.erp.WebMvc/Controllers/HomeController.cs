using Microsoft.AspNetCore.Mvc;
using rapid.erp.Core.FlashMessage;

namespace rapid.erp.WebMvc.Controllers
{
    public class HomeController : BaseController
    {
        #region Global Variable Declaration
        private readonly ILogger<HomeController> _iLogger;
        private readonly IFlashMessage _iFlashMessage;
        #endregion

        #region Constructor
        public HomeController(ILogger<HomeController> iLogger, IFlashMessage iFlashMessage)
        {
            _iLogger = iLogger;
            _iFlashMessage = iFlashMessage;
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

        #endregion
    }
}