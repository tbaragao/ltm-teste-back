using Common.Domain;
using Microsoft.AspNetCore.Mvc;
using System;

namespace SmartSecretary.Api.Controllers
{
	
    [Route("api/[controller]")]
    public class HealthController : Controller
    {

        [HttpGet]
        public string Get()
        {
            return string.Format("Is running at now {0}", DateTime.Now.ToTimeZone());
        }
    }
}
