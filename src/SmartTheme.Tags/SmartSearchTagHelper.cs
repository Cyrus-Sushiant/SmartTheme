using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace SmartTheme.Tags
{
    [HtmlTargetElement("smart-search")]
    public partial class SmartSearchTagHelper : TagHelper
    {
        private const string RedirectToAttributeName = "asp-redirect-to";
        private const string PrintUrlAttributeName = "asp-print-url";
        private const string PrintAttributeName = "asp-print-button";
        private const string SMSAttributeName = "asp-SMS-button";
        private const string SMSUrlAttributeName = "asp-SMS-url";
        [HtmlAttributeName(RedirectToAttributeName)]
        public string RedirectTo { get; set; }
        [HtmlAttributeName(PrintAttributeName)]
        public bool PrintButton { get; set; }
        [HtmlAttributeName(SMSAttributeName)]
        public bool SMSButton { get; set; }
        [HtmlAttributeName(PrintUrlAttributeName)]
        public string PrintUrl { get; set; }

        [HtmlAttributeName(SMSUrlAttributeName)]
        public string SMSUrl { get; set; }

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
            output.Attributes.Add("class", "panel panel-flat");

            output.PreContent.AppendHtml(@"
            <div class='panel-heading'>
                <h5 class='panel-title'>جستجو</h5>
                <div class='heading-elements'>
                    <ul class='icons-list'>
                        <li><a data-action='collapse'></a></li>
                    </ul>
                </div>
            </div><div class='panel-body'>");

            var builder = new HtmlContentBuilder();
            var printStr = "";
            if (PrintButton)
            {
                printStr = $@"&nbsp;<button type = 'button' class='btn btn-info btn-labeled' onclick='javascript: GoToReport(""{PrintUrl}"", ""#frm-search"")'><b><i class='fa fa-print'></i></b>چاپ و پیش نمایش گزارش</button>";
            }
            var smsStr = "";
            if (SMSButton)
            {
                smsStr = $@"&nbsp;<button class='btn btn-danger btn-sm ph25 btn-labeled' type='button' onclick='javascript: GoToSendSMS(""/SendSMS/Preview"", ""#frm-search"",""{SMSUrl}"")'><b><i class='fa fa-sms'></i></b>ارسال پیامک</button>";
            }

            output.PostContent.AppendHtml($@"</div><div class='panel-footer'>
            <div class='text-right'><button type = 'submit' class='btn btn-primary btn-labeled'><b><i class='fa fa-search'></i></b>جستجو</button>{printStr}{smsStr}&nbsp;<button type='button' class='btn bg-slate btn-labeled' onclick='RedirectTo(""{RedirectTo}"")'><b><i class='fa fa-backspace'></i></b>انصراف</button></div></div>");

            base.Process(context, output);
        }
    }
}
