using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.Models_V2.Domain
{
    public class User : Owner
    {
        public bool Admin { get; set; }
    }
}
