using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCartSystem.Abstraction.Model;


namespace ShoppingCartSystem.Abstraction
{
    interface IProductManagement
    {
        Products AddProduct(Products products);
        int DeleteProduct(int productId);
        bool DeleteProduct(int product, int quantity);
        bool UpdateProduct(Products product);


    }
}
