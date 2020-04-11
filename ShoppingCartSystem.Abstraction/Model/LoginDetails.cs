using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartSystem.Abstraction.Model
{
    public class LoginDetails
    {
        [Key]
        public int? LoginID { get; set; }
        public bool LoginStaus
        { get; set; }
        public string Message { get; set; }

        public Users User
        { get; set; }

    }
}
