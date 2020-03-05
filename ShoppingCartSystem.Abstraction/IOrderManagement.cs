
using ShoppingCartSystem.Abstraction.Model;
namespace ShoppingCartSystem.Abstraction
{
    interface IOrderManagement
    {

        OrderDetail GetOrderDetails(int orderId);
        int CreateOrder(OrderDetail orderDetail); // it will add to Cart and OrderId to show the detail further




    }
}
