using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace rapid.erp.Core.Exceptions
{
    public static class ExceptionHelper
    {
        public static string ExceptionMessageFormat(System.Exception ex)
        {
            string message = "Error: There was a problem while processing your request: " + ex.Message;

            if (ex.InnerException != null)
            {
                System.Exception inner = ex.InnerException;
                if (inner is System.Data.Common.DbException)
                    message = "Database is currently experiencing problems. " + inner.Message;
                else if (inner is System.Data.DataException)
                    message = "Datebase is currently problem.";
                else if (inner is NullReferenceException)
                    message = "There are one or more required fields that are missing.";
                else if (inner is ArgumentException)
                {
                    string paramName = ((ArgumentException)inner).ParamName;
                    message = string.Concat("The ", paramName, " value is illegal.");
                }
                else if (inner is ApplicationException)
                    message = "Exception in application" + inner.Message;
                else
                    message = inner.Message;

            }

            return message;
        }

        public static string ExceptionMessageForNullObject()
        {
            string message = "Requested object could not found.";

            return message;
        }

        public static ErrorPageViewModel ExceptionErrorMessageFormat(System.Exception ex)
        {
            var errorPageViewModel = new ErrorPageViewModel();

            string message = "Error: There was a problem while processing your request: " + ex.Message;

            if (ex.InnerException != null)
            {
                System.Exception inner = ex.InnerException;
                if (inner is System.Data.Common.DbException)
                    message = "Database is currently experiencing problems. " + inner.Message;
                else if (inner is System.Data.DataException)
                    message = "Datebase is currently problem.";
                else if (inner is NullReferenceException)
                    message = "There are one or more required fields that are missing.";
                else if (inner is ArgumentException)
                {
                    string paramName = ((ArgumentException)inner).ParamName;
                    message = string.Concat("The ", paramName, " value is illegal.");
                }
                else if (inner is ApplicationException)
                    message = "Exception in application" + inner.Message;
                else
                    message = inner.Message;

            }

            errorPageViewModel.ErrorType = "error";
            errorPageViewModel.ErrorMessage = message;

            return errorPageViewModel;
        }

        public static ErrorPageViewModel ExceptionErrorMessageForNullObject()
        {
            var errorPageViewModel = new ErrorPageViewModel();

            string message = "Requested object could not found.";

            errorPageViewModel.ErrorType = "warn";
            errorPageViewModel.ErrorMessage = message;

            return errorPageViewModel;
        }

        public static ErrorPageViewModel ExceptionErrorMessageForCommon()
        {
            var errorPageViewModel = new ErrorPageViewModel();

            string message = "Oops! Exception in application.";

            errorPageViewModel.ErrorType = "info";
            errorPageViewModel.ErrorMessage = message;

            return errorPageViewModel;
        }

        public static string ModelStateErrorFormatForAlert(ModelStateDictionary modelStateDictionary)
        {
            string message = "<div class='row'>";
            message += "<div class='col-md-12 col-sm-12'>";

            message += "<div class='alert alert-danger alert-dismissable' data-autodismiss='alert'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button><strong></strong>";

            foreach (var modelStateValues in modelStateDictionary.Values)
            {
                if (modelStateValues.Errors.Any())
                {
                    foreach (var modelError in modelStateValues.Errors)
                    {
                        message += "<div class='row'>";
                        message += "<div class='col-md-12 col-sm-12'>";

                        message += modelError.ErrorMessage;

                        message += "</div>";
                        message += "</div>";
                    }
                }
            }


            message += "</div>";

            message += "</div>";
            message += "</div>";

            return message;
        }

        public static string ModelStateErrorFormat(ModelStateDictionary modelStateDictionary)
        {
            string message = string.Empty;

            foreach (var modelStateValues in modelStateDictionary.Values)
            {
                if (modelStateValues.Errors.Any())
                {
                    foreach (var modelError in modelStateValues.Errors)
                    {
                        message += "<div class='row'>";
                        message += "<div class='col-md-12 col-sm-12'>";

                        message += modelError.ErrorMessage;

                        message += "</div>";
                        message += "</div>";
                    }
                }
            }

            return message;
        }

        public static string ModelStateErrorFirstFormat(ModelStateDictionary modelStateDictionary)
        {
            string message = string.Empty;

            foreach (var modelStateValues in modelStateDictionary.Values)
            {
                if (modelStateValues.Errors.Any())
                {
                    var modelError = modelStateValues.Errors.FirstOrDefault();
                    message += modelError.ErrorMessage;
                }
            }

            return message;
        }

    }
}
