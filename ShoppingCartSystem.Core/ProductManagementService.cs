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

        /// <summary>
        /// Get all the products information, which are available to our application.
        /// </summary>
        /// <returns> returns List of product type. </returns>
        public List<Products> GetAllAvailableProducts()
        {
            if (products.Count > 0)
            {
                Console.WriteLine("Product Details :");
                foreach (var product in products)
                {
                    Console.WriteLine("Product ID : {0} | Name : {1} | Price : {2}", product.ProductId, product.Name, product.Price);
                }
            }
            else
            {
                Console.WriteLine("No stock available.");
            }

            return products;
        }

        /// <summary>
        /// Delete Product from in memory db.
        /// </summary>
        /// <param name="productId"> Product id required to delete. </param>
        /// <returns>Returns the boolean value which inform delete operation of product was successfull or not in form of true/false.</returns>
        public bool DeleteProduct(int productId)
        {
            try
            {
                var result = products.Remove(products.Single(x => x.ProductId == productId));
                return result;
            }
            catch (ArgumentNullException argumentNull)
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

        /// <summary>
        /// Add the object of product type to the end of product list.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public int AddNewProduct(Products product)
        {
            int productId = 1;
            if (products.Count > 0)
            {
                productId = InsertProductId();
            }
            products.Add(new Products()
            {
                ProductId = productId,
                Name = product.Name,
                Description = product.Description,
                Quantity = product.Quantity,
                Price = product.Price,
            }
            );
            return products.Select(x => x.ProductId).Last();
        }


        /// <summary>
        /// Insert product id for each newly added product
        /// </summary>
        /// <returns> Newly added product id return</returns>
        public int InsertProductId()
        {
            var lastAddedProduct = products.Last();
            return lastAddedProduct.ProductId + 1;
        }

        /// <summary>
        /// Update product information
        /// </summary>
        /// <param name="product"> contains product related information that need to update.</param>
        public void UpdateProductInfo(Products product)
        {
            var productToUpdate = products.Where(x => x.ProductId == product.ProductId);
            if (products.Count() > 0)
            {
                var toUpdate = productToUpdate.First<Products>();
                products.Remove(toUpdate);
                products.Add(product);
            }
        }

        public static void UpdateProductQuantityAfterApplyOrder(Products product)
        {

            var prodDetail = products.Find(x => x.ProductId == product.ProductId);
            products.Select(c => { c.Quantity = (c.Quantity - product.Quantity); return c; }).ToList();

        }

        public static void UpdateProductInfo(int productId, int quantity)
        {

            var prodDetail = products.Find(x => x.ProductId == productId);
            products.Select(x => { x.Quantity = quantity; return x; }).ToList();

        }

        public static void UpdateProductInfo(int productId, decimal price)
        {

            var prodDetail = products.Find(x => x.ProductId == productId);
            products.Select(x => { x.Price = price; return x; }).ToList();
        }

        public static void UpdateProductInfo(int productId, string desciption)
        {
            var prodDetail = products.Find(x => x.ProductId == productId);
            products.Select(x => { x.Description = desciption; return x; }).ToList();
        }

        public static void UpdateProductInfoName(int productId, string name)
        {
            var prodDetail = products.Find(x => x.ProductId == productId);
            products.Select(x => { x.Name = name; return x; }).ToList();
        }

        public static void UpdateProductsDetailAfterCancelProduct(Products product)
        {
            var prodDetail = products.Find(x => x.ProductId == product.ProductId);
            products.Select(c => { c.Quantity = (c.Quantity + product.Quantity); return c; }).ToList();
        }


        public static int GetProductQuantity(int productId)
        {
            var productAvailableQuantity = products.Where(x => x.ProductId == productId).Select(x => x.Quantity).First();
            return productAvailableQuantity;
        }

        public static decimal GetProductPrice(int productId)
        {
            var productCurrentPrice = products.Where(x => x.ProductId == productId).Select(x => x.Price).First();
            return productCurrentPrice;
        }

        public static string GetProductName(int productId)
        {
            var productName = products.Where(x => x.ProductId == productId).Select(x => x.Name).First();
            return productName;
        }

        public static string GetProductDescription(int productId)
        {
            var productDescription = products.Where(x => x.ProductId == productId).Select(x => x.Description).First();
            return productDescription;
        }
    }
}
