using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJ.Poco
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public int? ParentId { get; set; }
        public string DisplayName { get; set; }
        public string Url { get; set; }
        public int MenuItemTypeId { get; set; }
        public List<MenuItem> Children { get; set; }
    }

    public enum MenuItemType
    {
        Unknown = 0,
        Link = 1,
        Separator = 2
    }
}
