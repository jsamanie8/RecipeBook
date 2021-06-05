using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Recipe.Models_V2.Domain;
using Recipe.Services_V2.Interfaces;
using Recipe.Models_V2.Requests.Owner;

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
        }
        [HttpGet, AllowAnonymous]
        public ActionResult<List<Owner>> Get()
        {
            ActionResult result = null;

            try
            {
                var ownersResult = _service.Get();
                result = Ok(ownersResult);
            }
            catch (Exception ex)
            {
                result = StatusCode(500, ex.ToString());
            }
            
            return result;
        }

        [HttpPost]
        public ActionResult Add(OwnerAddRequest model)
        {
            //TODO: Hash the password. Return the Id created.
            ActionResult result = null;
            try
            {
                int id = _service.Add(model);

                //result = Ok(id);
                result = Created201(id);
            }
            catch (Exception ex)
            {
                result = StatusCode(500, ex.ToString());
            }

            return result;
        }

        [HttpPut]
        public ActionResult Update(OwnerUpdateRequest model)
        {
            ActionResult result = null;

            try
            {
                _service.Update(model);
                result = Ok200();
            }
            catch (Exception ex)
            {
                result = StatusCode(500, ex.ToString());
            }

            return result;
        }
    }
}
