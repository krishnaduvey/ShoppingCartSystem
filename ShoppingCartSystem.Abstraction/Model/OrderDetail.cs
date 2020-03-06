using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartSystem.Abstraction.Model
{
    public class OrderDetail
    {
        //order status, order number, order details
        public int OrderId
        { get; set; }
        public bool OrderStaus
        { get; set; }
        public string OrderDetails
        { get; set; }
      

    }
}
