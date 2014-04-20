namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Helpers
{
    using System.Web.Mvc;

    public static class HtmlHelpers
    {
        public static MvcHtmlString AjaxModalWindow(
            this HtmlHelper htmlHelper,
            string buttonText,
            string targetUrl,
            string modalId,
            string targetId,
            string cssClass,
            string iconClass,
            string dataGet)
        {
            var builder = new TagBuilder("button");
            builder.SetInnerText(" " + buttonText);
            builder.Attributes.Add("type", "button");
            builder.Attributes.Add("data-target-url", targetUrl);
            builder.Attributes.Add("data-target-id", targetId);
            builder.Attributes.Add("data-modal-id", modalId);
            builder.Attributes.Add("data-get", dataGet);
            builder.AddCssClass("ajax-modal-window");
            builder.AddCssClass(cssClass);
            var iconBuilder = new TagBuilder("i");
            iconBuilder.AddCssClass(iconClass);
            builder.InnerHtml += iconBuilder.ToString();

            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString AjaxLinkModalWindow(
            this HtmlHelper htmlHelper,
            string linkText,
            string targetUrl,
            string modalId,
            string targetId,
            string cssClass,
            string iconClass,
            string dataGet)
        {
            var builder = new TagBuilder("a");
            builder.SetInnerText(" " + linkText);
            builder.Attributes.Add("href", "#");
            builder.Attributes.Add("data-target-url", targetUrl);
            builder.Attributes.Add("data-target-id", targetId);
            builder.Attributes.Add("data-modal-id", modalId);
            builder.Attributes.Add("data-get", dataGet);
            builder.AddCssClass("ajax-link-modal-window");
            builder.AddCssClass(cssClass);
            var iconBuilder = new TagBuilder("i");
            iconBuilder.AddCssClass(iconClass);
            builder.InnerHtml += iconBuilder.ToString();

            return new MvcHtmlString(builder.ToString());
        }
    }
}