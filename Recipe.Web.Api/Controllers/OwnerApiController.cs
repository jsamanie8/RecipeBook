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
using Recipe.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Recipe.Web.Api.Controllers
{
    [Route("api/owners")]
    [ApiController]
    public class OwnerApiController : BaseApiController
    {
        private IOwnerService _service = null;
        private readonly string _connection;

        public OwnerApiController(IOwnerService service, ILogger<OwnerApiController> logger, IConfiguration config) : base (logger)
        {
            _service = service;
            //Work on getting connection string from Startup/Dependency Injection...
            //Check if the connection string shows up here.
            _connection = config.GetConnectionString("Default");
        }
        [HttpGet, AllowAnonymous]
        public ActionResult<Owner> Get()
        {
            //return 
            return Created201();
        }
    }
}
