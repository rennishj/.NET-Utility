﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RJ.DAL.EF6
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NetUtilityEntities : DbContext
    {
        public NetUtilityEntities()
            : base("name=NetUtilityEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public  DbSet<ExternalUser> ExternalUsers { get; set; }
        public  DbSet<MenuItem> MenuItems { get; set; }
        public  DbSet<MenuItemType> MenuItemTypes { get; set; }
        public  DbSet<PartnerLink> PartnerLinks { get; set; }
        public  DbSet<Partner> Partners { get; set; }
        public  DbSet<User> Users { get; set; }
    }
}