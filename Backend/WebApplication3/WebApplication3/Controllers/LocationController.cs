using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApplication3.Data;
using WebApplication3.Data.Repository;
using WebApplication3.Models;
using WebApplication3.MyLogging;
using APIResponse = WebApplication3.Models.APIResponse;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "LoginForLocalUsers", Roles = "Superadmin,Admin")]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepository;
        private APIResponse _apiResponse;

        public LocationController(ILogger<LocationController> logger, ILocationRepository locationRepository, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _locationRepository = locationRepository;
            _apiResponse = new();
        }

        [HttpGet]
        [Route("All", Name = "GetAllLocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetLocationsAsync()
        {
            try
            {
                // _logger.LogInformation("GetDocuments method started");
                var locations = await _locationRepository.GetAllAsync();
                //mapper
                _apiResponse.Data = _mapper.Map<List<LocationDTO>>(locations);
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                //OK - 200 - Success
                //return Ok(CollegeRepository.Students);
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetLocationById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetLocationByIDAsync(int id)
        {
            try
            {
                //BadRequest - 400 - Client error
                if (id <= 0)
                {
                    //_logger.LogWarning("Bad Request");
                    return BadRequest();
                }
                var location = await _locationRepository.GetAsync(location => location.Id == id);
                //NotFound - 400 -NotFound - Client error
                if (location == null)
                {
                    //_logger.LogError("Document not found with given Id");
                    return NotFound($"The document with id {id} not found");
                }
                _apiResponse.Data = _mapper.Map<LocationDTO>(location);
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                //OK - 200 -Success
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }
        }

        [HttpPost]
        [Route("Create")]
        //api/student/create
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateLocationAsync([FromBody] LocationDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest();
                }
                //mapper
                Location location = _mapper.Map<Location>(dto);

                var locationAfterCreation = await _locationRepository.CreateAsync(location);
                dto.Id = locationAfterCreation.Id;

                _apiResponse.Data = dto;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                //Status - 201
                //https://localhost:7190/api/Documnet/2

                return CreatedAtRoute("GetLocationById", new { id = dto.Id }, _apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }
        }

        [HttpPut]
        [Route("Update")]
        //api/student/update
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> UpdateLocationAsync([FromBody] LocationDTO dto)
        {
            try
            {
                if (dto == null || dto.Id <= 0)
                {
                    return BadRequest();
                }

                var existingLocation = await _locationRepository.GetAsync(location => location.Id == dto.Id, true);
                if (existingLocation == null)
                {
                    return NotFound();
                }

                var newRecord = _mapper.Map<Location>(dto);
                await _locationRepository.UpdateAsync(newRecord);

                return NoContent();
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }
        }

        [HttpPatch]
        [Route("{id:int}/UpdatePartial")]
        //api/student/1/UpdatePartial
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> UpdateLocationPartialAsync(int id, [FromBody] JsonPatchDocument<LocationDTO> patchLocation)
        {
            try
            {
                if (patchLocation == null || id <= 0)
                {
                    return BadRequest();
                }

                var existingLocation = await _locationRepository.GetAsync(student => student.Id == id, true);
                if (existingLocation == null)
                {
                    return NotFound();
                }

                var locationDTO = _mapper.Map<LocationDTO>(existingLocation);
                patchLocation.ApplyTo(locationDTO, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                existingLocation = _mapper.Map<Location>(locationDTO);
                await _locationRepository.UpdateAsync(existingLocation);

                //204 - Not Content
                return NoContent();
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }
        }

        [HttpDelete("Delete/{id}", Name = "DeleteLocationById")]
        //api/student/delete/1
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteLocationAsync(int id)
        {
            try
            {
                //BadRequest - 400 - Client error
                if (id <= 0)
                {
                    return BadRequest();
                }
                var location = await _locationRepository.GetAsync(location => location.Id == id);
                //NotFound - 400 -NotFound - Client error
                if (location == null)
                {
                    return NotFound($"The location with id {id} not found");
                }

                await _locationRepository.DeleteAsync(location);
                _apiResponse.Data = true;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }
        }

    }
}
