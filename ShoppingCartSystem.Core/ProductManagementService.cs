using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ShoppingCartSystem.Abstraction.Model;
using ShoppingCartSystem.Abstraction;

namespace ShoppingCartSystem.Core
{
    public class ProductManagementService
    {
        // view , modify, remove { item number, item detail}
        // out of stock
        // how many product left associated with product id

        public static List<Products> products = new List<Products>();

        public static void ProductDetails()
        {
            if (products.Count > 0)
            {
                Console.WriteLine("Product Details :");
                foreach (var product in products)
                {
                    Console.WriteLine("Name : {0} | Price : {1} | Desciption : {2}", product.Name, product.Price, product.Description);
                }
            }
            else
            {
                Console.WriteLine("No stock available.");
            }
        }

        public static bool DeleteProduct(int productId)
        {
            try
            {
                var result = products.Remove(products.Single(x => x.ProductId == productId));
                return result;
            }
            catch (ArgumentNullException argumentNull )
            {
                Console.WriteLine(argumentNull);
                return false;
            }
            catch (InvalidOperationException invalidOperation)
            {
                Console.WriteLine(invalidOperation);
                return false;
            }
        }

        public static void AddNewProduct(Products product)
        {
            products.Add(new Products()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Quantity = product.Quantity,
                Price = product.Price,
            }
            );
        }

    }
}
