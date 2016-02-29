using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJ.Poco
{
   public interface IModificationHistory
    {
         DateTime DateCreated { get; set; }
         DateTime DateModified { get; set; }
         bool IsDirty { get; set; }
    }
}
