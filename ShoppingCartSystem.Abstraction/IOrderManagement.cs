
using ShoppingCartSystem.Abstraction.Model;
using System.Collections.Generic;

namespace ShoppingCartSystem.Abstraction
{
    public interface IOrderManagement
    {

        OrderDetail GetOrderDetails(int userId);
        
        int ApplyOrder(int userId); // it will add to Cart and OrderId to show the detail further

        bool CancelOrder(int OrderId,int userId);

        List<Products> ViewProductsInCart(int userId);

        bool AddProductToCart(Products product,int userId);

        bool RemoveProductFromCart(int productId, int userId);

        string GetOrderStatus(int orderId);

    }
}
