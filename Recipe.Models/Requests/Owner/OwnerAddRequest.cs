using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.Models.Requests.Owner
{
    public class OwnerAddRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
