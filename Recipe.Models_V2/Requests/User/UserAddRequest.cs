using Recipe.Models_V2.Requests.Owner;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.Models_V2.Requests.User
{
    public class UserAddRequest : OwnerAddRequest
    {
        public bool Admin { get; set; }
    }
}
