using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace SmartTheme.Tags
{
    [HtmlTargetElement("smart-file-upload", Attributes = ForAttributeName)]
    public partial class SmartFileUploadTagHelper : TagHelper
    {
        private const string ForAttributeName = "asp-for";
        private const string SizeAttributeName = "asp-col-size";
        private const string VisibleAttributeName = "asp-visible";
        private const string TitleAttributeName = "asp-title";
        private const string HelpAttributeName = "asp-help";

        [HtmlAttributeName(ForAttributeName)]
        public string For { get; set; }

        [HtmlAttributeName(SizeAttributeName)]
        public int ColSize { get; set; } = 4;

        [HtmlAttributeName(VisibleAttributeName)]
        public bool Visible { get; set; } = true;

        [HtmlAttributeName(TitleAttributeName)]
        public string Title { get; set; }

        [HtmlAttributeName(HelpAttributeName)]
        public string Help { get; set; }

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

            if (!Visible)
            {
                output.TagName = "";
                output.Content.SetHtmlContent("");
                return;
            }

            //output.SuppressOutput();

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            string colSize = SmartMainHelper.GetColSize(ColSize);
            output.Attributes.Add("class", $"{colSize} form-group");

            string helpTag = null;
            if (!string.IsNullOrEmpty(Help))
            {
                helpTag = $"<span data-popup='popover' data-target='hover' data-placement='top' data-original-title='راهنما' data-content='{Help}'><i class='fa fa-info-circle text-primary'></i></span>";
            }

            output.PreContent.AppendHtml($@"<label >{Title}{helpTag}</label>
                                                <input name='{For}' class='file-input' id='{For}' data-show-upload='false' data-show-caption='false' data-show-preview='true' onchange=""$('uploader-{For}').val(this.value);"" type='file' />
                                           ");

            base.Process(context, output);
        }
    }
}
