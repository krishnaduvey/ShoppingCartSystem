using ShoppingCartSystem.Abstraction.Model;
using System;
using System.Data.Entity;

namespace ShoppingCartSystem.Infra
{
    public class DatabaseConfig: DbContext
    {
        static DatabaseConfig()
        {
            Database.SetInitializer<DatabaseConfig>(null);
        }
        public DatabaseConfig() : base("name=ShoppingCartSystem") { 
        }

        public DbSet<Cart> cart { get; set; }
        public DbSet<LoginDetails> login { get; set; }
        public DbSet<OrderDetail> orderDetail { get; set; }
        //public DbSet<Orders> orders { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<Users> users { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
            //Configuration.LazyLoadingEnabled = false;
        }
    }

}
