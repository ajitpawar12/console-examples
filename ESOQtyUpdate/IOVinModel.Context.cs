﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ESOQtyUpdate
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class IOVinTrackingEntities : DbContext
    {
        public IOVinTrackingEntities()
            : base("name=IOVinTrackingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRolePermission> AspNetRolePermissions { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ChatMessage> ChatMessages { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Container> Containers { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Distributor> Distributors { get; set; }
        public virtual DbSet<DocNumber> DocNumbers { get; set; }
        public virtual DbSet<DocNumberType> DocNumberTypes { get; set; }
        public virtual DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public virtual DbSet<FieldStepRoleVisibility> FieldStepRoleVisibilities { get; set; }
        public virtual DbSet<FormField> FormFields { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Party> Parties { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Plant> Plants { get; set; }
        public virtual DbSet<ShipmentMode> ShipmentModes { get; set; }
        public virtual DbSet<StatusDistributorDoc> StatusDistributorDocs { get; set; }
        public virtual DbSet<StatusesTrack> StatusesTracks { get; set; }
        public virtual DbSet<Step> Steps { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<UserChatUnreadCount> UserChatUnreadCounts { get; set; }
        public virtual DbSet<UserCountry> UserCountries { get; set; }
        public virtual DbSet<UserDistributor> UserDistributors { get; set; }
        public virtual DbSet<UserUtilization> UserUtilizations { get; set; }
        public virtual DbSet<Vin> Vins { get; set; }
        public virtual DbSet<Zone> Zones { get; set; }
    
        public virtual ObjectResult<sp_UserUitilizationReport_Result> sp_UserUitilizationReport(string fromdate, string todate, string userRole, Nullable<short> projectType)
        {
            var fromdateParameter = fromdate != null ?
                new ObjectParameter("fromdate", fromdate) :
                new ObjectParameter("fromdate", typeof(string));
    
            var todateParameter = todate != null ?
                new ObjectParameter("todate", todate) :
                new ObjectParameter("todate", typeof(string));
    
            var userRoleParameter = userRole != null ?
                new ObjectParameter("userRole", userRole) :
                new ObjectParameter("userRole", typeof(string));
    
            var projectTypeParameter = projectType.HasValue ?
                new ObjectParameter("projectType", projectType) :
                new ObjectParameter("projectType", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_UserUitilizationReport_Result>("sp_UserUitilizationReport", fromdateParameter, todateParameter, userRoleParameter, projectTypeParameter);
        }
    }
}