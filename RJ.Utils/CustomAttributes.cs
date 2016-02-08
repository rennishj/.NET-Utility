using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJ.Utils
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
}
