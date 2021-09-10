using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace SmartTheme.Tags
{
    [HtmlTargetElement("smart-page")]
    public partial class SmartPageTagHelper : TagHelper
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
            output.Attributes.Add("class", "content");

            output.PreElement.AppendHtml($@"<div class='page-header'>
                                                 <div class='page-header-content'>
                                                    <div class='page-title'>
                                                        <h4><i class='icon-arrow-left52 position-left'></i> <span class='text-semibold'>{Title}</span></h4>
                                                    </div>
                                                </div>
                                            </div>");


            base.Process(context, output);
        }
    }
}
