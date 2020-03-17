using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCartSystem.Abstraction.Model;


namespace ShoppingCartSystem.Abstraction
{
    /// <summary>
    /// Interface to manage Product.
    /// </summary>
    public interface IProductManagement
    {
        /// <summary>
        /// Add new product.
        /// </summary>
        /// <param name="products">Input products type object as parameter.</param>
        /// <returns>Return int type id of newly created product.</returns>
        int AddNewProduct(Products products);

        /// <summary>
        /// Delete product.
        /// </summary>
        /// <param name="productId">Input int type productid as parameter.</param>
        /// <returns>Return bool type.</returns>
        bool DeleteProduct(int productId);

        /// <summary>
        /// Uodate product information.
        /// </summary>
        /// <param name="product">New Product information of products type object.</param>
        /// <returns>Returns updated information.</returns>
        Products UpdateProductInfo(Products product);

        /// <summary>
        /// List of available products.
        /// </summary>
        /// <returns>Returns list type products object.</returns>
        List<Products> GetAllAvailableProducts();

        /// <summary>
        /// Update the product quantity information.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
          Products UpdateProductQuantity(Products product);

    /// <summary>
    /// Update product desciption
    /// </summary>
    /// <param name="product">New desciption information of products type object.</param>
    /// <returns>Returns updated information.</returns>
          Products UpdateProductDescription(Products product);

        /// <summary>
        /// Update product Name
        /// </summary>
        /// <param name="product">New desciption in form of products type object.</param>
        /// <returns>Returns updated information.</returns>
        Products UpdateProductName(Products product);
        
        /// <summary>
        /// Update product price
        /// </summary>
        /// <param name="product">New desciption in form of products type object.</param>
        /// <returns>Returns updated information.</returns>
        Products UpdateProductPrice(Products product);
     
    }
}
