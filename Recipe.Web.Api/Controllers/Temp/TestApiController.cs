using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipe.Web.Api.Controllers.Temp
{
    //Test this entry point.
    [Route("api/test")]
    [ApiController]
    public class TestApiController : BaseApiController
    {
        public TestApiController(ILogger<TestApiController> logger) : base (logger)
        {

        }

        [HttpGet()]
        [AllowAnonymous]
        public ActionResult Test()
        {
            Logger.LogInformation("Test endpoint is firing.");

            return Ok200();
        }
    }
}
