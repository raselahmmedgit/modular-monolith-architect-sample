using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace rapid.erp.Core.Service
{
    /// <summary>
    /// Custom MVC View render service module
    /// </summary>
    public interface IViewRenderService
    {
        /// <summary>
        /// Get view (after find, bind and render) as string
        /// </summary>
        /// <param name="viewPath">Name of the View</param>
        /// <returns></returns>
        string Render(string viewPath);

        /// <summary>
        /// Get view (after find, bind and render) as string
        /// </summary>
        /// <param name="viewPath">Name of the View</param>
        /// <param name="model">Binding data model</param>
        /// <returns></returns>
        string Render<TModel>(string viewPath, TModel model);
    }
    /// <summary>
    /// Custom MVC View render service module implementation
    /// </summary>
    public class ViewRenderService : IViewRenderService
    {
        IRazorViewEngine _viewEngine;
        IHttpContextAccessor _httpContextAccessor;
        IActionContextAccessor _actionContext;

        public ViewRenderService(IRazorViewEngine viewEngine, IHttpContextAccessor httpContextAccessor, IActionContextAccessor actionContext)
        {
            _viewEngine = viewEngine;
            _httpContextAccessor = httpContextAccessor;
            _actionContext = actionContext;
        }

        public string Render(string viewPath)
        {
            return Render(viewPath, string.Empty);
        }

        public string Render<TModel>(string viewPath, TModel model)
        {

            var actionContext = new ActionContext(_httpContextAccessor.HttpContext, _actionContext.ActionContext.RouteData, _actionContext.ActionContext.ActionDescriptor);
            var viewEngineResult = _viewEngine.FindView(actionContext, viewPath, false);

            if (!viewEngineResult.Success)
            {
                throw new InvalidOperationException($"Couldn't find view {viewPath}");
            }

            var view = viewEngineResult.View;

            using (var output = new StringWriter())
            {
                var viewContext = new ViewContext();
                viewContext.HttpContext = _httpContextAccessor.HttpContext;
                viewContext.ViewData = new ViewDataDictionary<TModel>(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                { Model = model };
                viewContext.Writer = output;

                view.RenderAsync(viewContext).GetAwaiter().GetResult();

                return output.ToString();
            }
        }
    }
}
