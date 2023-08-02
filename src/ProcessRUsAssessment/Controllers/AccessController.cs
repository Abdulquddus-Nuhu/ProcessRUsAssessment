using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessRUsAssessment.Models;
using ProcessRUsAssessment.Services;
using ProcessRUsAssessment.Shared.Responses;
using Swashbuckle.AspNetCore.Annotations;
using static ProcessRUsAssessment.Constants.StringConstants;

namespace ProcessRUsAssessment.Controllers
{
    [Route("api/[controller]")]
    public class AccessController : Controller
    {
        private readonly FruitsRepository _fruitsRepository;

        public AccessController(FruitsRepository fruitsRepository)
        {
            _fruitsRepository = fruitsRepository;
        }

        //[Authorize(Roles = Roles.ADMIN + ", " + Roles.BACKOFFICE)]
        [SwaggerOperation(
        Summary = "Get Five Random Fruits Endpoint",
        Description = "This endpoint returns 5 random fruits. It requires Admin or BackOffice privilege",
        OperationId = "fruit.get",
        Tags = new[] { "AccessEndpoints" })
        ]   
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<FruitResponse>), StatusCodes.Status200OK)]
        [HttpGet(Name = "GetFruits")]
        public async Task<IEnumerable<FruitResponse>> GetFruits()
        {
            return await _fruitsRepository.GetRandomFruitsAsync();
        }
    }
}

