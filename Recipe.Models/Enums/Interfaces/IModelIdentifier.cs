using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.Models.Enums.Interfaces
{
    public interface IModelIdentifier
    {
        //Hydrate with Id.
        int Id { get; set; }
    }
}
