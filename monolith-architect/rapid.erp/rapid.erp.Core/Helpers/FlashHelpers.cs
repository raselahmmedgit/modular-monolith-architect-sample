using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using rapid.erp.Core.Exceptions;
using rapid.erp.Core.Helpers;
using System;
using System.Web;

namespace Microsoft.AspNetCore.Mvc
{
    public static class FlashHelpers
    {
        private static string _tempDataString = "alert";

        public static ActionResult WithInfo(this ActionResult actionResult, Controller controller, string title, string message, string tmpData, bool hideTitle)
        {
            AddMessageToTempData(controller, AlertType.Info, title, message, tmpData);
            return actionResult;
        }
        public static ActionResult WithInfo(this ActionResult actionResult, Controller controller, string title, string message, string tmpData)
        {
            AddMessageToTempData(controller, AlertType.Info, title, message, tmpData);
            return actionResult;
        }
        public static ActionResult WithInfo(this ActionResult actionResult, Controller controller, string message, string tmpData)
        {
            AddMessageToTempData(controller, AlertType.Info, "", message, tmpData);
            return actionResult;
        }
        public static ActionResult WithInfo(this ActionResult actionResult, Controller controller, string message)
        {
            AddMessageToTempData(controller, AlertType.Info, "", message);
            return actionResult;
        }

        public static ActionResult WithSuccess(this ActionResult actionResult, Controller controller, string title, string message)
        {
            AddMessageToTempData(controller, AlertType.Success, title, message);
            return actionResult;
        }
        public static ActionResult WithSuccess(this ActionResult actionResult, Controller controller, string message)
        {
            AddMessageToTempData(controller, AlertType.Success, "", message);
            return actionResult;
        }

        public static ActionResult WithWarning(this ActionResult actionResult, Controller controller, string title = "", string message = "")
        {
            AddMessageToTempData(controller, AlertType.Warning, title, message);
            return actionResult;
        }

        public static ActionResult WithError(this ActionResult actionResult, Controller controller, string title, string message, string tmpData)
        {
            AddMessageToTempData(controller, AlertType.Error, title, message);
            return actionResult;
        }
        public static ActionResult WithError(this ActionResult actionResult, Controller controller, string message = "")
        {
            AddMessageToTempData(controller, AlertType.Error, "", message);
            return actionResult;
        }
        public static ActionResult WithError(this ActionResult actionResult, Controller controller, string message, string tmpData)
        {
            AddMessageToTempData(controller, AlertType.Error, "", message, tmpData);
            return actionResult;
        }

        public static void FlashInfo(this Controller controller, string title, string message, string tmpData)
        {
            AddMessageToTempData(controller, AlertType.Info, title, message, string.IsNullOrEmpty(tmpData) ? _tempDataString : tmpData);
        }
        public static void FlashInfo(this Controller controller, string message, string tmpData)
        {
            AddMessageToTempData(controller, AlertType.Info, "", message, tmpData);
        }
        public static void FlashInfo(this Controller controller, string message)
        {
            AddMessageToTempData(controller, AlertType.Info, "", message, _tempDataString);
        }
        public static void FlashInfo(this Controller controller, ErrorPageViewModel errorPageViewModel)
        {
            AddMessageToTempData(controller, AlertType.Info, "", errorPageViewModel.ErrorMessage, _tempDataString);
        }
        public static void FlashInfo(this Controller controller, ErrorPageViewModel errorPageViewModel, string tmpData)
        {
            AddMessageToTempData(controller, AlertType.Info, "", errorPageViewModel.ErrorMessage, tmpData);
        }

        public static void FlashSuccess(this Controller controller, string title, string message, string tmpData)
        {
            AddMessageToTempData(controller, AlertType.Success, title, message, tmpData);
        }
        public static void FlashSuccess(this Controller controller, string message, string tmpData)
        {
            AddMessageToTempData(controller, AlertType.Success, "", message, tmpData);
        }
        public static void FlashSuccess(this Controller controller, string message)
        {
            AddMessageToTempData(controller, AlertType.Success, "", message);
        }
        public static void FlashSuccess(this Controller controller, ErrorPageViewModel errorPageViewModel)
        {
            AddMessageToTempData(controller, AlertType.Success, "", errorPageViewModel.ErrorMessage, _tempDataString);
        }
        public static void FlashSuccess(this Controller controller, ErrorPageViewModel errorPageViewModel, string tmpData)
        {
            AddMessageToTempData(controller, AlertType.Success, "", errorPageViewModel.ErrorMessage, tmpData);
        }

        public static void FlashWarning(this Controller controller, string message)
        {
            AddMessageToTempData(controller, AlertType.Warning, "", message);
        }
        public static void FlashWarning(this Controller controller, string message, string tmpData)
        {
            AddMessageToTempData(controller, AlertType.Warning, "", message, tmpData);
        }
        public static void FlashWarning(this Controller controller, string title, string message, string tmpData)
        {
            AddMessageToTempData(controller, AlertType.Warning, title, message, tmpData);
        }
        public static void FlashWarning(this Controller controller, ErrorPageViewModel errorPageViewModel)
        {
            AddMessageToTempData(controller, AlertType.Warning, "", errorPageViewModel.ErrorMessage, _tempDataString);
        }
        public static void FlashWarning(this Controller controller, ErrorPageViewModel errorPageViewModel, string tmpData)
        {
            AddMessageToTempData(controller, AlertType.Warning, "", errorPageViewModel.ErrorMessage, tmpData);
        }

        public static void FlashError(this Controller controller, string message)
        {
            AddMessageToTempData(controller, AlertType.Error, "", message);
        }
        public static void FlashError(this Controller controller, string message, string tmpData)
        {
            AddMessageToTempData(controller, AlertType.Error, "", message, tmpData);
        }
        public static void FlashError(this Controller controller, string title, string message, string tmpData)
        {
            AddMessageToTempData(controller, AlertType.Error, title, message, tmpData);
        }
        public static void FlashError(this Controller controller, ErrorPageViewModel errorPageViewModel)
        {
            AddMessageToTempData(controller, AlertType.Error, "", errorPageViewModel.ErrorMessage, _tempDataString);
        }
        public static void FlashError(this Controller controller, ErrorPageViewModel errorPageViewModel, string tmpData)
        {
            AddMessageToTempData(controller, AlertType.Error, "", errorPageViewModel.ErrorMessage, tmpData);
        }

        public static IHtmlContent RenderAlertRaw(this IHtmlHelper helper, string tempData = "")
        {
            object alertData = helper.ViewContext.TempData[tempData ?? _tempDataString];
            if (alertData == null)
                return HtmlString.Empty;
            var alert = new AlertMessage(alertData.ToString());
            return new HtmlString(alert.Title);
        }
        public static IHtmlContent RenderAlert(this IHtmlHelper helper, string tempData)
        {
            object alertData = helper.ViewContext.TempData[tempData ?? _tempDataString];

            if (alertData == null)
                return HtmlString.Empty;

            var alert = new AlertMessage(alertData.ToString());

            if (String.IsNullOrEmpty(alert.Message) && String.IsNullOrEmpty(alert.Title))
                return HtmlString.Empty;
            return
                new HtmlString(String.Format(
                    "<div class='row'> <div class='col-12'> <div class='alert alert-{0} alert-dismissable'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button><strong>{1}</strong>{2}</div> </div> </div>",
                    alert.CssClass, HttpUtility.HtmlEncode(alert.Title), HttpUtility.HtmlEncode(alert.Message)));
        }
        public static IHtmlContent RenderAlert(this IHtmlHelper helper)
        {
            return RenderAlert(helper, _tempDataString);
        }
        public static IHtmlContent RenderStaticAlertInfo(this IHtmlHelper helper, string message)
        {
            if (string.IsNullOrEmpty(message) && string.IsNullOrEmpty(message))
                return HtmlString.Empty;
            return
                new HtmlString(string.Format(
                    "<div class='row'> <div class='col-12'> <div class='alert alert-{0} alert-dismissable'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button><strong>{1}</strong>{2}</div> </div> </div>",
                    MessageHelper.MessageTypeInfo, "", HttpUtility.HtmlEncode(message)));
        }
        public static IHtmlContent RenderStaticAlertWarning(this IHtmlHelper helper, string message)
        {
            if (string.IsNullOrEmpty(message) && string.IsNullOrEmpty(message))
                return HtmlString.Empty;
            return
                new HtmlString(string.Format(
                    "<div class='row'> <div class='col-12'> <div class='alert alert-{0} alert-dismissable'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button><strong>{1}</strong>{2}</div> </div> </div>",
                    MessageHelper.MessageTypeWarning, "", HttpUtility.HtmlEncode(message)));
        }
        public static IHtmlContent RenderStaticAlertError(this IHtmlHelper helper, string message)
        {
            if (string.IsNullOrEmpty(message) && string.IsNullOrEmpty(message))
                return HtmlString.Empty;
            return
                new HtmlString(string.Format(
                    "<div class='row'> <div class='col-12'> <div class='alert alert-{0} alert-dismissable'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button><strong>{1}</strong>{2}</div> </div> </div>",
                    MessageHelper.MessageTypeDanger, "", HttpUtility.HtmlEncode(message)));
        }
        public static IHtmlContent RenderStaticAlertSuccess(this IHtmlHelper helper, string message)
        {
            if (string.IsNullOrEmpty(message) && string.IsNullOrEmpty(message))
                return HtmlString.Empty;
            return
                new HtmlString(string.Format(
                    "<div class='row'> <div class='col-12'> <div class='alert alert-{0} alert-dismissable'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button><strong>{1}</strong>{2}</div> </div> </div>",
                    MessageHelper.MessageTypeSuccess, "", HttpUtility.HtmlEncode(message)));
        }
        public static IHtmlContent RenderAlert(this IHtmlHelper helper, string message, string title = "", string cssclass = "success")
        {
            if (message == null)
                return HtmlString.Empty;

            if (String.IsNullOrEmpty(message) && String.IsNullOrEmpty(title))
                return HtmlString.Empty;
            return
                new HtmlString(String.Format(
                    "<div class='row'> <div class='col-12'> <div class='alert alert-{0} alert-dismissable'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button><strong>{1}</strong>{2}</div> </div> </div>",
                    cssclass, HttpUtility.HtmlEncode(title), HttpUtility.HtmlEncode(message)));
        }

        private static void AddMessageToTempData(Controller controller, AlertType type, string title, string message)
        {
            var alertMessage = new AlertMessage
            {
                Title = title,
                Message = message,
                CssClass = type.CssClass
            };

            controller.TempData[_tempDataString] = alertMessage.AsJson();
        }
        private static void AddMessageToTempData(Controller controller, AlertType type, string title, string message, string tempData)
        {
            var alertMessage = new AlertMessage
            {
                Title = title,
                Message = message,
                CssClass = type.CssClass
            };

            controller.TempData[tempData] = alertMessage.AsJson();
        }

        internal class AlertType
        {
            public string CssClass { get; private set; }

            public AlertType(string cssClass)
            {
                CssClass = cssClass;
            }

            public static AlertType Success = new AlertType("success");
            public static AlertType Info = new AlertType("info");
            public static AlertType Warning = new AlertType("warning");
            public static AlertType Error = new AlertType("danger");
        }

        internal class AlertMessage
        {
            public string Title { get; set; }
            public string Message { get; set; }
            public string CssClass { get; set; }

            public AlertMessage() { }
            public AlertMessage(string json)
            {
                var alertMessage = JsonConvert.DeserializeObject<AlertMessage>(json);

                Title = alertMessage.Title;
                Message = alertMessage.Message;
                CssClass = alertMessage.CssClass;
            }

            public string AsJson()
            {
                return JsonConvert.SerializeObject(this);
            }
        }

    }
}
