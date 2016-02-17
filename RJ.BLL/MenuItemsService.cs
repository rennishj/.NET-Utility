using RJ.DAL;
using RJ.Poco;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace RJ.BLL
{
    public class MenuItemsService
    {
        public MenuAccess DAL { get { return new MenuAccess(); } }

        public async Task<List<MenuItem>> MenuItemReadAll()
        {           
            List<MenuItem> menuItems = CacheService.Get("menuitems") as List<MenuItem>;
            if (menuItems == null)
            {
                menuItems = await DAL.GetAllMenuItems();
                SetParentChildRelationShip(menuItems, null);
                CacheService.Put(menuItems,"menuitems", new CacheItemPolicy { AbsoluteExpiration = DateTime.Now.AddMinutes(10), Priority = CacheItemPriority.Default });
            }            
            return menuItems;
        }

        private void SetParentChildRelationShip(List<MenuItem> menuItems,int? parentId)
        {
            foreach(var mi in menuItems)
            {                
                if (mi.ParentId == parentId)
                { 
                    //Set children for the parent
                    var children = menuItems.Where(m => m.ParentId == mi.MenuItemId).ToList();
                    mi.Children = children;
                    //set the children for each child
                    SetParentChildRelationShip(menuItems, mi.MenuItemId);
                }
            }
        }
    }
}
