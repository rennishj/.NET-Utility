using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJ.Poco.Attributes
{
    /// <summary>
    /// These are the attributes we should look out for while iterating through the object
    /// </summary>
   public class ExcelHeader : Attribute
    {
        public string Name { get; set; }
       public ExcelHeader(string headerName)
       {
           this.Name = headerName;
       }
    }

   public class ExcelOrderAttribute : Attribute
   {
       public int ColumnOrder { get; set; }
       public ExcelOrderAttribute(int columnOrder)
       {
           this.ColumnOrder = columnOrder;
       }
   }
}
