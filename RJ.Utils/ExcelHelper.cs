using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.IO;
using RJ.Poco.Attributes;
using System.Reflection;

namespace RJ.Utils
{
    /// <summary>
    /// https://www.paragon-inc.com/resources/blogs-posts/easy_excel_interaction_pt5
    /// </summary>
    public class ExcelHelper
    {
        public static Stream CreateExcelPackage<T>(IEnumerable<T> data)
        {
            //iterate through the object and check for ExcelHeader,Column Order attribute and 
            using (ExcelPackage ep = new ExcelPackage(new MemoryStream()))
            {
                ep.Workbook.Properties.Author = "RJ Utils";
                ep.Workbook.Properties.Title = "EP Plus Excel export";
                ep.Workbook.Properties.Comments = "This is the geneareted excel file";
                ep.Workbook.Worksheets.Add("First Method");
                var worksheet = ep.Workbook.Worksheets[1];
                worksheet.Cells[1, 1].LoadFromCollection(data, true);
                ep.Save();
                return ep.Stream;
            }

        }

        public static ExcelPackage CreateExcel<T>(IEnumerable<T> data)
        {
            return null;
        }

        private static List<FieldInfo> BuildFieldInfo(Type tpe)
        {
            List<FieldInfo> retVal = new List<FieldInfo>();
            var propertyInfos = tpe.GetProperties().Where(p => p.CanRead && p.GetCustomAttribute<ExcelHeaderAttribute>() != null).ToList();
            int fiCounter = 0;
            foreach (var pi in propertyInfos)
            {
                var displayOrderAttribute = pi.GetCustomAttribute<ExcelColumnOrderAttribute>();
                var headerNameAttribute = pi.GetCustomAttribute<ExcelHeaderAttribute>();
                var formatStringAttribute = pi.GetCustomAttribute<ExcelFormatAttribute>();
                var typeCode  = Type.GetTypeCode(pi.PropertyType);
                //To do : Check for Nullable Types
                FieldInfo fi = new FieldInfo
                {
                    NaturalOrder = fiCounter++,
                    DisplayOrder = Int32.MaxValue
                };
                switch (typeCode)
                {                                    
                    case TypeCode.Int16:                       
                    case TypeCode.Int32:                       
                    case TypeCode.Int64:
                    case TypeCode.Decimal:
                    case TypeCode.Double:
                    case TypeCode.SByte:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                    case TypeCode.Byte:
                        fi.DataType = ExcelDataType.Number;
                        break;
                    case TypeCode.Boolean:
                        fi.DataType = ExcelDataType.Boolean;
                        break;
                    case TypeCode.Char:
                    case TypeCode.String:
                        fi.DataType = ExcelDataType.String;
                        break;
                    case TypeCode.DateTime:
                        fi.DataType = ExcelDataType.DateTime;
                        break;   
                    default:
                        continue;//Object types has to be flattened to the primitive types
                }
                fi.DisplayOrder = displayOrderAttribute != null ? displayOrderAttribute.ColumnOrder : Int32.MaxValue;
                fi.HeaderName = headerNameAttribute != null ? headerNameAttribute.Name : pi.Name;
                fi.FormatString = formatStringAttribute != null ? formatStringAttribute.Format : null;
                retVal.Add(fi);
            }
            return retVal.OrderBy(o => o.DisplayOrder).ThenBy(o => o.NaturalOrder).ToList();
        }
    }

    public class FieldInfo
    {
        public int DisplayOrder { get; set; }
        public string HeaderName { get; set; }
        public string FormatString { get; set; }
        public ExcelDataType DataType { get; set; }
        public int NaturalOrder { get; set; }

    }

    public enum ExcelDataType
    { 
        Number = 1,
        String,
        DateTime,
        Boolean

    }


}
