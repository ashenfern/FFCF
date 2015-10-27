﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FFC.Framework.ClientSubscription.Web.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class FFCEntities : DbContext
    {
        public FFCEntities()
            : base("name=FFCEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<C__RefactorLog> C__RefactorLog { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    
        public virtual ObjectResult<sp_Forecast_GetDailyAvereageProductTransactions_Result> sp_Forecast_GetDailyAvereageProductTransactions()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Forecast_GetDailyAvereageProductTransactions_Result>("sp_Forecast_GetDailyAvereageProductTransactions");
        }
    
        public virtual ObjectResult<sp_Forecast_GetDailyTimeSpecificAvereageProductTransactions_Result> sp_Forecast_GetDailyTimeSpecificAvereageProductTransactions()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Forecast_GetDailyTimeSpecificAvereageProductTransactions_Result>("sp_Forecast_GetDailyTimeSpecificAvereageProductTransactions");
        }
    
        public virtual ObjectResult<sp_Forecast_GetProductCountDayByProductId_Result> sp_Forecast_GetProductCountDayByProductId(Nullable<int> productId)
        {
            var productIdParameter = productId.HasValue ?
                new ObjectParameter("ProductId", productId) :
                new ObjectParameter("ProductId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Forecast_GetProductCountDayByProductId_Result>("sp_Forecast_GetProductCountDayByProductId", productIdParameter);
        }
    
        public virtual ObjectResult<sp_Forecast_GetProductCountMonthDayByProductId_Result> sp_Forecast_GetProductCountMonthDayByProductId(Nullable<int> productId)
        {
            var productIdParameter = productId.HasValue ?
                new ObjectParameter("ProductId", productId) :
                new ObjectParameter("ProductId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Forecast_GetProductCountMonthDayByProductId_Result>("sp_Forecast_GetProductCountMonthDayByProductId", productIdParameter);
        }
    
        public virtual ObjectResult<sp_Forecast_GetProductCountYearDayByProductId_Result> sp_Forecast_GetProductCountYearDayByProductId(Nullable<int> productId)
        {
            var productIdParameter = productId.HasValue ?
                new ObjectParameter("ProductId", productId) :
                new ObjectParameter("ProductId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Forecast_GetProductCountYearDayByProductId_Result>("sp_Forecast_GetProductCountYearDayByProductId", productIdParameter);
        }
    
        public virtual ObjectResult<sp_Forecast_GetWeeklyAverageTransactions_Result> sp_Forecast_GetWeeklyAverageTransactions()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Forecast_GetWeeklyAverageTransactions_Result>("sp_Forecast_GetWeeklyAverageTransactions");
        }
    
        public virtual ObjectResult<sp_Forecast_GetWeeklyAvereageProductTransactions_Result> sp_Forecast_GetWeeklyAvereageProductTransactions()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Forecast_GetWeeklyAvereageProductTransactions_Result>("sp_Forecast_GetWeeklyAvereageProductTransactions");
        }
    }
}
