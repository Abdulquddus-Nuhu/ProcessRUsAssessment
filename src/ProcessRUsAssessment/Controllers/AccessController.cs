using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        private readonly FruitsService _fruitsService;

        public AccessController(FruitsService fruitsService)
        {
            _fruitsService = fruitsService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.ADMIN + ", " + Roles.BACKOFFICE)]
        [SwaggerOperation(
        Summary = "Get five random fruits endpoint",
        Description = "This endpoint returns 5 random fruits. It requires Admin or BackOffice privilege",
        OperationId = "fruits.get",
        Tags = new[] { "AccessEndpoints" })
        ]   
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<FruitResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(Name = "GetFruits")]
        public async Task<IEnumerable<FruitResponse>> GetFruits()
        {
            return await _fruitsService.GetRandomFruitsAsync();
        }
    }
}

