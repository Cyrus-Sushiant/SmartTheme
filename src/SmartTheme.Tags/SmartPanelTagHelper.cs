using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;

namespace SmartTheme.Tags
{
    [HtmlTargetElement("smart-panel")]
    public partial class SmartPanelTagHelper : TagHelper
    {
        private const string BackRouteValuesAttributeName = "asp-back-route-values";
        private const string BackRouteControllerNameAttributeName = "asp-back-route-controller";
        private const string BackRouteActionNameAttributeName = "asp-back-route-action";
        private const string BackRouteTextAttributeName = "asp-back-route-text";
        private const string TitleAttributeName = "asp-title";
        private IHtmlGenerator _generator;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName(TitleAttributeName)]
        public string Title { get; set; }

        [HtmlAttributeName(BackRouteControllerNameAttributeName)]
        public string BackRouteControllerName { get; set; }

        [HtmlAttributeName(BackRouteActionNameAttributeName)]
        public string BackRouteActionName { get; set; } = "Index";

        [HtmlAttributeName(BackRouteActionNameAttributeName)]
        public string BackRouteText { get; set; } = "برگشت به لیست";

        [HtmlAttributeName(BackRouteValuesAttributeName)]
        public object BackRouteValues { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="generator">HTML generator</param>
        public SmartPanelTagHelper(IHtmlGenerator generator) : base()
        {
            _generator = generator;
        }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="output">Output</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "panel-body");

            output.PreElement.AppendHtml("<div class='panel panel-flat'>");

            //header
            //open & close head tag
            if (!string.IsNullOrEmpty(Title))
                output.PreElement.AppendHtml($@"<div class='panel-heading'>
                                                    <h5 class='panel-title'>{Title}<a class='heading-elements-toggle'><i class='icon-more'></i></a></h5>
                                                    <div class='heading-elements'>
                                                        <ul class='icons-list'>
                                                            <li><a data-action='collapse'></a></li>
                                                        </ul>
                                                    </div>
                                                </div>");


            //footer
            var builder = new HtmlContentBuilder();
            builder.AppendHtml("<div class='panel-footer text-right'>");

            //Back btn
            var actionAnchor = _generator.GenerateActionLink(
                                        ViewContext,
                                        linkText: BackRouteText,
                                        actionName: BackRouteActionName,
                                        controllerName: BackRouteControllerName,
                                        fragment: null,
                                        hostname: null,
                                        htmlAttributes: new Dictionary<string, object> { { "class", "btn bg-slate btn-labeled" } },
                                        protocol: null,
                                        routeValues: BackRouteValues
                                        );

            actionAnchor.InnerHtml.AppendHtml("<b><i class='fa fa-backspace'></i></b>");
            builder.AppendHtml(actionAnchor);

            builder.AppendHtml("</div>");
            output.PostElement.AppendHtml(builder);
            output.PostElement.AppendHtml("</div>");

            base.Process(context, output);
        }
    }
}
