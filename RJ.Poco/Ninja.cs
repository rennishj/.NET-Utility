using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJ.Poco
{
    public class Ninja : IModificationHistory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Clan Clan { get; set; }
        public int ClanId { get; set; }
        public List<NinjaEquipment> EquipmentsOwned { get; set; }
        public bool ServedInOniwaban { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsDirty { get; set; }
    }

    public class Clan : IModificationHistory
    {
        public Clan()
        {
            this.Ninjas = new List<Ninja>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Ninja> Ninjas { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsDirty { get; set; }
    }

    public class NinjaEquipment : IModificationHistory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Ninja Ninja { get; set; }
        public EquipmentType Type { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsDirty { get; set; }
    }

    public class EquipmentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NinjaId { get; set; }
        public EquipmentType Type { get; set; }
    }
}
