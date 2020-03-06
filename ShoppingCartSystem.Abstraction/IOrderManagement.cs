
using ShoppingCartSystem.Abstraction.Model;
namespace ShoppingCartSystem.Abstraction
{
    interface IOrderManagement
    {

        OrderDetail GetOrderDetails(int orderId, int userId);
        int ApplyOrder(OrderDetail orderDetail); // it will add to Cart and OrderId to show the detail further

        bool CancelOrder(int OrderId);



    }
}
