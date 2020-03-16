using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCartSystem.Abstraction.Model;


namespace ShoppingCartSystem.Abstraction
{
    public interface IProductManagement
    {
        int AddNewProduct(Products products);
        bool DeleteProduct(int productId);

        Products UpdateProductInfo(Products product);

        List<Products> GetAllAvailableProducts();

        /// <summary>
        /// Update the product quantity information.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
          Products UpdateProductQuantity(Products product);
    
          Products UpdateProductDescription(Products product);
      
          Products UpdateProductName(Products product);
       
          Products UpdateProductPrice(Products product);
     
    }
}
