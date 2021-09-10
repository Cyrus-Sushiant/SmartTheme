using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace SmartTheme.Tags
{
    [HtmlTargetElement("smart-panel-header")]
    public partial class SmartPanelHeaderTagHelper : TagHelper
    {
        private const string TitleAttributeName = "asp-title";

        [HtmlAttributeName(TitleAttributeName)]
        public string Title { get; set; }

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
            output.Attributes.Add("class", "panel-heading");

            output.PreContent.AppendHtml($@"<div class='page-header-content'>
                                                    <h5 class='page-title'>
                                                        {Title}
                                                    </h5>
                                                    <div class='heading-elements'>
                                                        <ul class='icons-list'>
                                                            <li><a data-action='collapse'></a></li>
                                                        </ul>
                                                    </div>
                                                </div>");

            base.Process(context, output);
        }
    }
}
