using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCartSystem.Abstraction.Model;
using ShoppingCartSystem.Abstraction;
using System.Linq;

namespace ShoppingCartSystem.Core
{
    public class OrderManagementService : IOrderManagement
    {
        public static List<Cart> cartProduct = new List<Cart>();
        public static List<OrderDetail> order = new List<OrderDetail>();
        public static decimal totalAmount = 0;
        private static int orderId = 1;


        

        #region View Products in Cart
        /// <summary>
        /// Showing the list of products in users cart
        /// </summary>
        /// <param name="userId"> user id of current user will be used as parameter.</param>
        /// <returns> returns the list type of products.</returns>
        public List<Products> ViewProductsInCart(int userId)
        {
            var productsInCart = GetProductListFromCart(userId);
            if (productsInCart != null)
            {
                var listOfProduct = from prod in productsInCart
                                    where prod.UserId == userId
                                    select prod.Product;
                return listOfProduct.ToList();
            }
            return null;
        }


        #endregion

        #region Add Product to Cart
        /// <summary>
        /// user can add product to the cart.
        /// </summary>
        /// <param name="product"> Object of product type passed as parameter which holds the complete detail of product.</param>
        /// <param name="userId">user id of current user.</param>
        /// <returns>return boolean value for this particular event.</returns>
        public bool AddProductToCart(Products product, int userId)
        {
            bool isProductAvailable = false;
            isProductAvailable = product.Quantity <= ProductManagementService.GetProductQuantity(product.ProductId);
            if (isProductAvailable)
            {
                cartProduct.Add(new Cart() { UserId = userId, Product = product });
                return true;
            }
            else
            return false;
        }
        #endregion

        #region Remove Product from cart
        /// <summary>
        /// Remove product from users Cart
        /// </summary>
        /// <param name="productId"> Product Id that you want from</param>
        /// <param name="userId">user id of current user</param>
        /// <returns></returns>
        public bool RemoveProductFromCart(int productId, int userId)
        {
            try
            {
                var productsInCart = GetProductListFromCart(userId);
                if (productsInCart == null)
                {
                    Console.WriteLine("Cart is empty.");
                    return false;
                }
                var isProductInCart = cartProduct.FindAll(x => (x.Product.ProductId == productId) && (x.UserId == userId));

                if (isProductInCart != null && (isProductInCart.Count > 0))
                {
                    cartProduct.RemoveAll(x => (x.Product.ProductId == productId) && (x.UserId == userId));
                    return true;
                }
                else
                {
                    Console.WriteLine("Invalid product id : {0}", productId);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        public static bool isProductInCart(int productId, int userId) {
           bool isProductInCart= cartProduct.Any(x => (x.Product.ProductId == productId) && (x.UserId == userId));            
            return isProductInCart;
        }

        #region User can Get Order detaill using orderId
        /// <summary>
        /// User can check particular order
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userId"></param>
        /// <returns> returns the object of OrderDetails type.</returns>
        public OrderDetail GetOrderDetailsOfUser(int orderId, int userId)
        {
            var orderInfo = from ord in order
                            where (ord.UserId == userId) && (ord.OrderId == orderId)
                            select ord;

            return orderInfo.First();
        }
        public List<OrderDetail> GetOrderDetailsOfUser(int userId)
        {
            var orderInfo = from ord in order
                            where (ord.UserId == userId)
                            select ord;

            return orderInfo.ToList();
        }

        /// <summary>
        /// Get information of particular order.
        /// </summary>
        /// <param name="orderId">Order id of particular order.</param>
        /// <returns>return the object of OrderDetails type.</returns>
        public OrderDetail GetOrderDetails(int orderId)
        {
            var orderInfo = from ord in order
                            where (ord.OrderId == orderId)
                            select ord;

            return orderInfo.First();
        }
        #endregion

        #region Apply Order
        /// <summary>
        /// Apply order by a user
        /// </summary>
        /// <param name="userId">user id of a user.</param>
        /// <returns>return the user id</returns>
        public bool ApplyOrder(int userId)
        {
            int newOrderId = -1;
            var productsInCart = GetProductListFromCart(userId);
            if (productsInCart != null)
            {
                var isProductInStock = isAllCartProductInStock(userId);
                var outOfStock = GetOutOfStockProducts(userId);
                if (isProductInStock)
                {
                    foreach (var product in productsInCart)
                    {
                        ProductManagementService.UpdateProductQuantityAfterApplyOrder(product.Product);
                        newOrderId = CreateOrder(product.Product, userId);
                        if (newOrderId != 0)
                            new OrderManagementService().RemoveProductFromCart(product.Product.ProductId,userId);
                        else
                            Console.WriteLine("{0} Can't Removed.", product.Product.Name);

                    }
                    
                    return true;
                }
                else
                {

                    foreach (var prod in outOfStock)
                    {
                        Console.WriteLine("Product {0} is Out of Stock. Please remove from Cart.", ProductManagementService.GetProductName(prod.ProductId));
                    }
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Cart is empty.");
                return false;
            }
        }

        private bool isAllCartProductInStock(int userId)
        {
            var productsInCart = GetProductListFromCart(userId);
            var productQuant = productsInCart.All(x => x.Product.Quantity <= ProductManagementService.GetProductQuantity(x.Product.ProductId));
            return productQuant;
        }

        private List<Products> GetOutOfStockProducts(int userId)
        {
            var productsInCart = GetProductListFromCart(userId);
            var outOfStock = productsInCart
                   .Select(x => x.Product)
                   .Where(y => y.Quantity >
                   ProductManagementService.GetProductQuantity(y.ProductId)
                   ).ToList();
            return outOfStock;
        }


        private static int CreateOrder(Products product, int userId)
        {
            var newOrder = new OrderDetail()
            {
                Status = OrderStatus.InProcess,
                OrderId = GenerateOrderId(),
                Product = product,
                UserId = userId
            };
            order.Add(newOrder);
            return newOrder.OrderId;
        }

        #endregion

        #region Cancel order
        /// <summary>
        /// User can cancel the order
        /// </summary>
        /// <param name="OrderId">Order id of a orders.</param>
        /// <param name="userId">userid of user.</param>
        /// <returns>returns boolean value for cancel order.</returns>
        public bool CancelOrder(int orderId, int userId)
        {
            try {
                var orderData = GetOrderDetails(orderId);
                if (orderData != null)
                {
                    order.RemoveAll(x => (x.OrderId == orderId) && (x.UserId == userId));
                    ProductManagementService.UpdateProductsDetailAfterCancelProduct(orderData.Product);
                    return true;
                }
                else
                    return false;
            } catch (Exception ) {
                Console.WriteLine("Order not avilable.");
                return false;
            }
        }
        #endregion

        
        /// <summary>
        /// View all orders
        /// </summary>
        /// <param name="userId"> user id of a user.</param>
        public List<OrderDetail> ViewAllOrders(int userId)
        {
            List<OrderDetail> list;
            var user = UserManagementService.GetUserInfo(userId);
            if (user.UserRole.ToString() == "Admin")
            {
                list = AllOrders();
            }
            else
                list = CurrentUserOrders(userId);
            return list;
        }

        /// <summary>
        /// Get all order list which is watched by admin user.
        /// </summary>
        /// <returns>returns list of object of orderdetails type.</returns>
        private static List<OrderDetail> AllOrders()
        {
            var ordersWatchByAdmin = from ord in order select ord;
            return ordersWatchByAdmin.ToList();
        }

        /// <summary>
        /// Get all order list which is watched by current user.
        /// </summary>
        /// <param name="userId"> user id of a user.</param>
        /// <returns>returns list of object of orderdetails type.</returns>
        private static List<OrderDetail> CurrentUserOrders(int userId)
        {
            var ordersWatchByNonAdmin = from ord in order
                                        where ord.UserId == userId
                                        select ord;
            return ordersWatchByNonAdmin.ToList();
        }



        #region Get order status via orderId
        /// <summary>
        /// Get status of Order.
        /// </summary>
        /// <param name="orderId">id of Order</param>
        /// <returns>Returns the status of your order</returns>
        public string GetOrderStatus(int orderId)
        {
            try
            {
                var orderStatus = from ord in order
                                  where ord.OrderId == orderId
                                  select ord.Status.ToString();
                return orderStatus.First();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";
        }

        /// <summary>
        /// Get status of Order via user
        /// </summary>
        /// <param name="orderId">id of Order</param>
        /// <param name="userId">id of a user</param>
        /// <returns>Returns the status of your order</returns>
        public static string GetOrderStatus(int orderId, int userId)
        {
            var status = from ord in order
                         where (ord.OrderId == orderId) && (ord.UserId == userId)
                         select ord.Status;

            Console.WriteLine(status);
            return status.First().ToString();
        }
        #endregion



        #region Add product to cart util function just to add product without making object of product model
        private void AddProductToCart(int productId, int quantity, int userId)
        {
            Products product = new Products()
            {
                ProductId = productId,
                Quantity = quantity,
                Price = ProductManagementService.GetProductPrice(productId)
            };
            AddProductToCart(product, userId);
        }

       
        #endregion

        #region Get Product List from Cart (util)
        private static List<Cart> GetProductListFromCart(int userId)
        {
            var products = cartProduct.FindAll(x => x.UserId == userId);
            if (products != null && (products.Count > 0))
                return products;
            else return null;
        }

        #endregion

        #region Generate order id for internal use ( util)
        private static int GenerateOrderId()
        {
            return orderId++;
        }
        #endregion      

        #region Total price per quantity (util)
        public static decimal GetTotalPriceAccordingToQuantity(decimal price, int quantity)
        {
            return quantity * price;
        }
        #endregion

        #region Check Cart is empty or not 
        public static bool isProductExistsInUserCart(int userId)
        {
            var products = cartProduct.FindAll(x => x.UserId == userId);
            if (products != null && (products.Count > 0))
                return true;
            else return false;
        }
        #endregion


        // update cart prduct
        public Cart ModifyCartProduct(Products product,int userId) {
            var prodDetail = cartProduct.Where(x => (x.Product.ProductId == product.ProductId) && x.UserId==userId);
            if (cartProduct.Count() > 0)
            {
                prodDetail.Select(x =>
                {
                    x.Product.Quantity = product.Quantity;
                    return x;
                }).ToList();
                return prodDetail.First<Cart>();
            }
            return null;
        }
    }
}