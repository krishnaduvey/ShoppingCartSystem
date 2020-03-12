using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartSystem.Abstraction.Model
{
    public enum OrderStatus
    {
        InProcess,
        Delivered
    }

    public class OrderDetail
    {
        //order status, order number, order details
        public int OrderId
        { get; set; }
        public OrderStatus OrderStaus
        { get; set; }

        public int UserId
        { get;
          set;
        }
        public Products Product
        {
            get;
            set;
        }
    }
}
