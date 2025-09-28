using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Reflection;

namespace rapid.erp.Core.Utility
{
    public static class UtilHelper
    {
        /// <summary>
        /// Get Two Digit Round value
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Return 0.00 if input is null</returns>
        public static decimal RoundTwoDigit(decimal? data)
        {
            decimal value = (decimal)0.00;
            if (data.HasValue)
                value = Math.Round(data.Value, 2);
            return Decimal.Parse(value.ToString("0.00"));
        }

        /// <summary>
        /// Generate New Random Number (6 digit)
        /// </summary>
        /// <returns></returns>
        public static string GenerateNewRandomD6()
        {
            Random generator = new Random();
            String r = generator.Next(1, 999999).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateNewRandomD6();
            }
            return "123456";
        }

        /// <summary>
        /// Get Month Name list
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetMonth()
        {
            var list = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                list.Add(new SelectListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i), i.ToString()));
            }
            return list;
        }

        /// <summary>
        /// Get Year list from system date (This year -1)
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetYear()
        {
            var list = new List<SelectListItem>();
            int CurrentYear = DateTime.Now.Year;
            list.Add(new SelectListItem((CurrentYear - 1).ToString(), (CurrentYear - 1).ToString()));
            for (int i = 0; i <= 12; i++)
            {
                list.Add(new SelectListItem((CurrentYear + i).ToString(), (CurrentYear + i).ToString()));
            }
            return list;
        }


        public static int GetNoOfDaysLeft(DateTime startDate, DateTime endDate, Boolean excludeWeekends, List<DateTime> excludeDates)
        {
            int count = 0;
            for (DateTime index = startDate; index < endDate; index = index.AddDays(1))
            {
                if (excludeWeekends && index.DayOfWeek != DayOfWeek.Friday && index.DayOfWeek != DayOfWeek.Saturday)
                {
                    bool excluded = false; ;
                    for (int i = 0; i < excludeDates.Count; i++)
                    {
                        if (index.Date.CompareTo(excludeDates[i].Date) == 0)
                        {
                            excluded = true;
                            break;
                        }
                    }

                    if (!excluded)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Add working daty. Exclude Friday and Saturday
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="nDays"></param>
        /// <returns></returns>
        public static DateTime AddWorkingDays(DateTime dt, int nDays)
        {
            int weeks = nDays / 5;
            nDays %= 5;
            while (dt.DayOfWeek == DayOfWeek.Friday || dt.DayOfWeek == DayOfWeek.Saturday)
                dt = dt.AddDays(1);

            while (nDays-- > 0)
            {
                dt = dt.AddDays(1);
                if (dt.DayOfWeek == DayOfWeek.Saturday)
                    dt = dt.AddDays(2);
            }
            return dt.AddDays(weeks * 7);
        }

        /// <summary>
        /// Gets an assembly by its name if it is currently loaded
        /// </summary>
        /// <param name="Name">Name of the assembly to return</param>
        /// <returns>The assembly specified if it exists, otherwise it returns null</returns>
        public static System.Reflection.Assembly GetLoadedAssembly(string Name)
        {
            try
            {
                foreach (Assembly TempAssembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (TempAssembly.GetName().Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return TempAssembly;
                    }
                }
                return null;
            }
            catch { throw; }
        }

        /// <summary>
        /// Get Controller from Name of Controller.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static object GetController(string Name)
        {
            try
            {
                var assembly = rapid.erp.Core.Utility.UtilHelper.GetLoadedAssembly("Bits.EAudit.Web");
                Type _type = assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Controller)))
                                .Where(x=> x.Name == "ReportWritingReportGenerationController")
                                .FirstOrDefault();
                var  c = Activator.CreateInstance(_type);
                return c;
            }
            catch { throw; }
        }
    }
}
