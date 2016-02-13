using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RJ.DAL.EF6;

namespace RJ.DAL
{
   public class MenuAccess
    {
       public NetUtilityEntities2 Context
       {
           get { return new NetUtilityEntities2(); }
       }
       public List<MenuItem> GetAllMenuItems()
       {
           var menuITems =  Context.MenuItems.ToList();
           return menuITems;
       }
    }
}
