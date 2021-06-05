using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Recipe.Web.Api.Controllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected ILogger Logger { get; set; }
        public BaseApiController(ILogger logger)
        {
            logger.LogInformation($"Controller Firing {this.GetType().Name}");
            Logger = logger;
        }

        protected OkObjectResult Ok200()
        {
            return base.Ok("Success!");
        }

        protected CreatedResult Created201(int id)
        {
            string url = Request.Path;

            return base.Created(url, id);
        }

        protected NotFoundObjectResult NotFound404()
        {
            return base.NotFound("Sorry, not found :(");
        }

        protected ObjectResult CustomResponse(HttpStatusCode code)
        {
            return StatusCode((int)code, "My custom response");
        }
    }
}
