using Recipe.Models_V2.Domain;
using Recipe.Models_V2.Requests.Owner;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.Services_V2.Interfaces
{
    public interface IOwnerService
    {
        List<Owner> Get();
        
        //Modify this Add method. TODO
        int Add(OwnerAddRequest model);
    }
}
