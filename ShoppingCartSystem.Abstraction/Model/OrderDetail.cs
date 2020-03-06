using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartSystem.Abstraction.Model
{
    public enum OrderStatus
    {
        InProcess=1,
        InCart=2
    }

    public class OrderDetail
    {
        //order status, order number, order details
        public int OrderId
        { get; set; }
        public OrderStatus OrderStaus
        { get; set; }
        public string OrderDetails
        { get; set; }

        public Users User
        { get;
          set;
        }
    }
}
