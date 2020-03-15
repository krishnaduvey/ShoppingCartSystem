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

    }
}
