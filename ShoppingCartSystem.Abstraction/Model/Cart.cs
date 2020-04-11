
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCartSystem.Abstraction.Model
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public int UserId
        { get; set; }
        public Products Product
        { get; set; }

      
    }
}





















