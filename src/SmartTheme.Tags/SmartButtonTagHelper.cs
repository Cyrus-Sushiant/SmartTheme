using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace SmartTheme.Tags
{
    [HtmlTargetElement("smart-button")]
    public partial class SmartButtonTagHelper : TagHelper
    {
        private const string VisibleAttributeName = "asp-visible";
        private const string SizeAttributeName = "asp-size";
        private const string BackgroundAttributeName = "asp-bg";
        private const string TypeAttributeName = "asp-type";
        private const string FaIconAttributeName = "asp-fa-icon";
        private const string CSSClassAttributeName = "class";

        [HtmlAttributeName(VisibleAttributeName)]
        public bool Visible { get; set; } = true;

        [HtmlAttributeName(SizeAttributeName)]
        public BtnSize Size { get; set; } = BtnSize.MD;

        [HtmlAttributeName(BackgroundAttributeName)]
        public BtnBg Bg { get; set; } = BtnBg.Primary;

        [HtmlAttributeName(TypeAttributeName)]
        public BtnType Type { get; set; } = BtnType.Button;

        [HtmlAttributeName(CSSClassAttributeName)]
        public string CSSClass { get; set; }

        [HtmlAttributeName(FaIconAttributeName)]
        public string FaIcon { get; set; }

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

            //output.SuppressOutput();

            if (!Visible)
            {
                output.TagName = "";
                output.Content.SetHtmlContent("");
                return;
            }

            output.TagName = "button";
            output.TagMode = TagMode.StartTagAndEndTag;

            string css = $"btn bg-{Bg.ToString().ToLower()} btn-labeled legitRipple";
            if (Size != BtnSize.MD)
            {
                css = $"{css} {(Size == BtnSize.SM ? "btn-sm" : "btn-lg")}";
            }

            if (!string.IsNullOrEmpty(CSSClass))
            {
                css = $"{css} {CSSClass}";
            }

            output.Attributes.Add("class", css);
            output.Attributes.Add("type", Type.ToString().ToLower());

            if (!string.IsNullOrEmpty(FaIcon))
            {
                output.PostContent.SetHtmlContent($"<b><i class='fa fa-{FaIcon}'></i></b>");
            }

            base.Process(context, output);
        }
    }

    public enum BtnSize
    {
        LG,
        MD,
        SM
    }

    public enum BtnBg
    {
        Primary,
        Success,
        Danger,
        Warning,
        Slate
    }

    public enum BtnType
    {
        Button,
        Submit
    }
}
