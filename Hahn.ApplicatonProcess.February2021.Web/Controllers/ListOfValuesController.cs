using Hahn.ApplicatonProcess.February2021.Domain.Interfaces;
using Hahn.ApplicatonProcess.February2021.Domain.Models.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class ListOfValuesController : ControllerBase
    {
        private readonly IListOfValuesService _listOfValuesService;
        private readonly ILogger<ListOfValuesController> _logger;

        public ListOfValuesController(IListOfValuesService listOfValuesService, ILogger<ListOfValuesController> logger)
        {
            _listOfValuesService = listOfValuesService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a list of countries
        /// </summary>
        /// <response code="200">The list of countries</response>
        /// <response code="500">Error occured retrieving the list of countries, returns message in machine-readable format for specifying errors in HTTP API responses based on https://tools.ietf.org/html/rfc7807</response>
        [HttpGet]
        [ProducesResponseType(typeof(GetCountriesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetCountriesResponse>> GetCountries()
        {
            var result = new GetCountriesResponse
            {
                Countries = await _listOfValuesService.GetAllCountriesAsync()
            };
            _logger.LogInformation("Countries found: {0}", result.Countries.Count);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a list of departments
        /// </summary>
        /// <response code="200">The list of departments</response>
        /// <response code="500">Error occured retrieving the list of departments, returns message in machine-readable format for specifying errors in HTTP API responses based on https://tools.ietf.org/html/rfc7807</response>
        [HttpGet]
        [ProducesResponseType(typeof(GetDepartmentsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetDepartmentsResponse>> GetDepartments()
        {
            var result = new GetDepartmentsResponse
            {
                Departments = await _listOfValuesService.GetAllDepartmentsAsync()
            };
            _logger.LogInformation("Departments found: {0}", result.Departments.Count);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a list of top level domains
        /// </summary>
        /// <response code="200">The list of top level domains</response>
        /// <response code="500">Error occured retrieving the list of top level domains, returns message in machine-readable format for specifying errors in HTTP API responses based on https://tools.ietf.org/html/rfc7807</response>
        [HttpGet]
        [ProducesResponseType(typeof(GetTopLevelDomainsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetTopLevelDomainsResponse>> GetTopLevelDomains()
        {
            var result = new GetTopLevelDomainsResponse
            {
                TopLevelDomains = await _listOfValuesService.GetAllTopLevelDomainsAsync()
            };
            _logger.LogInformation("Top level domains found: {0}", result.TopLevelDomains.Count);
            return Ok(result);
        }

    }
}
