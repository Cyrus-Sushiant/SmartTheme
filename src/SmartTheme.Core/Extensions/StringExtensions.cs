using System;
using System.Text.Json;

namespace SmartTheme.Core.Extensions
{
    public static class StringExtensions
    {
        public static long? ConvertToInt64Nullable(this string input, bool removeSplitChars)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            if (removeSplitChars)
                input = input.Replace(",", string.Empty)
                                .Replace("،", string.Empty)
                                .Replace("-", string.Empty)
                                .Replace("_", string.Empty)
                                .Replace("/", string.Empty)
                                .Replace("\\", string.Empty)
                                .Replace(".", string.Empty);

            if (Int64.TryParse(input, out long num))
            {
                return num;
            }
            else
            {
                return null;
            }
        }

        public static long? ConvertToInt64Nullable(this string input)
        {
            return ConvertToInt64Nullable(input, true);
        }

        public static long ConvertToInt64(this string input, bool removeSplitChars)
        {
            if (string.IsNullOrEmpty(input))
                return 0;

            if (removeSplitChars)
                input = input.Replace(",", string.Empty)
                                .Replace("،", string.Empty)
                                .Replace("-", string.Empty)
                                .Replace("_", string.Empty)
                                .Replace("/", string.Empty)
                                .Replace("\\", string.Empty)
                                .Replace(".", string.Empty);

            if (!string.IsNullOrEmpty(input) && Int64.TryParse(input, out long num))
            {
                return num;
            }
            else
            {
                return 0;
            }
        }

        public static long ConvertToInt64(this string input)
        {
            return ConvertToInt64(input, true);
        }


        public static int? ConvertToInt32Nullable(this string input)
        {
            if (!string.IsNullOrEmpty(input) && Int32.TryParse(input, out int num))
            {
                return num;
            }
            else
            {
                return null;
            }
        }

        public static long ConvertToInt32(this string input)
        {
            if (!string.IsNullOrEmpty(input) && Int32.TryParse(input, out int num))
            {
                return num;
            }
            else
            {
                return 0;
            }
        }

        public static int? ConvertToByteNullable(this string input)
        {
            if (!string.IsNullOrEmpty(input) && Byte.TryParse(input, out byte num))
            {
                return num;
            }
            else
            {
                return null;
            }
        }

        public static string GetValueOrNull(this string input, bool trimValue = true)
        {
            if (!input.IsNullOrEmptyOrWhiteSpace())
            {
                return trimValue ? input.Trim() : input;
            }
            else
            {
                return null;
            }
        }

        public static string ToStringForSQL(this int? input)
        {
            if (input.HasValue)
            {
                return input.ToString();
            }
            else
            {
                return "NULL";
            }
        }

        public static string ToStringForSQL(this bool input)
        {
            if (input)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        public static string ToStringForSQL(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return $"N'{input}'";
            }
            else
            {
                return "NULL";
            }
        }

        public static string UrlEncode(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            else
            {
                return System.Web.HttpUtility.UrlEncode(input);
            }
        }

        public static string HtmlEncode(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            else
            {
                return System.Web.HttpUtility.HtmlEncode(input);
            }
        }

        public static string HtmlDecode(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            else
            {
                return System.Web.HttpUtility.HtmlDecode(input)
                    //custom
                    .Replace("&excl;", "!")
                    .Replace("&commat;", "@")
                    .Replace("&lpar;", "(")
                    .Replace("&rpar;", ")")
                    .Replace("&rpar;", ")")
                    .Replace("&comma;", ",")
                    .Replace("&quest;", "?")
                    .Replace("&period;", ".");
            }
        }

        /// <summary>
        /// Get SEO Url
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetSlug(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            else
            {
                var str = input.Trim('_')
                    //custom
                    .Replace("&excl;", "-")
                    .Replace("&commat;", "-")
                    .Replace("&lpar;", "-")
                    .Replace("&rpar;", "-")
                    .Replace("&rpar;", "-")
                    .Replace("&comma;", "-")
                    .Replace("&quest;", "-")
                    .Replace("&period;", "-")
                    .Replace("!", "-")
                    .Replace("@", "-")
                    .Replace("#", "-")
                    .Replace("$", "-")
                    .Replace("%", "-")
                    .Replace("^", "-")
                    .Replace("&", "-")
                    .Replace("*", "-")
                    .Replace("(", "-")
                    .Replace(")", "-")
                    .Replace("+", "-")
                    .Replace("=", "-")
                    .Replace("/", "-")
                    .Replace("~", "-")
                    .Replace("`", "-")
                    .Replace("[", "-")
                    .Replace("]", "-")
                    .Replace("{", "-")
                    .Replace("}", "-")
                    .Replace(@"\", "-")
                    .Replace("|", "-")
                    .Replace(".", "-")
                    .Replace("،", "-")
                    .Replace(",", "-")
                    .Replace(">", "-")
                    .Replace("<", "-")
                    .Replace("?", "-")
                    .Replace("/", "-")
                    .Replace(" ", "-")
                    .Replace("'", "-")
                    .Replace("\"", "-")

                    .Replace("_", "-");

                while (str.Contains("--"))
                {
                    str = str.Replace("--", "-");
                }

                return str.Trim('-').ToLower();
            }
        }

        public static bool IsValidJson(this string stringValue)
        {
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return false;
            }

            var value = stringValue.Trim();

            if ((value.StartsWith("{") && value.EndsWith("}")) || //For object
                (value.StartsWith("[") && value.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JsonDocument.Parse(value);
                    return true;
                }
                catch (JsonException)
                {
                    return false;
                }
            }

            return false;
        }

        public static bool IsNullOrEmpty(this string input, bool trimValue = true)
        {
            if (string.IsNullOrEmpty(input))
            {
                return true;
            }

            return trimValue ? string.IsNullOrEmpty(input.Trim()) : string.IsNullOrEmpty(input);
        }

        public static bool IsNullOrEmptyOrWhiteSpace(this string input, bool trimValue = true)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                return true;
            }

            return trimValue ? (string.IsNullOrEmpty(input.Trim()) || string.IsNullOrWhiteSpace(input.Trim())) : (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input));
        }
    }
}
