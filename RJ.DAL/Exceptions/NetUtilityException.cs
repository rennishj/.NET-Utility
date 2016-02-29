using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJ.DAL.Exceptions
{
    /// <summary>
    /// This is used to throw your own exceptions
    /// </summary>
   public class NetUtilityException : Exception
    {
       public NetUtilityException() : base()
       {
           this.ErrorMessages = new List<string>();
       }

       public NetUtilityException(string message) : base(message)
       {
           this.ErrorMessages = new List<string>();
       }

       public NetUtilityException(string message,Exception inner) : base(message,inner)
       {
           this.ErrorMessages = new List<string>();
       }
       public List<string> ErrorMessages { get; set; }
    }
}
