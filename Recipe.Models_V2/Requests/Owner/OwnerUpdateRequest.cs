using Recipe.Models_V2.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.Models_V2.Requests.Owner
{
    public class OwnerUpdateRequest : OwnerAddRequest, IModelIdentifier
    {
        public int Id { get; set; }
    }
}
