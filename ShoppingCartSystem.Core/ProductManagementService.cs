using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ShoppingCartSystem.Abstraction.Model;
using ShoppingCartSystem.Abstraction;

namespace ShoppingCartSystem.Core
{
    public class ProductManagementService : IProductManagement
    {
        public static List<Products> products = new List<Products>();

        /// <summary>
        /// Get all the products information, which are available to our application.
        /// </summary>
        /// <returns> returns List of product type. </returns>
        public List<Products> GetAllAvailableProducts()
        {
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
            try {
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
            catch (Exception ) {
                return -1;
            }
           
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

        #region Update Product information
        /// <summary>
        /// Update product information
        /// </summary>
        /// <param name="product"> contains product related information that need to update.</param>
        public Products UpdateProductInfo(Products product)
        {
            var productToUpdate = products.Where(x => x.ProductId == product.ProductId);
            if (products.Count() > 0)
            {
                productToUpdate.Select(x => 
                { 
                    x.Name = product.Name;
                    x.Description = product.Description;
                    x.Price = product.Price;
                    x.Quantity = product.Quantity;
                    return x; 
                }).ToList();
                return productToUpdate.First<Products>();              
            }
            return null;
        }

        /// <summary>
        /// Update the product quantity information.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public Products UpdateProductQuantity(Products product)
        {
            try { 
            var prodDetail = products.Where(x => x.ProductId == product.ProductId);
            if (products.Count() > 0)
            {
                prodDetail.Select(x =>
                {
                    x.Quantity = product.Quantity;
                    return x;
                }).ToList();
                return prodDetail.First<Products>();
            }
        }
            catch (Exception) {
                Console.WriteLine("Invalid entry");
            }
            return null;
        }
        public Products UpdateProductDescription(Products product)
        {
            try { 
            var prodDetail = products.Where(x => x.ProductId == product.ProductId);
            if (products.Count() > 0)
            {
                prodDetail.Select(x =>
                {
                    x.Description = product.Description;
                    return x;
                }).ToList();
                return prodDetail.First<Products>();
            }
        }
            catch (Exception) {
                Console.WriteLine("Invalid entry");
            }

            return null;

        }
        public Products UpdateProductName(Products product)
        {
            try {
                var prodDetail = products.Where(x => x.ProductId == product.ProductId);
                if (products.Count() > 0)
                {
                    prodDetail.Select(x =>
                    {
                        x.Name = product.Name;
                        return x;
                    }).ToList();
                    return prodDetail.First<Products>();
                }
            }
            catch (Exception) {
                Console.WriteLine("Invalid entry");
            }
            return null;
        }
        public Products UpdateProductPrice(Products product)
        {
            try { 
            var prodDetail = products.Where(x => x.ProductId == product.ProductId);
            if (products.Count() > 0)
            {
                prodDetail.Select(x =>
                {
                    x.Price = product.Price;
                    return x;
                }).ToList();
                return prodDetail.First<Products>();
            }
        }
            catch (Exception) {
                Console.WriteLine("Invalid entry");
            }
            return null;
        }
        public static Products UpdateProductQuantityAfterApplyOrder(Products product)
        {
            var prodDetail = products.Where(x => x.ProductId == product.ProductId);
            if (products.Count() > 0)
            {
                prodDetail.Select(x =>
                {
                    x.Quantity -= product.Quantity;
                    return x;
                }).ToList();
                return prodDetail.First<Products>();
            }
            return null;
        }
        public static Products UpdateProductsDetailAfterCancelProduct(Products product)
        {
            var prodDetail = products.Where(x => x.ProductId == product.ProductId);
            if (products.Count() > 0)
            {
                prodDetail.Select(x =>
                {
                    x.Quantity += product.Quantity;
                    return x;
                }).ToList();
                return prodDetail.First<Products>();
            }
            return null;
        }
        #endregion

        #region Get Product Information
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
        #endregion
    }
}
