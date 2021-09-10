using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace SmartTheme.Tags
{
    [HtmlTargetElement("smart-label", Attributes = ForAttributeName)]
    public partial class SmartLabel : LabelTagHelper
    {
        private const string ForAttributeName = "asp-for";
        private const string SizeAttributeName = "asp-col-size";
        private const string TextAttributeName = "asp-text";
        private const string CSSClassAttributeName = "class";

        [HtmlAttributeName(TextAttributeName)]
        public string Text { get; set; }

        [HtmlAttributeName(SizeAttributeName)]
        public int ColSize { get; set; } = 4;

        [HtmlAttributeName(CSSClassAttributeName)]
        public string CSSClass { get; set; }

        public SmartLabel(IHtmlGenerator generator) : base(generator)
        {
        }

        /// <summary>
        /// ProcessAsync
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="output">Output</param>
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            this.CustomProcess(context, output);
            return base.ProcessAsync(context, output);
        }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="output">Output</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            this.CustomProcess(context, output);
            base.Process(context, output);
        }

        private void CustomProcess(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            string classes;
            string colSize = SmartMainHelper.GetColSize(ColSize);
            if (string.IsNullOrEmpty(CSSClass) || string.IsNullOrEmpty(CSSClass.Trim()))
            {
                classes = colSize;
            }
            else
            {
                classes = $"{colSize} {CSSClass.Trim()}";
            }
            output.Attributes.SetAttribute("class", classes);

            if (output.TagMode == TagMode.SelfClosing)
            {
                output.TagMode = TagMode.StartTagAndEndTag;
            }
        }
    }
}
