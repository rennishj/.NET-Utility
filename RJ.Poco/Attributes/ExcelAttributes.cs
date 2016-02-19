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
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ExcelHeaderAttribute : Attribute
    {
        public string Name { get; set; }
        public ExcelHeaderAttribute(string headerName)
        {
            this.Name = headerName;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ExcelColumnOrderAttribute : Attribute
    {
        public int ColumnOrder { get; set; }
        public ExcelColumnOrderAttribute(int columnOrder)
        {
            this.ColumnOrder = columnOrder;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ExcelFormatAttribute : Attribute
    {
        public string Format { get; set; }
        public ExcelFormatAttribute(string format)
        {
            this.Format = format;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ExcelIgnoreAttribute : Attribute
    {

    }
}
