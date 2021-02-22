using AutoMapper;
using Hahn.ApplicatonProcess.February2021.Domain.Exceptions;
using Hahn.ApplicatonProcess.February2021.Domain.Interfaces;
using Hahn.ApplicatonProcess.February2021.Domain.Models.API;
using Hahn.ApplicatonProcess.February2021.Domain.Models.DTO;
using Hahn.ApplicatonProcess.February2021.Domain.Utils;
using Hahn.ApplicatonProcess.February2021.Domain.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Web.Controllers
{
    /// <summary>
    /// Endpoint to handle the class named Asset 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;
        private readonly IValidationService _validationService;
        private readonly IMapper _mapper;
        private readonly ILogger<AssetController> _logger;

        public AssetController(IAssetService assetService, IValidationService validationService, IMapper mapper, ILogger<AssetController> logger)
        {
            _assetService = assetService;
            _validationService = validationService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a specific asset by unique id
        /// </summary>
        /// <remarks>Please provide integer value</remarks>
        /// <param name="id" example="1">The asset unique id</param>
        /// <response code="200">The asset data retrievied</response>
        /// <response code="400">Incorrect identifier format, returns message in machine-readable format for specifying errors in HTTP API responses based on https://tools.ietf.org/html/rfc7807 </response>
        /// <response code="404">The asset not found, returns message in machine-readable format for specifying errors in HTTP API responses based on https://tools.ietf.org/html/rfc7807</response>
        /// <response code="500">Error occured retrieving the asset, returns message in machine-readable format for specifying errors in HTTP API responses based on https://tools.ietf.org/html/rfc7807</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetAssetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetAssetResponse>> GetAsync([FromRoute]int id)
        {
            try
            {
                var result = await _assetService.GetAsync(id);
                return Ok(_mapper.Map<GetAssetResponse>(result));
            }
            catch(AssetNotFoundException ex)
            {
                _logger.LogWarning("Requested asset {0} not found!", ex.Message);
                return NotFound();
            }
        }

        /// <summary>
        /// Creates an asset
        /// </summary>
        /// <response code="201">The asset created successfully</response>
        /// <response code="400">Validation failed</response>
        /// <response code="500">Error occured creating the asset, returns message in machine-readable format for specifying errors in HTTP API responses based on https://tools.ietf.org/html/rfc7807</response>
        [HttpPost]
        [ProducesResponseType(typeof(AssetDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IList<ValidationMessage>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostAsync([FromBody]PostAssetRequest asset)
        {
            var result = _validationService.Validate<PostAssetRequest, AssetValidator>(asset);
            if(result.IsValid)
            {
                var id = await _assetService.CreateAsync(_mapper.Map<AssetDto>(asset));
                return Created($"{Url.ActionLink(action: "Post", controller:"Asset")}/{id}", await _assetService.GetAsync(id));
            }
            else
            {
                _logger.LogWarning("Failed validation: {0}", result.Errors.FormatErrors());
                return BadRequest(result.Errors.FormatErrors());
            }
        }

        /// <summary>
        /// Updates the asset
        /// </summary>
        /// <response code="204">The asset updated successfully</response>
        /// <response code="400">Validation failed</response>
        /// <response code="404">The asset not found, returns message in machine-readable format for specifying errors in HTTP API responses based on https://tools.ietf.org/html/rfc7807</response>
        /// <response code="500">Error occured updating the asset, returns message in machine-readable format for specifying errors in HTTP API responses based on https://tools.ietf.org/html/rfc7807</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IList<ValidationMessage>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PutAsync([FromBody] PutAssetRequest asset)
        {
            var result = _validationService.Validate<PutAssetRequest, AssetValidator>(asset);
            if (result.IsValid)
            {
                try
                {
                    await _assetService.UpdateAsync(_mapper.Map<AssetDto>(asset));
                    return NoContent();
                }
                catch (AssetNotFoundException ex)
                {
                    _logger.LogWarning("Requested asset {0} not found!", ex.Message);
                    return NotFound();
                }

            }
            else
            {
                _logger.LogWarning("Failed validation: {0}", result.Errors.FormatErrors());
                return BadRequest(result.Errors.FormatErrors());
            }
        }

        /// <summary>
        /// Deletes a specific asset by unique id
        /// </summary>
        /// <remarks>Please provide integer value</remarks>
        /// <param name="id" example="1">The asset unique id</param>
        /// <response code="204">The asset deleted successfully</response>
        /// <response code="400">Incorrect identifier format, returns message in machine-readable format for specifying errors in HTTP API responses based on https://tools.ietf.org/html/rfc7807 </response>
        /// <response code="404">The asset not found, returns message in machine-readable format for specifying errors in HTTP API responses based on https://tools.ietf.org/html/rfc7807</response>
        /// <response code="500">Error occured retrieving the asset, returns message in machine-readable format for specifying errors in HTTP API responses based on https://tools.ietf.org/html/rfc7807</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                await _assetService.DeleteAsync(id);
                return NoContent();
            }
            catch (AssetNotFoundException ex)
            {
                _logger.LogWarning("Requested asset {0} not found!", ex.Message);
                return NotFound();
            }
        }
    }
}
