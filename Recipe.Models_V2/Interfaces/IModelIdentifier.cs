using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.Models_V2.Interfaces
{
    public interface IModelIdentifier
    {
        //Hydrate with Id.
        int Id { get; set; }
    }
}
