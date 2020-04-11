using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Key]
        public int OrderId
        { get; set; }
        public OrderStatus Status
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
