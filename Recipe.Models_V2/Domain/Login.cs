using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.Models_V2.Domain
{
    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsOwner { get; set; }
    }
}
