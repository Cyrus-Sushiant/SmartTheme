using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartTheme.Tags
{
    public partial class SmartInputTagHelper : InputTagHelper
    {
        private const string SkinAttributeName = "asp-skin";


        [HtmlAttributeName(SkinAttributeName)]
        public SmartEditorSkins Skin { get; set; } = SmartEditorSkins.Default;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="generator">HTML generator</param>
        public SmartInputTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }
    }



    public enum SmartEditorSkins
    {
        Default
    }
}
