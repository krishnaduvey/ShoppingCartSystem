
using ShoppingCartSystem.Abstraction.Model;
using System.Collections.Generic;

namespace ShoppingCartSystem.Abstraction
{
    public interface IOrderManagement
    {
        /// <summary>
        /// Get Order information.
        /// </summary>
        /// <param name="userId">User id of current user.</param>
        /// <returns>retur</returns>
        List<OrderDetail> GetOrderDetailsOfUser(int userId);

        OrderDetail GetOrderDetailsOfUser(int orderId,int userId);
        bool AddProductToCart(Products product, int userId);

        bool RemoveProductFromCart(int productId, int userId);

        bool ApplyOrder(int userId); // it will add to Cart and OrderId to show the detail further

        bool CancelOrder(int OrderId,int userId);

        List<Products> ViewProductsInCart(int userId);

        string GetOrderStatus(int orderId);

        List<OrderDetail> ViewAllOrders(int userId);

        Cart ModifyCartProduct(Products product, int userId);
    }
}

				