using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SmartTheme.Core.Tools
{
    public struct DateTimeTools
    {
        private readonly static string[] persianMonthNames = new string[12]
        {
            "فروردین",
            "اردیبهشت",
            "خرداد",
            "تیر",
            "مرداد",
            "شهریور",
            "مهر",
            "آبان",
            "آذر",
            "دی",
            "بهمن",
            "اسفند",
        };

        public static DateTime IranStandardTimeNow
        {
            get { return TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("UTC"), TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time")); }
        }

        public static DateTime ConvertUtcTimeToIranStandardTime(DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById("UTC"), TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time"));
        }

        public static DateTime ConvertFromUtcTime(DateTime dateTime, string timeZoneById)
        {
            if (timeZoneById == "UTC")
            {
                return dateTime;
            }
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.FindSystemTimeZoneById(timeZoneById));
        }

        public static TimeSpan ConvertToUtcTime(TimeSpan time, string timeZoneById)
        {
            if (timeZoneById == "UTC")
            {
                return time;
            }
            var irDT = IranStandardTimeNow;
            DateTime dt = new DateTime(irDT.Year, irDT.Month, irDT.Day);
            dt = dt + time;
            return TimeZoneInfo.ConvertTimeToUtc(dt, TimeZoneInfo.FindSystemTimeZoneById(timeZoneById)).TimeOfDay;
        }

        #region GeorgianToPersian
        public static string GeorgianToPersian(DateTime dateTime, string sourceTimeZoneId, string destinationTimeZoneId, ShowMode showMode)
        {
            DateTime dt;
            if (sourceTimeZoneId != destinationTimeZoneId)
            {
                dt = TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(sourceTimeZoneId), TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
            }
            else
            {
                dt = dateTime;
            }
            return GeorgianToPersian(dt, showMode);
        }
        public static string GeorgianToPersian(DateTime? dateTime, string sourceTimeZoneId, string destinationTimeZoneId, ShowMode showMode, string defaultValue)
        {
            if (dateTime.HasValue)
                return GeorgianToPersian(dateTime.Value, sourceTimeZoneId, destinationTimeZoneId, showMode);
            else
                return defaultValue;
        }
        public static string GeorgianUtcToPersian(DateTime dateTime, ShowMode showMode)
        {
            return GeorgianToPersian(dateTime, TimeZoneId.UTC, TimeZoneId.IranStandardTime, showMode);
        }
        public static string GeorgianUtcToPersian(DateTime dateTime, ShowMode showMode, string defaultValue)
        {
            try
            {
                return GeorgianUtcToPersian(dateTime, showMode);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        public static string GeorgianUtcToPersian(DateTime? dateTime, ShowMode showMode, string defaultValue)
        {
            try
            {
                return GeorgianUtcToPersian(dateTime, showMode);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        public static string GeorgianUtcToPersian(DateTime? dateTime, ShowMode showMode)
        {
            if (!dateTime.HasValue)
            {
                return null;
            }
            return GeorgianToPersian(dateTime.Value, TimeZoneId.UTC, TimeZoneId.IranStandardTime, showMode);
        }
        public static string GeorgianUtcToPersian(DateTime dateTime, string destinationTimeZoneId, ShowMode showMode)
        {
            return GeorgianToPersian(dateTime, TimeZoneId.UTC, destinationTimeZoneId, showMode);
        }
        public static string GeorgianUtcToPersian(DateTime? dateTime, string destinationTimeZoneId, ShowMode showMode)
        {
            if (dateTime.HasValue)
                return GeorgianToPersian(dateTime.Value, TimeZoneId.UTC, destinationTimeZoneId, showMode);
            else
                return null;
        }
        public static string GeorgianUtcToPersian(DateTime? dateTime, string destinationTimeZoneId, ShowMode showMode, string defaultValue)
        {
            if (dateTime.HasValue)
                return GeorgianToPersian(dateTime.Value, TimeZoneId.UTC, destinationTimeZoneId, showMode);
            else
                return defaultValue;
        }
        public static string TryGeorgianToPersian(DateTime dateTime, ShowMode showMode)
        {
            try
            {
                return GeorgianToPersian(dateTime, showMode);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string GeorgianToPersian(DateTime dateTime, ShowMode showMode)
        {
            PersianCalendar p = new PersianCalendar();
            int year = p.GetYear(dateTime);
            int month = p.GetMonth(dateTime);
            int day = p.GetDayOfMonth(dateTime);
            //int hour = p.GetHour(dateTime);
            //int minute = p.GetMinute(dateTime);

            switch (showMode)
            {
                case ShowMode.OnlyYear:
                    return year.ToString();
                case ShowMode.OnlyDate:
                    return $"{year.ToString()}/{month.ToString().PadLeft(2, '0')}/{day.ToString().PadLeft(2, '0')}";
                case ShowMode.OnlyTime:
                    return dateTime.ToString("HH:mm");
                case ShowMode.YearAndMonthName:
                    return $"{persianMonthNames[(month - 1)]} {year.ToString()}";
                case ShowMode.DateWithMonthName:
                    return $"{day.ToString()} {persianMonthNames[(month - 1)]} {year.ToString()}";
                case ShowMode.DateWithMonthNameAndDayOfWeekName:
                    return $"{PersianDayOfWeek(p.GetDayOfWeek(dateTime))} {day.ToString()} {persianMonthNames[(month - 1)]} {year.ToString()}";
                case ShowMode.DateWithMonthNameAndTimeAndMode24Hours:
                    return $"{day.ToString()} {persianMonthNames[(month - 1)]} {year.ToString()} ساعت {dateTime.ToString("HH:mm")}";
                case ShowMode.DateWithMonthNameAndTimeAndMode12Hours:
                    return $"{day.ToString()} {persianMonthNames[(month - 1)]} {year.ToString()} ساعت {dateTime.ToString("t")}";
                default:
                    return $"{year.ToString()} / {month.ToString().PadLeft(2, '0')} / {day.ToString().PadLeft(2, '0')}";
            }
        }

        public static string PersianDayOfWeek(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "یکشنبه";
                case DayOfWeek.Monday:
                    return "دوشنبه";
                case DayOfWeek.Tuesday:
                    return "سه شنبه";
                case DayOfWeek.Wednesday:
                    return "چهار شنبه";
                case DayOfWeek.Thursday:
                    return "پنج شنبه";
                case DayOfWeek.Friday:
                    return "جمعه";
                case DayOfWeek.Saturday:
                    return "شنبه";
                default:
                    return null;
            }
        }


        #endregion


        //Persian To Gorgian
        #region PersianToGorgian
        public static DateTime? ParsePersianToGorgianWithTime(string dateTime)
        {
            if (!string.IsNullOrEmpty(dateTime) && !string.IsNullOrEmpty(dateTime.Trim()) && (dateTime.Trim() != "----/--/-- --:--:--" || dateTime.Trim() != "--:--:-- --/--/----"))
            {
                int hour = 0;
                int minute = 0;
                int second = 0;
                string[] dateTimeArray = dateTime.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string date = dateTimeArray.Count() == 2 && dateTimeArray[1].Contains("/") ? dateTimeArray[1] : dateTimeArray[0];
                if (dateTimeArray.Count() == 2 && !string.IsNullOrEmpty(dateTimeArray[1]) && !string.IsNullOrEmpty(dateTimeArray[1].Trim()) && dateTimeArray[1].Trim() != "--:--")
                {
                    string[] splitTime = dateTimeArray[1].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    Int32.TryParse(splitTime[0], out hour);
                    Int32.TryParse(splitTime[1], out minute);
                    Int32.TryParse(splitTime[2], out second);
                }

                try
                {
                    PersianCalendar p = new PersianCalendar();
                    string[] split = date.Trim().Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    if (Int32.Parse(split[0]) > 31)
                    {
                        int year = Int32.Parse(split[0]);
                        int month = Int32.Parse(split[1]);
                        int day = Int32.Parse(split[2]);
                        return (p.ToDateTime(year, month, day, hour, minute, second, 0));
                    }
                    else
                    {
                        int year = Int32.Parse(split[2]);
                        int month = Int32.Parse(split[1]);
                        int day = Int32.Parse(split[0]);
                        return (p.ToDateTime(year, month, day, hour, minute, second, 0));
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static DateTime? ParsePersianToGorgianWithTime(string date, string time)
        {
            if (!string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(date.Trim()) && (date.Trim() != "----/--/--" || date.Trim() != "--/--/----"))
            {
                int hour = 0;
                int minute = 0;
                if (!string.IsNullOrEmpty(time) && !string.IsNullOrEmpty(time.Trim()) && time.Trim() != "--:--")
                {
                    string[] splitTime = time.Split(new char[] { ':' });
                    hour = Int32.Parse(splitTime[0]);
                    minute = Int32.Parse(splitTime[1]);
                }

                try
                {
                    PersianCalendar p = new PersianCalendar();
                    string[] split = date.Split(new char[] { '/' });
                    if (Int32.Parse(split[0]) > 31)
                    {
                        int year = Int32.Parse(split[0]);
                        int month = Int32.Parse(split[1]);
                        int day = Int32.Parse(split[2]);
                        return (p.ToDateTime(year, month, day, hour, minute, 0, 0));
                    }
                    else
                    {
                        int year = Int32.Parse(split[2]);
                        int month = Int32.Parse(split[1]);
                        int day = Int32.Parse(split[0]);
                        return (p.ToDateTime(year, month, day, hour, minute, 0, 0));
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static bool TryParsePersianToGorgian(string date, out DateTime dateTime)
        {
            if (!string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(date.Trim()) && (date.Trim() != "----/--/--" || date.Trim() != "--/--/----"))
            {
                try
                {
                    PersianCalendar p = new PersianCalendar();
                    string[] split = date.Split(new char[] { '/', '-' });
                    if (Int32.Parse(split[0]) > 31)
                    {
                        int year = Int32.Parse(split[0]);
                        int month = Int32.Parse(split[1]);
                        int day = Int32.Parse(split[2]);
                        dateTime = p.ToDateTime(year, month, day, 0, 0, 0, 0);
                    }
                    else
                    {
                        int year = Int32.Parse(split[2]);
                        int month = Int32.Parse(split[1]);
                        int day = Int32.Parse(split[0]);
                        dateTime = p.ToDateTime(year, month, day, 0, 0, 0, 0);
                    }
                    return true;
                }
                catch (Exception)
                {
                    dateTime = DateTime.UtcNow;
                    return false;
                }
            }
            else
            {
                dateTime = DateTime.UtcNow;
                return false;
            }
        }

        public static DateTime? ParsePersianToGorgianUtc(string date, string sourceTimeZoneId)
        {
            if (!string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(date.Trim()) && (date.Trim() != "----/--/--" || date.Trim() != "--/--/----"))
            {
                try
                {
                    PersianCalendar p = new PersianCalendar();
                    string[] split = date.Split(new char[] { '/', '-' });
                    if (Int32.Parse(split[0]) > 31)
                    {
                        int year = Int32.Parse(split[0]);
                        int month = Int32.Parse(split[1]);
                        int day = Int32.Parse(split[2]);
                        return TimeZoneInfo.ConvertTime(p.ToDateTime(year, month, day, 0, 0, 0, 0), TimeZoneInfo.FindSystemTimeZoneById(sourceTimeZoneId), TimeZoneInfo.FindSystemTimeZoneById("UTC"));
                    }
                    else
                    {
                        int year = Int32.Parse(split[2]);
                        int month = Int32.Parse(split[1]);
                        int day = Int32.Parse(split[0]);
                        return TimeZoneInfo.ConvertTime(p.ToDateTime(year, month, day, 0, 0, 0, 0), TimeZoneInfo.FindSystemTimeZoneById(sourceTimeZoneId), TimeZoneInfo.FindSystemTimeZoneById("UTC"));
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }


        public static DateTime? ParsePersianToGorgianUtc(string date)
        {
            DateTime? dt = ParsePersianToGorgian(date);

            if (dt.HasValue)
            {
                return TimeZoneInfo.ConvertTime(dt.Value, TimeZoneInfo.FindSystemTimeZoneById("UTC"), TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time"));
            }
            else
            {
                return null;
            }
        }

        public static DateTime? ParsePersianToGorgianDateTimeUtc(string date)
        {
            DateTime? dt = ParsePersianToGorgianDateTime(date);

            if (dt.HasValue)
            {
                return TimeZoneInfo.ConvertTimeToUtc(dt.Value, TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time"));
            }
            else
            {
                return null;
            }
        }

        public static DateTime? ParsePersianToGorgian(string date)
        {
            if (!string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(date.Trim()) && (date.Trim() != "----/--/--" || date.Trim() != "--/--/----"))
            {
                try
                {
                    PersianCalendar p = new PersianCalendar();
                    string[] split = date.Split(new char[] { '/', '-' });
                    if (Int32.Parse(split[0]) > 31)
                    {
                        int year = Int32.Parse(split[0]);
                        int month = Int32.Parse(split[1]);
                        int day = Int32.Parse(split[2]);
                        return (p.ToDateTime(year, month, day, 0, 0, 0, 0));
                    }
                    else
                    {
                        int year = Int32.Parse(split[2]);
                        int month = Int32.Parse(split[1]);
                        int day = Int32.Parse(split[0]);
                        return (p.ToDateTime(year, month, day, 0, 0, 0, 0));
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static DateTime? ParsePersianToGorgian(string date, TimeSpan time)
        {
            var d = ParsePersianToGorgian(date);
            if (d == null)
            {
                return null;
            }

            return new DateTime(d.Value.Year, d.Value.Month, d.Value.Day, time.Hours, time.Minutes, time.Seconds);
        }
        public static DateTime? ParsePersianToGorgianDateTime(string dateTime)
        {
            if (!string.IsNullOrEmpty(dateTime) && !string.IsNullOrEmpty(dateTime.Trim()) && (dateTime.Trim() != "----/--/--" || dateTime.Trim() != "--/--/----"))
            {
                try
                {
                    if (dateTime.Contains(":"))
                    {
                        dateTime = dateTime.Replace(" / ", "/");
                        var dateSplit = dateTime.Split(new char[] { ' ' });
                        PersianCalendar p = new PersianCalendar();
                        string[] dSplit = (dateSplit[0].Contains(":") ? dateSplit[1] : dateSplit[0]).Split(new char[] { '/', '-', ':' });
                        string[] tSplit = (dateSplit[0].Contains(":") ? dateSplit[0] : dateSplit[1]).Split(new char[] { '/', '-', ':' });
                        if (Int32.Parse(dSplit[0]) > 31)
                        {
                            int year = Int32.Parse(dSplit[0]);
                            int month = Int32.Parse(dSplit[1]);
                            int day = Int32.Parse(dSplit[2]);
                            int hour = Int32.Parse(tSplit[0]);
                            int minute = Int32.Parse(tSplit[1]);
                            int second = tSplit.Length > 2 ? Int32.Parse(tSplit[2]) : 0;
                            return (p.ToDateTime(year, month, day, hour, minute, second, 0));
                        }
                        else
                        {
                            int year = Int32.Parse(dSplit[2]);
                            int month = Int32.Parse(dSplit[1]);
                            int day = Int32.Parse(dSplit[0]);
                            int hour = Int32.Parse(tSplit[2]);
                            int minute = Int32.Parse(tSplit[1]);
                            int second = tSplit.Length > 2 ? Int32.Parse(tSplit[0]) : 0;
                            return (p.ToDateTime(year, month, day, hour, minute, second, 0));
                        }
                    }
                    else
                    {
                        PersianCalendar p = new PersianCalendar();
                        string[] split = dateTime.Split(new char[] { '/', '-' });
                        if (Int32.Parse(split[0]) > 31)
                        {
                            int year = Int32.Parse(split[0]);
                            int month = Int32.Parse(split[1]);
                            int day = Int32.Parse(split[2]);
                            return (p.ToDateTime(year, month, day, 0, 0, 0, 0));
                        }
                        else
                        {
                            int year = Int32.Parse(split[2]);
                            int month = Int32.Parse(split[1]);
                            int day = Int32.Parse(split[0]);
                            return (p.ToDateTime(year, month, day, 0, 0, 0, 0));
                        }
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static bool ValidatePersianToGorgian(string date)
        {
            return ParsePersianToGorgian(date).HasValue;
        }
        #endregion

        public static Dictionary<DayOfWeek, string> ListOfPersianDayOfWeek()
        {
            Dictionary<DayOfWeek, string> dicWeek = new Dictionary<DayOfWeek, string>();
            dicWeek.Add(DayOfWeek.Saturday, "شنبه");
            dicWeek.Add(DayOfWeek.Sunday, "یکشنبه");
            dicWeek.Add(DayOfWeek.Monday, "دوشنبه");
            dicWeek.Add(DayOfWeek.Tuesday, "سه شنبه");
            dicWeek.Add(DayOfWeek.Wednesday, "چهار شنبه");
            dicWeek.Add(DayOfWeek.Thursday, "پنج شنبه");
            dicWeek.Add(DayOfWeek.Friday, "جمعه");
            return dicWeek;
        }


        //ParseTime
        public static TimeSpan? ParseTime(string time)
        {
            if (!string.IsNullOrEmpty(time) && !string.IsNullOrEmpty(time.Trim()) && time.Trim() != "--:--")
            {
                int hour = 0;
                int minute = 0;
                if (!string.IsNullOrEmpty(time) && !string.IsNullOrEmpty(time.Trim()) && time.Trim() != "--:--")
                {
                    string[] splitTime = time.Split(new char[] { ':' });
                    hour = Int32.Parse(splitTime[0]);
                    minute = Int32.Parse(splitTime[1]);
                }

                try
                {
                    return new TimeSpan(hour, minute, 0);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static string TryGetDateForDatepicker(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return TryGetDateForDatepicker(dateTime.Value);
            }
            else
            {
                return null;
            }
        }

        public static string TryGetDateForDatepicker(DateTime dateTime)
        {
            try
            {
                PersianCalendar pc = new PersianCalendar();
                //[1392, 12, 1, 11, 11]
                return $"[{pc.GetYear(dateTime)}, {pc.GetMonth(dateTime)}, {pc.GetDayOfMonth(dateTime)}, {pc.GetHour(dateTime)}, {pc.GetMinute(dateTime)}, {pc.GetSecond(dateTime)}]";
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string TryGetDateForDatepicker(string dateTimeStr)
        {
            try
            {
                string[] strSplitDate = dateTimeStr.Split('/');
                //[1395, 01, 01]
                return $"[{strSplitDate[0]}, {strSplitDate[1]}, {strSplitDate[2]}]";
            }
            catch (Exception)
            {
                try
                {
                    string[] strSplitDate = GeorgianToPersian(DateTimeTools.IranStandardTimeNow, ShowMode.OnlyDate).Split('/');
                    return $"[{strSplitDate[0]}, {strSplitDate[1]}, {strSplitDate[2]}]";
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }
    }

    public enum ShowMode
    {
        OnlyYear,
        OnlyDate,
        YearAndMonthName,
        DateWithMonthName,
        DateWithMonthNameAndDayOfWeekName,
        DateWithMonthNameAndTimeAndMode24Hours,
        DateWithMonthNameAndTimeAndMode12Hours,
        OnlyTime
    }

    public struct TimeZoneId
    {
        public static string UTC => "UTC";
        public static string IranStandardTime => "Iran Standard Time";
    }
}
