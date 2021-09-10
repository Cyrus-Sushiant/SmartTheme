using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartTheme.Core.Tools;
using System;

namespace SmartTheme.Tags
{
    [HtmlTargetElement("smart-js-persian-datepicker", Attributes = ForAttributeName)]
    public partial class SmartPersianDatepickerJsTagHelper : SmartInlineScriptTagHelper
    {
        private const string ForAttributeName = "asp-for";
        private const string SetDateTimeNowAttributeName = "asp-set-datetime-now";
        private const string SetDateTimeAttributeName = "asp-set-datetime";
        private const string DateTimeAttributeName = "asp-datetime";
        private const string EnabledTimePickerAttributeName = "asp-enable-time-picker";

        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName(SetDateTimeNowAttributeName)]
        public bool SetDateTimeNow { get; set; }

        [HtmlAttributeName(SetDateTimeAttributeName)]
        public bool SetDateTime { get; set; } = true;

        [HtmlAttributeName(EnabledTimePickerAttributeName)]
        public bool EnabledTimePicker { get; set; }

        [HtmlAttributeName(DateTimeAttributeName)]
        public dynamic UserDateTime { get; set; }

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

            string val = ".val('')";

            if (SetDateTimeNow)
            {
                val = $".pDatepicker('setDate', {DateTimeTools.TryGetDateForDatepicker(DateTimeTools.IranStandardTimeNow)})";
            }
            else if (SetDateTime)
            {
                if (For.Metadata.ModelType == typeof(string))
                {
                    if (For.Model != null && !string.IsNullOrEmpty(For.Model.ToString()))
                    {
                        val = $".pDatepicker('setDate', {DateTimeTools.TryGetDateForDatepicker(For.Model.ToString())})";
                    }
                }
                else if (For.Metadata.ModelType == typeof(DateTime))
                {
                    if (For.Model != null)
                    {
                        val = $".pDatepicker('setDate', {DateTimeTools.TryGetDateForDatepicker((DateTime)For.Model)})";
                    }
                }
            }
            else if (UserDateTime != null)
            {
                if (UserDateTime is string)
                {
                    val = $".pDatepicker('setDate', {DateTimeTools.TryGetDateForDatepicker((string)UserDateTime)})";
                }
                else if (UserDateTime is DateTime)
                {
                    val = $".pDatepicker('setDate', {DateTimeTools.TryGetDateForDatepicker((DateTime)UserDateTime)})";
                }
            }

            string timePicker = "timePicker: { enabled: false }, format: 'YYYY/MM/DD'";
            if (EnabledTimePicker)
            {
                timePicker = "timePicker: { enabled: true }, format: 'YYYY/MM/DD HH:mm:ss'";
            }

            output.PreContent.AppendHtml($@"$(document).ready(function () {{
            $('#{For.Name}').persianDatepicker({{
                {timePicker}
            }}){val};
        }});");

            base.Process(context, output);
        }
    }
}
