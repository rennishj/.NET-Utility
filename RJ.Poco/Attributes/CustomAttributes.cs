using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJ.Poco.Attributes
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = false,Inherited = true)]
  public  class ColumnOrderAttribute : Attribute
    {
        public int Order { get; set; }
        public ColumnOrderAttribute(int columnOrder)
        {
            this.Order = columnOrder;
        }
    }

    /// <summary>
    /// This attribute is used to exclude properties from the csv file generation
    /// </summary>
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = false)]
    public class CsvIgnore : Attribute
    {
        
    }
}
