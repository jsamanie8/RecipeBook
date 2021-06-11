using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Recipe.Helpers.Security;
using Recipe.Models_V2.Domain;
using Recipe.Services_V2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipe.Web.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserApiController : BaseApiController
    {
        private IUserService _service = null;
        private Salt salt = null;
        private readonly string _connection;

        public UserApiController(IUserService service, ILogger<UserApiController> logger, IConfiguration config) : base (logger)
        {
            _service = service;
            salt = new Salt();
        }

        [HttpGet, AllowAnonymous]
        public ActionResult<List<User>> Get()
        {
            ActionResult result = null;

            try
            {
                var userResult = _service.Get();
                result = Ok(userResult);
            }
            catch (Exception ex)
            {
                result = StatusCode(500, ex.ToString());
            }

            return result;
        }
    }
}
