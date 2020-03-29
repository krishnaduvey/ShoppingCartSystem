using Microsoft.EntityFrameworkCore;
using ShoppingCartSystem.Abstraction.Model;
using System;

namespace ShoppingCartSystem.Infrastructure
{
    public class DatabaseConfig: DbContext
    {
        public DatabaseConfig() : base("ShoppinCartDB")
        {
            Database..SetInitializer<DatabaseConfig>(new ShoppingCartDbInitializer());
        }
        public DbSet<Cart> cart { get; set; }
        public DbSet<LoginDetails> login { get; set; }
        public DbSet<OrderDetail> orderDetail { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<Users> users { get; set; }
    }
}
