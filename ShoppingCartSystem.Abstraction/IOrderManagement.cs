
using ShoppingCartSystem.Abstraction.Model;
using System.Collections.Generic;

namespace ShoppingCartSystem.Abstraction
{
    /// <summary>
    /// Interface to manage order and cart.
    /// </summary>
    public interface IOrderManagement
    {
        /// <summary>
        /// Get Order information.
        /// </summary>
        /// <param name="userId">User id of current user.</param>
        /// <returns>retur</returns>
        List<OrderDetail> GetOrderDetailsOfUser(int userId);

        /// <summary>
        /// Get Order detail of user.
        /// </summary>
        /// <param name="orderId">input order id.</param>
        /// <param name="userId">input user id.</param>
        /// <returns>Return order detail of any user in form of OrderDetail object.</returns>
        OrderDetail GetOrderDetailsOfUser(int orderId,int userId);

        /// <summary>
        /// add Product to cart.
        /// </summary>
        /// <param name="product">input Products type object.</param>
        /// <param name="userId">input user id</param>
        /// <returns>return bool value.</returns>
        bool AddProductToCart(Products product, int userId);

        /// <summary>
        /// Remove Product from cart.
        /// </summary>
        /// <param name="productId">input order id.</param>
        /// <param name="userId">input user id</param>
        /// <returns>return bool value.</returns>
        bool RemoveProductFromCart(int productId, int userId);

        /// <summary>
        /// Apply order ( Buy Products )
        /// </summary>
        /// <param name="userId">input user id</param>
        /// <returns>return bool value.</returns>
        bool ApplyOrder(int userId); 

        /// <summary>
        /// Cancel any order.
        /// </summary>
        /// <param name="OrderId">input order id.</param>
        /// <param name="userId">input user id</param>
        /// <returns>return bool value.</returns>
        bool CancelOrder(int OrderId,int userId);

        /// <summary>
        /// Display products in cart.
        /// </summary>
        /// <param name="userId">Input user id as parameter.</param>
        /// <returns>Return list of products type.</returns>
        List<Products> ViewProductsInCart(int userId);

        /// <summary>
        /// Get status of order.
        /// </summary>
        /// <param name="orderId">Input order id as parameter.</param>
        /// <returns>Return string type status of order.</returns>
        string GetOrderStatus(int orderId);

        /// <summary>
        /// View all orders.
        /// </summary>
        /// <param name="userId">input userid to check orders.</param>
        /// <returns>Returns list of orders.</returns>
        List<OrderDetail> ViewAllOrders(int userId);

        /// <summary>
        /// Modify product in cart.
        /// </summary>
        /// <param name="product">input product information to update as paramter.</param>
        /// <param name="userId">input userId as parameter.</param>
        /// <returns>Returns cart type object.</returns>
        Cart ModifyCartProduct(Products product, int userId);
    }
}

				