using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipe.Models.Domain;
using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipe.Web.Api.Controllers
{
    [Route("api/owners")]
    [ApiController]
    public class OwnerApiController : ControllerBase
    {
        private string _connection;

        public OwnerApiController()
        {
            
        }
        [HttpGet, AllowAnonymous]
        public ActionResult<Owner> Get()
        {
            return null;
        }
    }
}
