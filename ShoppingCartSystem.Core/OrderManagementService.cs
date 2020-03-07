using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCartSystem.Abstraction.Model;
using ShoppingCartSystem.Abstraction;

namespace ShoppingCartSystem.Core
{
    class OrderManagementService
    {
        public static Dictionary<int, int> addOrderToCart;

        string[] actions = {  "ViewProductsInCart", "AddToCart", "DeleteToCart", "ApplyOrder", "CheckOrderStatus" };
        
        
        // All products
        // Select product ( Product Id ) ( ID, Name, Price )
        // Product Name, Description, Price + input for quantity
        // Ask for AddToCart option ( Add to Cart object which have Products, Users, OrderId, OrderStatus
        // 

        public static void AddToCart(OrderDetail order, int userId)
        {
            Dictionary<int, int> addToCart = new Dictionary<int, int>();
            throw new NotImplementedException();
        }

        public static void RemoveFromCart()
        {

        }

        public static void BuyNow()
        {

        }


        public static void CheckOrderStatus()
        {

        }

    }
}
