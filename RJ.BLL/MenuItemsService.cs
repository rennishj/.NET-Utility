using RJ.DAL;
using RJ.Poco;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJ.BLL
{
    public class MenuItemsService
    {
        public MenuAccess DAL { get { return new MenuAccess(); } }

        public async Task<List<MenuItem>> MenuItemReadAll()
        {
            var menuItems =  await DAL.GetAllMenuItems();            
            SetParentChildRelationShip(menuItems, null);
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
