using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace SmartTheme.Core.Extensions
{
    /// <summary>
    /// Converts English digits of a given number to their equivalent Persian digits.
    /// </summary>
    public static class PersianNumbersExtentions
    {
        /// <summary>
        /// Converts English digits of a given number to their equivalent Persian digits.
        /// </summary>
        public static string ToPersianNumbers(this int number, string format = "")
        {
            return ToPersianNumbers(!string.IsNullOrEmpty(format) ?
                    number.ToString(format) : number.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Converts English digits of a given number to their equivalent Persian digits.
        /// </summary>
        public static string ToPersianNumbers(this long number, string format = "")
        {
            return ToPersianNumbers(!string.IsNullOrEmpty(format) ?
                    number.ToString(format) : number.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Converts English digits of a given number to their equivalent Persian digits.
        /// </summary>
        public static string ToPersianNumbers(this int? number, string format = "")
        {
            if (!number.HasValue) number = 0;
            return ToPersianNumbers(!string.IsNullOrEmpty(format) ?
                    number.Value.ToString(format) : number.Value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Converts English digits of a given number to their equivalent Persian digits.
        /// </summary>
        public static string ToPersianNumbers(this long? number, string format = "")
        {
            if (!number.HasValue) number = 0;
            return ToPersianNumbers(!string.IsNullOrEmpty(format) ?
                number.Value.ToString(format) : number.Value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Converts English digits of a given string to their equivalent Persian digits.
        /// </summary>
        /// <param name="data">English number</param>
        /// <returns></returns>
        public static string ToPersianNumbers(this string data)
        {
            if (string.IsNullOrWhiteSpace(data)) return string.Empty;
            return
               data
                .ToEnglishNumbers()
                .Replace("0", "\u06F0")
                .Replace("1", "\u06F1")
                .Replace("2", "\u06F2")
                .Replace("3", "\u06F3")
                .Replace("4", "\u06F4")
                .Replace("5", "\u06F5")
                .Replace("6", "\u06F6")
                .Replace("7", "\u06F7")
                .Replace("8", "\u06F8")
                .Replace("9", "\u06F9")
                .Replace(".", ",");
        }

        /// <summary>
        /// Converts Persian and Arabic digits of a given string to their equivalent English digits.
        /// </summary>
        /// <param name="data">Persian number</param>
        /// <returns></returns>
        public static string ToEnglishNumbers(this string data)
        {
            if (string.IsNullOrWhiteSpace(data)) return string.Empty;
            return
               data.Replace("\u0660", "0") //٠
                   .Replace("\u06F0", "0") //۰
                   .Replace("\u0661", "1") //١
                   .Replace("\u06F1", "1") //۱
                   .Replace("\u0662", "2") //٢
                   .Replace("\u06F2", "2") //۲
                   .Replace("\u0663", "3") //٣
                   .Replace("\u06F3", "3") //۳
                   .Replace("\u0664", "4") //٤
                   .Replace("\u06F4", "4") //۴
                   .Replace("\u0665", "5") //٥
                   .Replace("\u06F5", "5") //۵
                   .Replace("\u0666", "6") //٦
                   .Replace("\u06F6", "6") //۶
                   .Replace("\u0667", "7") //٧
                   .Replace("\u06F7", "7") //۷
                   .Replace("\u0668", "8") //٨
                   .Replace("\u06F8", "8") //۸
                   .Replace("\u0669", "9") //٩
                   .Replace("\u06F9", "9") //۹
                   ;
        }

        /// <summary>
        /// تعیین معتبر بودن کد ملی
        /// </summary>
        /// <param name="nationalCode">کد ملی وارد شده</param>
        /// <returns>
        /// در صورتی که کد ملی صحیح باشد خروجی <c>true</c> و در صورتی که کد ملی اشتباه باشد خروجی <c>false</c> خواهد بود
        /// </returns>
        /// <exception cref="System.Exception"></exception>
        public static bool IsValidNationalCode(this string nationalCode)
        {
            //در صورتی که کد ملی وارد شده تهی باشد

            if (String.IsNullOrEmpty(nationalCode))
                return false;
            //throw new Exception("لطفا کد ملی را صحیح وارد نمایید");


            //در صورتی که کد ملی وارد شده طولش کمتر از 10 رقم باشد
            if (nationalCode.Length != 10)
                return false;
            //throw new Exception("طول کد ملی باید ده کاراکتر باشد");

            //در صورتی که کد ملی ده رقم عددی نباشد
            var regex = new Regex(@"\d{10}");
            if (!regex.IsMatch(nationalCode))
                return false;
            //throw new Exception("کد ملی تشکیل شده از ده رقم عددی می‌باشد؛ لطفا کد ملی را صحیح وارد نمایید");

            //در صورتی که رقم‌های کد ملی وارد شده یکسان باشد
            var allDigitEqual = new[] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
            if (allDigitEqual.Contains(nationalCode)) return false;


            //عملیات شرح داده شده در بالا
            var chArray = nationalCode.ToCharArray();
            var num0 = Convert.ToInt32(chArray[0].ToString()) * 10;
            var num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
            var num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
            var num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
            var num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
            var num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
            var num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
            var num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
            var num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
            var a = Convert.ToInt32(chArray[9].ToString());

            var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
            var c = b % 11;

            return (((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)));
        }

        public static bool IsValidNationalCode(this long nationalCode)
        {
            return nationalCode.ToString().PadLeft(10, '0').IsValidNationalCode();
        }

        public static bool IsValidNationalCode(this long? nationalCode)
        {
            return nationalCode.HasValue && nationalCode.Value.ToString().PadLeft(10, '0').IsValidNationalCode();
        }
    }
}
