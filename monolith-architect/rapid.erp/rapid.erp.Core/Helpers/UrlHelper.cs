using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;

namespace rapid.erp.Core.Helpers
{
    public class UrlHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        public UrlHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }
        public string GenerateUrl(ViewContext viewContext, string actionName, string controllerName, object routeValues)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(viewContext);
            var url = urlHelper.Action(actionName, controllerName, routeValues, null, null, null);
            return url;
        }
    }
}
