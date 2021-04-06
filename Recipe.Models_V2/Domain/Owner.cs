using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.Models_V2.Domain
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
