using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RJ.Poco;

namespace RJ.DAL.EF6
{
   public class NinjaContext : DbContext
    {
       public NinjaContext() : base("NetUtility")
       {

       }

       public DbSet<Ninja> Ninjas { get; set; }
       public DbSet<NinjaEquipment> Equipment { get; set; }
       public DbSet<Clan> Clans { get; set; }
       protected override void OnModelCreating(DbModelBuilder modelBuilder)
       {
           modelBuilder.Types()
                       .Configure(c => c.Ignore("IsDirty"));
            base.OnModelCreating(modelBuilder);
       }

       public override int SaveChanges()
       {
           var modifiedOrAddedEntities = this.ChangeTracker.Entries().Where(e => e.Entity is IModificationHistory && (e.State == EntityState.Added || e.State == EntityState.Modified)).Select(e => e.Entity as IModificationHistory);
           foreach (var history in modifiedOrAddedEntities)
           {
               if (history.DateCreated == DateTime.MinValue)
               {
                   history.DateCreated = DateTime.Now;
               }
               history.DateModified = DateTime.Now;
           }
           var rowsAffected =  base.SaveChanges();
           
           foreach (var item in this.ChangeTracker.Entries().Where(e => e.Entity is IModificationHistory).Select(e => e.Entity as IModificationHistory))
           {
               item.IsDirty = false;
           }

           return rowsAffected;
       }

    }
}
