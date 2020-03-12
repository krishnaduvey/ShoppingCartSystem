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
        private static List<Cart> cartProduct = new List<Cart>();
        private static List<OrderDetail> order = new List<OrderDetail>();
        private static decimal totalAmount = 0;
        private static bool isProductExistInCart = false;
        private static int orderId = 0;


        #region Cart

        #region View Products in Cart
        /// <summary>
        /// Showing the list of products in users cart
        /// </summary>
        /// <param name="userId"> user id of current user will be used as parameter.</param>
        /// <returns> returns the list type of products.</returns>
        public List<Products> ViewProductsInCart(int userId)
        {
            var productsInCart = GetProductListFromCart(userId);
            if (!(productsInCart.Count > 0))
            {
                Console.WriteLine("Cart if empty.");
            }
            else
            {
                printCartDetails(productsInCart);
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
        /// This function used to add product to the cart.
        /// </summary>
        /// <param name="product"> Object of product type passed as parameter which holds the complete detail of product.</param>
        /// <param name="userId">user id of current user.</param>
        /// <returns>return boolean value for this particular event.</returns>
        public bool AddProductToCart(Products product, int userId)
        {
            if (product.Quantity <= ProductManagementService.GetProductQuantity(product.ProductId))
            {
                cartProduct.Add(new Cart() { UserId = userId, Product = product });
                return true;
            }
            else
                Console.WriteLine("No stock available for this much number of quantity");
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
                if (!(productsInCart.Count > 0))
                {
                    Console.WriteLine("Cart is empty.");
                    return false;
                }
                cartProduct.RemoveAll(x => (x.Product.ProductId == productId) && (x.UserId == userId));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion


        #endregion

        #region Order

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
        public int ApplyOrder(int userId)
        {
            int newOrderId = -1;
            var productsInCart = GetProductListFromCart(userId);
            if (productsInCart.Count > 0)
            {
                foreach (var product in productsInCart)
                {
                    ProductManagementService.UpdateProductsDetailAfterBuying(product.Product);
                    newOrderId = CreateOrder(product.Product, userId);
                }
            }
            return newOrderId;
        }


        private static int CreateOrder(Products product, int userId)
        {
            var newOrder = new OrderDetail()
            {
                OrderStaus = OrderStatus.InProcess,
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
        public bool CancelOrder(int OrderId, int userId)
        {
            var orderData = GetOrderDetails(orderId);
            order.RemoveAll(x => (x.OrderId == orderId) && (x.UserId == userId));
            ProductManagementService.UpdateProductsDetailAfterCancelProduct(orderData.Product);
            return true;
        }
        #endregion

        #region View all orders via admin/ current user
        /// <summary>
        /// View all orders
        /// </summary>
        /// <param name="userId"> user id of a user.</param>
        public static List<OrderDetail> ViewAllOrders(int userId)
        {
            List<OrderDetail> list;
            var user = UserManagementService.GetUserInfo(userId);
            if (user.UserRole.ToString() == "Admin")
            {
                list = AllOrders();
            }
            else
                list=CurrentUserOrders(userId);

            return list;
        }

        /// <summary>
        /// Get all order list which is watched by admin user.
        /// </summary>
        /// <returns>returns list of object of orderdetails type.</returns>
        private static List<OrderDetail> AllOrders()
        {
            var ordersWatchByAdmin = from ord in order select ord;
            printOrderDetails(ordersWatchByAdmin);
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
            printOrderDetails(ordersWatchByNonAdmin);
            return ordersWatchByNonAdmin.ToList();
        }

        /// <summary>
        /// Print orders details.
        /// </summary>
        /// <param name="orders"> order list.</param>
        private static void printOrderDetails(IEnumerable<OrderDetail> orders)
        {
            Console.WriteLine("Order Details :");
            foreach (var order in orders)
            {
                Console.WriteLine("Order Id : {0}\t|\tProduct Name : {1}\t|\tQuantity : {2}\t|\tOrderStatus : {3}", order.OrderId, order.Product.Name, order.Product.Quantity, order.OrderStaus);
            }
        }

        /// <summary>
        /// Print Cart details.
        /// </summary>
        /// <param name="products">List of products in a cart.</param>
        private static void printCartDetails(List<Cart> products)
        {
            Console.WriteLine("Items in your Cart :");
            foreach (var product in products)
            {
                Console.WriteLine("Item Name : {0}\t|\tQuantity : {1}\t|\tItem Price : {2}", product.Product.Name, product.Product.Quantity, GetTotalPriceAccordingToQuantity(product.Product.Price, product.Product.Quantity));
                totalAmount += GetTotalPriceAccordingToQuantity(product.Product.Price, product.Product.Quantity);
            }
            Console.WriteLine("Total Amount : {0}", totalAmount);
        }
        #endregion

        #region Get order status via orderId
        public string GetOrderStatus(int orderId)
        {
            var orderStatus = from ord in order where ord.OrderId == orderId select ord.OrderStaus;
            return orderStatus.First().ToString();
        }
        public static string GetOrderStatus(int orderId, int userId)
        {
            var status = from ord in order
                         where (ord.OrderId == orderId) && (ord.UserId == userId)
                         select ord.OrderStaus;

            Console.WriteLine(status);
            return status.First().ToString();
        }
        #endregion


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
            if (products.Count > 0)
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
            if (products.Count > 0)
                return true;
            else return false;
        }
        #endregion


        // update cart prduct
    }
}
