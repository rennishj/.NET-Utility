using RJ.DAL.EF6;
using RJ.Poco;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJ.DAL.Repository
{
    /// <summary>
    /// This shows how to create a repository for a disconnected application.
    /// Repository sits on top of NinjaContext
    /// Make calls into the Repository from the MVC controller
    /// </summary>
   public class NetUtilityRepository
    {
       public List<Ninja> GetNinjasWithClan()
       {
           using (var cxt = new NinjaContext())
           {
               //We dont need any tracking to be done on the objects,cuz these will be disconnected from web/web api
               return cxt.Ninjas.AsNoTracking().OrderByDescending(o => o.DateCreated).ToList();
           }
       }

       public Ninja GetNinjaById(int id)
       {
           using (var cxt = new NinjaContext())
           {
               //This can also be used and it makes a trip to the database
               //return cxt.Ninjas.Find(id);
               return cxt.Ninjas.AsNoTracking().FirstOrDefault(o => o.Id == id);
           }
       }

       public IEnumerable GetClanList()
       {
           using (var cxt = new NinjaContext())
           {
               return cxt.Clans.AsNoTracking().OrderBy(o => o.Name).Select(c => new { c.Id, c.Name }).ToList();                                
           }
       }

       public void UpdateNinja(Ninja ninja)
       {
           using (var cxt = new NinjaContext())
           {
               cxt.Entry(ninja).State = System.Data.Entity.EntityState.Modified;
               cxt.SaveChanges();
           }
       }

       public void CreateNewNinja(Ninja ninja)
       {
           using (var cxt = new NinjaContext())
           {
               cxt.Ninjas.Add(ninja);
               cxt.SaveChanges();
           }
       }

       public void DeletNinja(int id)
       {
           using (var cxt = new NinjaContext())
           {
               var ninja = cxt.Ninjas.Find(id);
               cxt.Entry(ninja).State = System.Data.Entity.EntityState.Deleted;
               cxt.SaveChanges();
           }
       }

       public void AddNewEquipment(NinjaEquipment equipment,int ninjaId)
       {
           using (var cxt = new NinjaContext())
           {
               var ninja = cxt.Ninjas.Find(ninjaId);
               ninja.EquipmentsOwned.Add(equipment);
               cxt.SaveChanges();
           }
       }

       public void SaveUpdatedEquipment(NinjaEquipment equip,int ninjaId)
       {
           using (var cxt = new NinjaContext())
           {
               cxt.Entry(equip).State = System.Data.Entity.EntityState.Modified;
               var ninja = cxt.Ninjas.Find(ninjaId);
               equip.Ninja = ninja;
               cxt.SaveChanges();
           }
       }

       public void SaveUpdatedEquipmentAnotherWay(NinjaEquipment eqp,int ninjaId)
       {
           using (var cxt = new NinjaContext())
           {
               //This happens if you dont have a  foreign key (NInjaId ) on the model
               //var equipmentWithNinjaFromDatabase = cxt.Equipment.Include(e => e.Ninja).FirstOrDefault(e => e.Id == eqp.Id);
               //cxt.Entry(equipmentWithNinjaFromDatabase).CurrentValues.SetValues(eqp);
               //cxt.SaveChanges();

               //Better way is
               var equipment = cxt.Equipment.Find(eqp.Id);
               cxt.Entry(eqp).State = System.Data.Entity.EntityState.Modified;
               cxt.SaveChanges();
           }

       }

    }
}
