using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ProcessRUsAssessment.Constants.StringConstants;

namespace ProcessRUsAssessment.Controllers
{
    [Route("api/[controller]")]
    public class AccessController : Controller
    {
        // GET: api/values
        [HttpGet]
        [Authorize(Roles = Roles.ADMIN  + ", " + Roles.BACKOFFICE)]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}

