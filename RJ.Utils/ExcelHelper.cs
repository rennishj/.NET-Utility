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
        public static Stream CreateExcelPackage<T>(List<T> data)
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

        public static ExcelPackage CreateExcel<T>(List<T> data)
        {
            List<ColumnInfo> info = BuildColumnInfo(typeof(T));
            if (info == null || info.Count == 0)
            {
                return null;
            }
            //BuildColumnNumber(info);
            ExcelPackage ep = new ExcelPackage();
            ExcelWorksheet ws = ep.Workbook.Worksheets.Add("ExcelExport");
            BuildExcelHeader(ws, 1, info);
            WriteExcelData<T>(ws, 2, data, info);
            return ep;
        }

        private static void BuildColumnNumber(List<ColumnInfo> info)
        { 
            //set up the coulumnnumber by checking the sortorder and if thats 0,setup as it appears on the object
            int counter = 1;
            foreach (var ci in info)
            {
                if (ci.DisplayOrder > 0)
                {
                    ci.ColumnNumber = ci.DisplayOrder;
                }
                else
                {
                    ci.ColumnNumber = counter;
                }
                counter++;
            }
        }
        private static List<ColumnInfo> BuildColumnInfo(Type tpe)
        {
            List<ColumnInfo> retVal = new List<ColumnInfo>();
            var propertyInfos = tpe.GetProperties().Where(p => p.CanRead && p.GetCustomAttribute<ExcelIgnoreAttribute>() == null).ToList();
            int fieldNameCounter = 0;
            foreach (var propInfo in propertyInfos)
            {
                var displayOrderAttribute = propInfo.GetCustomAttribute<ExcelColumnOrderAttribute>();
                var headerNameAttribute = propInfo.GetCustomAttribute<ExcelHeaderAttribute>();
                var formatStringAttribute = propInfo.GetCustomAttribute<ExcelFormatAttribute>();
                var typeCode  = Type.GetTypeCode(propInfo.PropertyType);
                if (propInfo.PropertyType.IsGenericType)
                {
                    if (propInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        typeCode = Type.GetTypeCode(propInfo.PropertyType.GetGenericArguments()[0]);
                    }
                    else
                        continue;
                }
                ColumnInfo ci = new ColumnInfo() { Getter = propInfo };
                fieldNameCounter++;
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
                        ci.DataType = ExcelDataType.Number;
                        break;
                    case TypeCode.Boolean:
                        ci.DataType = ExcelDataType.Boolean;
                        break;
                    case TypeCode.Char:
                    case TypeCode.String:
                        ci.DataType = ExcelDataType.String;
                        break;
                    case TypeCode.DateTime:
                        ci.DataType = ExcelDataType.DateTime;
                        break;   
                    default:
                        continue;//Object types has to be flattened to the primitive types
                }
                ci.DisplayOrder = displayOrderAttribute != null ? displayOrderAttribute.ColumnOrder : fieldNameCounter;
                ci.HeaderName = headerNameAttribute != null ? headerNameAttribute.Name : propInfo.Name;
                ci.FormatString = formatStringAttribute != null ? formatStringAttribute.Format : null;
                ci.ColumnNumber = ci.DisplayOrder;
                retVal.Add(ci);
            }
            return retVal.OrderBy(o => o.DisplayOrder).ToList();
        }

        private static void BuildExcelHeader(ExcelWorksheet ws,int row,List<ColumnInfo> columnInfos)
        { 
            foreach (var ci in columnInfos)
	        {
                ws.Cells[row, ci.ColumnNumber].Value = ci.HeaderName;
                ws.Cells[row, ci.ColumnNumber].Style.Font.Bold = true;
                ws.Cells[row, ci.ColumnNumber].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
	        }
        }
        private static void WriteExcelData<T>(ExcelWorksheet ws,int row,List<T> data,List<ColumnInfo> props)
        {
            foreach (var obj in data)
            {
                WriteExcelRows(ws, row, obj, props);
                row++;
            }
        }

        private static void WriteExcelRows<T>(ExcelWorksheet ws,int row,T data,List<ColumnInfo> props)
        {
            foreach (var ci in props)
            {
                object cellData = ci.Getter.GetValue(data);
                if (cellData == null)
                {
                    cellData = "";
                }
                if (!string.IsNullOrWhiteSpace(ci.FormatString))
                { 
                    //Apply the format string to the cellData
                    switch (ci.DataType)
                    {
                        case ExcelDataType.Number:
                            ws.Cells[row, ci.ColumnNumber].Style.Numberformat.Format = ci.FormatString;
                            ws.Cells[row, ci.ColumnNumber].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                            break;                        
                        case ExcelDataType.DateTime:
                            try
                            {
                                if(cellData != null)
                                {
                                   cellData =  ((DateTime)(cellData)).ToString(ci.FormatString);
                                }
                            }
                            catch
                            {
                                cellData = "";                                
                            }
                            break;
                        case ExcelDataType.Boolean:
                            if (cellData == null || !((bool)cellData))
                            {
                                cellData = "False";
                            }
                            else
                            {
                              cellData = "True";
                            }
                            break;                      
                    }                
                }
                ws.Cells[row, ci.ColumnNumber].Value = cellData;
            }
        }

    }

    public class ColumnInfo
    {
        public int DisplayOrder { get; set; }
        public string HeaderName { get; set; }
        public string FormatString { get; set; }
        public ExcelDataType DataType { get; set; }
        public int NaturalOrder { get; set; }
        public int ColumnNumber { get; set; }
        public PropertyInfo Getter { get; set; }

    }

    public enum ExcelDataType
    { 
        Number,
        String,
        DateTime,
        Boolean

    }


}
