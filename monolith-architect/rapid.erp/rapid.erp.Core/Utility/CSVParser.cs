using Microsoft.AspNetCore.Http;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace rapid.erp.Core.Utility
{
    public static class CSVParser
    {
        public static DataTable ConvertCSVtoDataTable(IFormFile file)
        {
            DataTable dt = new DataTable();
            using (var stream = file.OpenReadStream())
            using (StreamReader sr = new StreamReader(stream))
            {

                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string readRows = sr.ReadLine().ToString();
                    string[] rows = Regex.Split(readRows, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                    //string[] rows = sr.ReadLine().Split(',');

                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i].Trim();
                    }
                    dt.Rows.Add(dr);
                }

            }
            return dt;
        }
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        var typeGEt = pro.PropertyType;
                        Type type = Nullable.GetUnderlyingType(pro.PropertyType) ?? pro.PropertyType;
                        string typeName = type.Name;

                        try
                        {
                            if (typeName == "Int16" || typeName == "Int32" || typeName == "Int64")
                            {
                                pro.SetValue(obj, Convert.ToInt32(dr[column.ColumnName]), null);

                            }
                            else if (typeName == "DateTime")
                            {

                                pro.SetValue(obj, DateTime.ParseExact((string)dr[column.ColumnName], "mm/dd/yyyy", CultureInfo.InvariantCulture), null);

                            }
                            else
                            {
                                pro.SetValue(obj, dr[column.ColumnName], null);

                            }
                        }

                        catch (Exception)
                        {
                            continue;

                        }



                    }
                    else
                        continue;
                }
            }
            return obj;
        }


        public static IEnumerable<T> DataTableToList<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        public static string GetExtension(this string name)
        {
            return Path.GetExtension(name);

        }
        public static IEnumerable<T> CSVToList<T>(this IFormFile file)
        {
            if (file != null)
            {
                if (file.FileName.GetExtension() != ".csv")
                    throw new Exception("File is not valid CSV");
                else
                    return ConvertCSVtoDataTable(file).DataTableToList<T>();

            }
            else
                throw new Exception("File can not be empty");
        }
    }
}
