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
using Microsoft.Extensions.Configuration;

namespace Recipe.Web.Api.Controllers
{
    [Route("api/owners")]
    [ApiController]
    public class OwnerApiController : ControllerBase
    {
        private readonly string _connection;

        public OwnerApiController(IConfiguration config)
        {
            //Work on getting connection string from Startup/Dependency Injection...
            //Check if the connection string shows up here.
            _connection = config.GetConnectionString("Default");
        }
        [HttpGet, AllowAnonymous]
        public ActionResult<Owner> Get()
        {
            return null;
        }
    }
}
