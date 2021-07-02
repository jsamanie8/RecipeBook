using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Recipe.Helpers.Security;
using Recipe.Models_V2.Domain;
using Recipe.Models_V2.Requests.User;
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
        private readonly string _connection;

        public UserApiController(IUserService service, ILogger<UserApiController> logger, IConfiguration config) : base (logger)
        {
            _service = service;
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

        [HttpPost]
        public ActionResult Add(UserAddRequest model)
        {
            ActionResult result = null;

            try
            {
                int id = _service.Add(model);
                result = Created201(id);
            }
            catch (Exception ex)
            {
                result = StatusCode(500, ex.ToString());
            }

            return result;
        }

        [HttpPost("{login}"), AllowAnonymous]
        public ActionResult<List<User>> Login(Login loginModel)
        {
            ActionResult<List<User>> result = null;

            try
            {
                var loginResult = _service.VerifyUser(loginModel);
                result = Ok(loginResult);
            }
            catch (Exception ex)
            {
                result = StatusCode(500, ex.ToString());
            }

            return result;
        }

        //TODO GET USER BY ID
    }
}
