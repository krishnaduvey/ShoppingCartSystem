using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartSystem.Abstraction.Model
{
    public class LoginDetails
    {
        public bool LoginStaus
        { get; set; }
        public string Message { get; set; }

        public Users User
        { get; set; }

    }
}
