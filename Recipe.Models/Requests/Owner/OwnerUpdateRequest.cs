using Recipe.Models.Enums.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.Models.Requests.Owner
{
    public class OwnerUpdateRequest : OwnerAddRequest, IModelIdentifier
    {
        public int Id { get; set; }
    }
}
