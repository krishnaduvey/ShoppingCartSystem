using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartSystem.Abstraction.Model
{
    public class Products
    {
        //product name, product id, product description, quantity, price
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
