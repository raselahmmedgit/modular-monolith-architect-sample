using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using System.Text.Encodings.Web;

public static class HtmlHelperExtensions
{
    #region Message Helpers

    #endregion

    #region Wrapper Panels

    #region Primitive Controls

    public static IHtmlContent TextBoxCalenderFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
    {
        Func<TModel, TProperty> deleg = expression.Compile();
        var result = deleg(htmlHelper.ViewData.Model);

        string value = null;

        if (result.ToString() == DateTime.MinValue.ToString())
            value = string.Empty;
        else
            value = string.Format("{0:M/dd/yyyy}", result);

        return htmlHelper.TextBoxFor(expression, new { @class = "datepicker text", Value = value });
    }

    public static IHtmlContent EditorCalenderFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
    {
        Func<TModel, TProperty> deleg = expression.Compile();
        var result = deleg(htmlHelper.ViewData.Model);

        string value = null;

        if (result.ToString() == DateTime.MinValue.ToString())
            value = string.Empty;
        else
            value = string.Format("{0:M/dd/yyyy}", result);

        return htmlHelper.TextBoxFor(expression, new { @class = "datepicker text", Value = value });
    }

    #endregion

    #region ValidationMessage Controls

    public static IHtmlContent CustomModalValidationMessageFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
    {
        IHtmlContent content = htmlHelper.ValidationMessageFor(expression);
        var strContent = GetString(content);
        string htmlString = string.Empty;
        htmlString = strContent.Replace("span", "div").Replace("field-validation-valid", "col-md-8 md-offset-4");
        return new HtmlString(htmlString);
    }

    public static IHtmlContent CustomValidationMessageFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
    {
        IHtmlContent content = htmlHelper.ValidationMessageFor(expression);
        var strContent = GetString(content);
        string htmlString = string.Empty;
        htmlString = strContent.Replace("span", "div").Replace("field-validation-valid", "col-md-12");
        return new HtmlString(htmlString);
    }

    internal static string GetString(IHtmlContent content)
    {
        using (var writer = new System.IO.StringWriter())
        {
            content.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
    }

    #endregion

    #endregion
}
