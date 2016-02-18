using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.IO;

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
    }


}
