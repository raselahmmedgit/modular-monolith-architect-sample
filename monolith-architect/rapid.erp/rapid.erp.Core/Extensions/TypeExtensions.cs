using Microsoft.AspNetCore.Http;
using rapid.erp.Core.Helpers;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace rapid.erp.Core.Extensions
{

    public static partial class TypeExtensions
    {
        #region Date

        /// <summary>
        /// Format : dd/MM/yyyy HH:mm:ss as string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToFormatedDateTimeString(this object obj)
        {
            if (obj != null)
                return ToFormatedDateTimeString(Convert.ToDateTime(obj));
            return null;
        }
        /// <summary>
        /// Format : dd/MM/yyyy HH:mm:ss as string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToFormatedDateTimeString(this DateTime obj)
        {
            return obj.ToString("dd/MM/yyyy HH:mm:ss");
        }

        /// <summary>
        /// Format: dd/MM/yyyy hh:mm:ss tt as string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToFormatedDateTimeWithAmPmString(this object obj)
        {
            if (obj != null)
                return ToFormatedDateTimeWithAmPmString(Convert.ToDateTime(obj));
            return null;
        }
        /// <summary>
        /// Format: dd/MM/yyyy hh:mm:ss tt as string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToFormatedDateTimeWithAmPmString(this DateTime obj)
        {
            //return obj.ToString("MM/dd/yyyy HH:mm:ss tt");
            return obj.ToString("dd/MM/yyyy hh:mm:ss tt");
        }
        public static string ToFormatedDateTimeWithAmPmString(this DateTime obj, string separator, bool nospace)
        {
            //return obj.ToString("MM/dd/yyyy HH:mm:ss tt");
            return obj.ToString($"dd{separator}MM{separator}yyyy{separator}hh{separator}mm{separator}ss{separator}tt");
        }
        /// <summary>
        /// Format: dd/MM/yyyy hh:mm:ss tt as string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToFormatedDateTimeWithAmPmString(this DateTime? obj)
        {
            if (obj == null)
                return "";
            //return obj.ToString("MM/dd/yyyy HH:mm:ss tt");
            return obj.Value.ToString("dd/MM/yyyy hh:mm:ss tt");
        }

        /// <summary>
        /// Format: dd MMMM, yyyy hh:mm:ss tt
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToFormatedScgDateTimeWithAmPmString(this object obj)
        {
            if (obj != null)
                return ToFormatedScgDateTimeWithAmPmString(Convert.ToDateTime(obj));
            return null;
        }
        /// <summary>
        /// Format: dd MMMM, yyyy hh:mm:ss tt
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToFormatedScgDateTimeWithAmPmString(this DateTime obj)
        {
            //return obj.ToString("MM/dd/yyyy HH:mm:ss tt");
            return obj.ToString("dd MMMM, yyyy hh:mm:ss tt");
        }

        public static string ToFormatedTimeString(this object obj)
        {
            if (obj != null)
                return ToFormatedTimeString(Convert.ToDateTime(obj));
            return null;
        }
        public static string ToFormatedTimeWithAmPmString(this object obj)
        {
            if (obj != null)
                return ToFormatedTimeWithAmPmString(Convert.ToDateTime(obj));
            return null;
        }
        public static string ToDbFormatedDateString(this object obj)
        {
            if (obj != null)
            {
                var date = Convert.ToDateTime(obj);
                return date.ToString("dd-MMM-yyyy");
            }
            return null;
        }
        public static string ToDbFormatedDateTimeString(this object obj)
        {
            if (obj != null)
            {
                var date = Convert.ToDateTime(obj);
                return date.ToString("dd-MMM-yyyy HH:mm:ss");
            }
            return null;
        }
        /// <summary>
        /// Displaay date as format : dd/MM/yyyy
        /// </summary>
        /// <param name="obj">this DateTime</param>
        /// <returns></returns>
        public static string ToFormatedDateString(this DateTime obj)
        {
            return obj.ToString("dd/MM/yyyy");
        }
        /// <summary>
        /// To Formated Date as Day, Month Name Year string (Format: dd MMM yyyy)
        /// </summary>
        /// <param name="obj">this DateTime?</param>
        /// <returns></returns>
        public static string ToDayMonthNameYearString(this DateTime? obj)
        {
            if (obj == null)
                return "";
            return Convert.ToDateTime(obj).ToString("dd MMM yyyy");
        }

        /// <summary>
        /// Format : MM/dd/yyyy hh:mm tt
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToFormatedShortDateTimeString(this DateTime? obj)
        {
            if (obj == null)
                return "";
            //return obj.ToString("MM/dd/yyyy HH:mm:ss tt");
            return obj.Value.ToString("dd/MM/yyyy hh:mm tt");
        }
        /// <summary>
        /// Format : MM/dd/yyyy hh:mm tt
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToFormatedShortDateTimeString(this DateTime obj)
        {
            return obj.ToString("dd/MM/yyyy hh:mm tt");
        }

        public static string ToFormatedTimeString(this DateTime obj)
        {
            return obj.ToString("HH:mm:ss");
        }
        public static string ToFormatedTimeWithAmPmString(this DateTime obj)
        {
            //return obj.ToString("HH:mm:ss tt");
            return obj.ToString("hh:mm:ss tt");
        }
        public static string ToFormatedShortTimeWithAmPmString(this DateTime obj)
        {
            //return obj.ToString("HH:mm:ss tt");
            return obj.ToString("hh:mm tt");
        }
        /// <summary>
        /// Perse date string to Datetime
        /// Format: dd/MMM/yyyy
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string IsValidDateThenParseString(this string dateTime)
        {
            if (!string.IsNullOrEmpty(dateTime))
            {

                if (DateTime.TryParseExact(dateTime, "MM/dd/yyyy", null, DateTimeStyles.None, out DateTime outDateTimeTime) == true)
                {
                    return dateTime;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
        public static DateTime? IsValidDateThenParseDate(this string dateTime)
        {
            if (DateTime.TryParse(dateTime, out DateTime outDateTimeTime))
            {
                return DateTime.Parse(outDateTimeTime.ToString("dd/MMM/yyyy"));
            }
            return null;
        }
        public static string IsValidDateThenParseDateToString(this DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return dateTime.Value.ToString("dd/MMM/yyyy");
            }
            return null;
        }
        public static DateTime? IsValidDateThenParseDateTime(this DateTime? dateTime, string time)
        {
            if (dateTime.HasValue)
            {
                string format = "dd/MMM/yyyy";
                var datetime = dateTime.Value.ToString(format) + " " + time;
                if (DateTime.TryParse(datetime, out DateTime outDateTimeTime))
                {
                    return DateTime.Parse(outDateTimeTime.ToString(format));
                }
            }
            return null;
        }
        public static DateTime? IsValidDateThenParseDateTime(this string dateTime)
        {
            return DateTime.TryParse(dateTime, out DateTime outDateTimeTime)
                ? (DateTime?)DateTime.Parse(outDateTimeTime.ToString("dd/MMM/yyyy hh:mm tt"))
                : null;
        }
        /// <summary>
        /// Display date as MMMM yyyy format. Example: January 2025. Input Type: this DateTime?
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToMonthFullNameYearString(this DateTime? dateTime)
        {
            if (dateTime != null)
            {
                if (DateTime.TryParse(dateTime.ToString(), out DateTime outDateTimeTime) == true)
                {
                    return outDateTimeTime.ToString("MMMM yyyy");
                }
                else
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }
         public static string ToMonthYearString(this DateTime? dateTime)
        {
            if (dateTime != null)
            {
                if (DateTime.TryParse(dateTime.ToString(), out DateTime outDateTimeTime) == true)
                {
                    return outDateTimeTime.ToString("MM/yyyy");
                }
                else
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }
        public static DateTime ToUtcDateTime(this DateTime utcDateTime)
        {
            utcDateTime = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Local);
            DateTime time = TimeZoneInfo.ConvertTimeToUtc(utcDateTime);
            return time;
        }
        public static TimeZoneInfo GetTimeZoneInfo(this string timeZoneId)
        {
            return TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        }
        public static string GetUtcTimeOffsetString(this string timeZoneId)
        {
            try
            {
                TimeZoneInfo utz = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                return utz.GetUtcOffset(DateTime.Now).ToString();
            }
            catch (TimeZoneNotFoundException)
            {
                return "";
            }
            catch (InvalidTimeZoneException)
            {
                return "";
            }
        }

        public static DateTime? ToUtcFromDesignatedTimeZone(this DateTime? designatedDateTime, string timeZoneId)
        {
            //DateTime cstTime = new DateTime();
            try
            {
                if (designatedDateTime == null)
                {
                    return null;
                }
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(designatedDateTime.Value, cstZone);
                Console.WriteLine("The date and time are {0} {1}.",
                    utcTime,
                    cstZone.IsDaylightSavingTime(utcTime) ?
                        cstZone.DaylightName : cstZone.StandardName);
                return utcTime;
            }
            catch (TimeZoneNotFoundException)
            {
                Console.WriteLine("The registry does not define the Central Standard Time zone.");
            }
            catch (InvalidTimeZoneException)
            {
                Console.WriteLine("Registry data on the Central Standard Time zone has been corrupted.");
            }
            return null;
        }

        public static DateTime ToUtcFromDesignatedTimeZone(this DateTime designatedDateTime, string timeZoneId)
        {
            //DateTime cstTime = new DateTime();
            try
            {
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(designatedDateTime, cstZone);
                Console.WriteLine("The date and time are {0} {1}.",
                    utcTime,
                    cstZone.IsDaylightSavingTime(utcTime) ?
                        cstZone.DaylightName : cstZone.StandardName);
                return utcTime;
            }
            catch (TimeZoneNotFoundException)
            {
                Console.WriteLine("The registry does not define the Central Standard Time zone.");
            }
            catch (InvalidTimeZoneException)
            {
                Console.WriteLine("Registry data on the Central Standard Time zone has been corrupted.");
            }
            return DateTime.UtcNow;

        }

        public static DateTime? ToDesignatedTimeZoneFromUtc(this DateTime? utcDateTime, string timeZoneId)
        {
            //DateTime cstTime = new DateTime();
            try
            {
                if (utcDateTime == null)
                {
                    return null;
                }
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime.Value, cstZone);
                Console.WriteLine("The date and time are {0} {1}.",
                    cstTime,
                    cstZone.IsDaylightSavingTime(cstTime) ?
                        cstZone.DaylightName : cstZone.StandardName);
                return cstTime;
            }
            catch (TimeZoneNotFoundException)
            {
                Console.WriteLine("The registry does not define the Central Standard Time zone.");
            }
            catch (InvalidTimeZoneException)
            {
                Console.WriteLine("Registry data on the Central Standard Time zone has been corrupted.");
            }
            return null;
        }

        public static DateTime ToDesignatedTimeZoneFromUtc(this DateTime utcDateTime, string timeZoneId)
        {
            //DateTime cstTime = new DateTime();
            try
            {
                utcDateTime = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                DateTime cstTime = TimeZoneInfo.ConvertTime(utcDateTime, cstZone);
                Console.WriteLine("The date and time are {0} {1}.",
                    cstTime,
                    cstZone.IsDaylightSavingTime(cstTime) ?
                        cstZone.DaylightName : cstZone.StandardName);
                return cstTime;
            }
            catch (TimeZoneNotFoundException)
            {
                Console.WriteLine("The registry does not define the Central Standard Time zone.");
            }
            catch (InvalidTimeZoneException)
            {
                Console.WriteLine("Registry data on the Central Standard Time zone has been corrupted.");
            }
            return DateTime.UtcNow;

        }

        public static DateTime ToDesignatedTimeZoneFromUtc(this DateTime utcDateTime, HttpContext context)
        {
            utcDateTime = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);
            var timeZoneId = context.User.Identity.GetDefaultAppTimeZoneId();
            //DateTime cstTime = new DateTime();
            try
            {
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, cstZone);
                Console.WriteLine("The date and time are {0} {1}.",
                    cstTime,
                    cstZone.IsDaylightSavingTime(cstTime) ?
                        cstZone.DaylightName : cstZone.StandardName);
                return cstTime;
            }
            catch (TimeZoneNotFoundException)
            {
                Console.WriteLine("The registry does not define the Central Standard Time zone.");
                throw;
            }
            catch (InvalidTimeZoneException)
            {
                Console.WriteLine("Registry data on the Central Standard Time zone has been corrupted.");
                throw;
            }
        }

        public static DateTime? ToDesignatedTimeZoneFromUtc(this DateTime? utcDateTime)
        {
            if (utcDateTime == null)
                return null;
            return ToDesignatedTimeZoneFromUtc(utcDateTime.GetValueOrDefault());
        }

        public static DateTime? ToUtcDateTimeFromDefaultTimeZoneOffset(this DateTime? localDateTime)
        {
            if (localDateTime == null)
                return null;
            return ToUtcDateTimeFromDefaultTimeZoneOffset(localDateTime.Value);
        }

        public static DateTime? ToUtcDateTimeFromTimeZone(this DateTime? localDateTime, string timeZoneId)
        {
            try
            {
                if (localDateTime == null) return null;
                TimeZoneInfo utz = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                DateTime dtUtc = TimeZoneInfo.ConvertTimeToUtc(localDateTime.Value, utz);
                return dtUtc;
            }
            catch (TimeZoneNotFoundException)
            {
                return null;
            }
            catch (InvalidTimeZoneException)
            {
                return null;
            }
        }

        public static DateTime ToUtcDateTimeFromTimeZone(this DateTime localDateTime, string timeZoneId)
        {
            try
            {
                TimeZoneInfo utz = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                DateTime dtUtc = TimeZoneInfo.ConvertTimeToUtc(localDateTime, utz);
                return dtUtc;
            }
            catch (TimeZoneNotFoundException)
            {
                Console.WriteLine("The registry does not define the " + timeZoneId + " zone.");
                return new DateTime();
            }
            catch (InvalidTimeZoneException)
            {
                Console.WriteLine("The registry does not define the " + timeZoneId + " zone.");
                return new DateTime();
            }

        }

        public static DateTime ToLocalDateTimeFromUtcOffset(this DateTime utcDateTime, string offset)
        {
            if (string.IsNullOrEmpty(offset))
                return utcDateTime;
            TimeSpan dd = TimeSpan.Parse(offset);
            var dateTime = utcDateTime.Add(dd);
            return dateTime;
        }
        public static DateTime? ToLocalDateTimeFromUtcOffset(this DateTime? utcDateTime, string offset)
        {
            if (string.IsNullOrEmpty(offset))
                return utcDateTime;
            return utcDateTime?.ToLocalDateTimeFromUtcOffset(offset);
        }
        public static DateTime? ToLocalDateTimeFromDefaultUtcOffset(this DateTime? utcDateTime, HttpContext context)
        {
            var offset = context?.User?.Identity.GetDefaultUtcTimeZoneOffset();
            return utcDateTime?.ToLocalDateTimeFromUtcOffset(offset);
        }
        public static DateTime ToLocalDateTimeFromDefaultUtcOffset(this DateTime utcDateTime, HttpContext context)
        {
            var offset = context?.User?.Identity.GetDefaultUtcTimeZoneOffset();
            return utcDateTime.ToLocalDateTimeFromUtcOffset(offset);
        }
        public static string ToLocalDateTimeFromUtcOffsetString(this DateTime utcDateTime, string offset)
        {
            if (string.IsNullOrEmpty(offset))
                return utcDateTime.ToFormatedDateTimeWithAmPmString();
            TimeSpan dd = TimeSpan.Parse(offset);
            DateTimeOffset tt = new(utcDateTime, dd);
            return tt.DateTime.ToFormatedDateTimeWithAmPmString();
        }
        public static string ToLocalDateTimeStringFromSignInUserUtcOffset(this DateTime utcDateTime, HttpContext context)
        {
            string offset = context.User.Identity.GetUserUtcTimeZoneOffset();
            TimeSpan dd = TimeSpan.Parse(offset);
            DateTimeOffset tt = new(utcDateTime, dd);
            return tt.DateTime.ToFormatedDateTimeWithAmPmString();
        }
        public static DateTime ToLocalDateTimeFromSignInUserUtcOffset(this DateTime utcDateTime, HttpContext context)
        {
            string offset = context.User.Identity.GetUserUtcTimeZoneOffset();
            TimeSpan timeSpanOffset = TimeZoneInfo.Local.GetUtcOffset(DateTimeOffset.Now);
            if (!string.IsNullOrEmpty(offset))
            {
                timeSpanOffset = TimeSpan.Parse(offset);
            }
            DateTimeOffset tt = new(utcDateTime, timeSpanOffset);
            return tt.DateTime;
        }
        public static DateTime? ToLocalDateTimeFromSignInUserUtcOffset(this DateTime? utcDateTime, HttpContext context)
        {
            if (utcDateTime == null)
                return null;
            string offset = context.User.Identity.GetUserUtcTimeZoneOffset();
            TimeSpan timeSpanOffset = TimeZoneInfo.Local.GetUtcOffset(DateTimeOffset.Now);
            if (!string.IsNullOrEmpty(offset))
            {
                timeSpanOffset = TimeSpan.Parse(offset);
            }
            DateTimeOffset tt = new(utcDateTime.GetValueOrDefault(), timeSpanOffset);
            return tt.DateTime;
        }
        public static DateTime? ToFormatedDateParse(this string date, string format = "dd/MM/yyyy")
        {
            if (string.IsNullOrEmpty(date))
                return null;
            var formatInfoinfo = new DateTimeFormatInfo();
            return DateTime.ParseExact(date, format, formatInfoinfo);

        }
        public static DateTime? ToFormatedDate(this string date)
        {
            if (string.IsNullOrEmpty(date))
                return null;
            var formatInfoinfo = new DateTimeFormatInfo();
            date = date.Replace("/", "-");
            List<string> lst = date.Split('-').ToList<string>();
            if (lst.Count == 3)
            {
                if (lst[1].Length != 3)
                {
                    lst[1] = formatInfoinfo.GetMonthName(lst[1].ToInt()).ToString();
                }
                return Convert.ToDateTime(lst.Aggregate((i, j) => i + "-" + j) + "  00:00:00.000");
            }
            else
                return null;
        }

        public static DateTime ConvertToUSACSTDateTime(this object obj)
        {
            if (obj != null)
            {
                var CSTDateTime = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(obj), TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));
                return CSTDateTime;
            }
            else
            {
                return DateTime.UtcNow;
            }
        }

        public static string GetFormattedString(this string input)
        {
            string str = System.Text.RegularExpressions.Regex.Replace(input, "<[^>]*>", string.Empty).Trim();
            return str;
        }

        public static bool IsValidEmailAddress(this string s)
        {
            Regex regex = new(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

        #endregion

        #region Number-Guid
        /// <summary>
        /// RoundTwoDigit
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static decimal ToRoundTwoDigit(this decimal? data)
        {
            decimal value = (decimal)0.00;
            if (data.HasValue)
                value = Math.Round(data.Value, 2);
            return Decimal.Parse(value.ToString("0.00"));
        }
        public static bool IsNumeric(this string str)
        {
            Regex _isNumber = new(@"^\d+.\d+$");
            Match m = _isNumber.Match(str);
            return m.Success;
        }
        public static bool ToBoolean(this object obj)
        {
            if (obj == null) return false;
            var val = obj.ToInt();
            if (val == 0 || val == 1)
            {
                return Convert.ToBoolean(val);
            }
            return false;
        }
        /// <summary>
        /// Return -99 when value is null or 0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Int32 ToInt3299(this object obj)
        {
            return Convert.ToInt32(obj == null || Convert.ToString(obj) == string.Empty ? 0 : obj);
        }
        public static int ToInt(this object obj, int defaultValue)
        {
            return ToInt32(obj, defaultValue);
        }
        public static int ToInt(this object obj)
        {
            return ToInt32(obj, 0);
        }
        public static Guid ToGuid(this object obj)
        {
            if (obj == null) return Guid.Empty;
            bool result = Guid.TryParse(obj.ToString(), out Guid outResult);
            if (result)
                return outResult;
            return Guid.Empty;
        }
        public static string ToStaffPin(this object obj)
        {
            if (obj == null) return "";
            string result = obj.ToString().PadLeft(8, '0');
            if (result.Length >= 8)
                return result;
            return "00000000";
        }

        public static string ToGuidString(this Guid obj)
        {
            return obj.ToString("N").ToLower();
        }
        public static string ToGuidString(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
                return obj.ToGuid().ToString("N").ToLower();
            return null;
        }
        public static int ToInt32(this object obj, int defaultValue)
        {
            if (obj == null || Convert.ToString(obj) == string.Empty || obj.ToString() == "-99" || obj.ToString().ToLower() == "new" || obj.ToString().ToLower() == "false")
            {
                return defaultValue;
            }
            if (obj.ToString().ToLower() == "true")
            {
                return 1;
            }
            return Convert.ToInt32(obj);
        }
        public static Int64 ToInt64(this object obj)
        {
            var input = obj == null || Convert.ToString(obj).Trim() == string.Empty || obj.ToString() == "-99" || obj.ToString().ToLower() == "new" ? 0 : obj;
            long.TryParse(input.ToString(), out long result);
            return result;
        }
        public static Int64 ToLong(this object obj)
        {
            return ToInt64(obj);
        }
        public static Decimal ToDecimal(this object obj)
        {
            return Convert.ToDecimal(obj == null || Convert.ToString(obj) == string.Empty || obj.ToString() == "-99" || obj.ToString().ToLower() == "new" ? 0 : obj);
        }
        public static Decimal ToRound(this object obj)
        {
            Decimal tmp = Convert.ToDecimal(obj == null || Convert.ToString(obj) == string.Empty || obj.ToString() == "-99" || obj.ToString().ToLower() == "new" ? 0 : obj);
            return Math.Round(tmp, 2);
        }

        public static string ToProperCase(this string str)
        {
            string formattedText = null;

            if (!string.IsNullOrEmpty(str))
            {
                formattedText = new System.Globalization.CultureInfo("en").TextInfo.ToTitleCase(str.ToLower());
            }
            return formattedText;
        }

        public static string MakeRoundToString(this object value)
        {
            if (value != null)
            {
                return decimal.TryParse(value.ToString(), out decimal output) ? output.ToRound().ToString() : string.Empty;
            }
            else
            {
                return string.Empty;
            }
        }
        public static string ToProperCaseNoS(this object obj)
        {
            if (obj != null)
                return ToProperCaseNoS(obj.ToString());
            else
                return null;
        }
        public static string ToProperCaseNoS(this string str)
        {
            string formattedText = null;

            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace('-', ' ');
                formattedText = new System.Globalization.CultureInfo("en").TextInfo.ToTitleCase(str.ToLower());
            }
            return formattedText;
        }
        public static string ToProperCaseLink(this string str)
        {
            string formattedText = null;

            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace(' ', '-');
                formattedText = new System.Globalization.CultureInfo("en").TextInfo.ToLower(str.ToLower());
            }
            return formattedText;
        }
        public static string ToDigit(this string str)
        {
            string formattedText = null;

            if (!string.IsNullOrEmpty(str))
            {
                formattedText = new string(str.Where(c => char.IsDigit(c)).ToArray());
            }
            return formattedText;
        }
        public static string ToUSAFormat(this string str)
        {
            string formattedText = null;

            if (!string.IsNullOrEmpty(str))
            {
                var value = Convert.ToInt64(str);
                formattedText = String.Format("{0:###-### ####}", value);
            }
            return formattedText;
        }
        /// <summary>
        /// Displaay date as format : dd/MM/yyyy
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToFormatedDateString(this object obj)
        {
            if (obj != null)
                return ToFormatedDateString(Convert.ToDateTime(obj));
            return null;
        }
        /// <summary>
        /// Displaay date as format : dd/MM/yyyy
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>string</returns>
        public static string ToShortDateString(this object obj)
        {
            if (obj != null)
                return ToFormatedDateString(Convert.ToDateTime(obj));
            return null;
        }

        #endregion


        public static string ToAlpha(this int val)
        {
            string result = "";
            do
            {
                result = (char)((val - 1) % 26 + 'A') + result;
                val = (val - 1) / 26;
            } while (val > 0);
            return result;
        }

        public static string ToFormatedSub(this object obj)
        {
            if (obj != null)
                return ToFormatedSub(obj.ToString());
            return "";
        }
        public static string ToFormatedSub(this string obj)
        {
            if (obj != null)
            {
                List<string> str = obj.Split(' ').ToList<string>();
                if (str.Count > 7)
                {
                    string result = "";

                    for (int i = 0; i <= 7; i++)
                    {
                        result += str[i] + " ";
                    }
                    return result.TrimEnd(' ') + " ...";
                }
                else
                {
                    return obj;
                }
            }
            else
            {
                return "";
            }


        }
        public static string ToFormatedSub(this string obj, int count)
        {
            if (obj != null)
            {
                List<string> str = obj.Split(' ').ToList<string>();
                if (str.Count > count)
                {
                    string result = "";

                    for (int i = 0; i <= count; i++)
                    {
                        result += str[i] + " ";
                    }
                    return result.TrimEnd(' ') + " ...";
                }
                else
                {
                    return obj;
                }
            }
            else
            {
                return "";
            }
        }



        /// <summary>
        /// Convert Date Format  Fri Jun 21 00:00:00 UTC+0100 2013 to 21/Fri/2013  </summary>
        /// <param name="date"> date in string</param>
        /// <param name="browser"></param>
        /// <seealso cref="String">
        ///  </seealso>
        public static string JSONToLocalDateString(this string date, string browser)
        {
            string output = null;
            if (!string.IsNullOrEmpty(date))
            {
                var inputSplit = date.Split(' ').ToList();
                output = string.Format("{0}/{1}/{2}", inputSplit[2], inputSplit[1], browser == "IE" ? inputSplit[5] : inputSplit[3]);
            }
            return output;
        }

        public static string ToShortMonthName(this int monthValue)
        {
            var output = CultureInfo.InvariantCulture.DateTimeFormat.GetAbbreviatedMonthName(monthValue);
            return output;
        }
        public static string ToMonthText(this int dateValue)
        {
            string[] monthDB = new string[] { "", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            string output = "";
            if (dateValue > 0 && dateValue < 13)
            {
                output = monthDB[dateValue];
            }
            return output;
        }
        public static string ToMonthAndYearText(this string date, string sep)
        {
            string[] monthDB = new string[] { "", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            string output = "";
            if (date != "")
            {
                DateTime? dt = Convert.ToDateTime(date);

                if (dt != null)
                {
                    int month = dt.Value.Month;
                    output = monthDB[month] + sep + " " + dt.Value.Year;
                }

            }
            return output;
        }

        public static string ToHexString(this string asciiString)
        {
            string hex = "";
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += string.Format("{0:x2}", System.Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }
        public static string ToStringFromHex(this string hexValue)
        {
            string strValue = "";
            while (hexValue.Length > 0)
            {
                strValue += System.Convert.ToChar(System.Convert.ToUInt32(hexValue.Substring(0, 2), 16)).ToString();
                hexValue = hexValue[2..];
            }
            return strValue;
        }

        public static string ToEncryptString(this object plainObject)
        {
            if (plainObject == null || Convert.ToString(plainObject) == string.Empty)
            {
                return "";
            }
            return RapidErpHelper.EncryptString(plainObject.ToString(), RapidErpSettings.KeyPassword,
                RapidErpSettings.SaltPassword);
        }
        public static string ToDectryptString(this string encryptValue)
        {
            if (encryptValue == null || Convert.ToString(encryptValue) == string.Empty)
            {
                return "";
            }
            return RapidErpHelper.DecryptString(encryptValue, RapidErpSettings.KeyPassword,
                RapidErpSettings.SaltPassword);
        }

        public static bool IsFunc(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var type = obj.GetType();
            if (!type.GetTypeInfo().IsGenericType)
            {
                return false;
            }

            return type.GetGenericTypeDefinition() == typeof(Func<>);
        }

        public static bool IsFunc<TReturn>(object obj)
        {
            return obj != null && obj.GetType() == typeof(Func<TReturn>);
        }

        public static bool IsPrimitiveExtendedIncludingNullable(Type type, bool includeEnums = false)
        {
            if (IsPrimitiveExtended(type, includeEnums))
            {
                return true;
            }

            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return IsPrimitiveExtended(type.GenericTypeArguments[0], includeEnums);
            }

            return false;
        }

        private static bool IsPrimitiveExtended(Type type, bool includeEnums)
        {
            if (type.GetTypeInfo().IsPrimitive)
            {
                return true;
            }

            if (includeEnums && type.GetTypeInfo().IsEnum)
            {
                return true;
            }

            return type == typeof(string) ||
                   type == typeof(decimal) ||
                   type == typeof(DateTime) ||
                   type == typeof(DateTimeOffset) ||
                   type == typeof(TimeSpan) ||
                   type == typeof(Guid);
        }

        public static bool IsAjaxRequest(this HttpRequest CurrentRequest)
        {
            if (CurrentRequest == null)
                return false;
            return string.Equals(CurrentRequest.Query["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal) ||
                string.Equals(CurrentRequest.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal) ||
                string.Equals(CurrentRequest.Headers["X-Requested-With"], "Fetch", StringComparison.Ordinal);
        }
    }

}
