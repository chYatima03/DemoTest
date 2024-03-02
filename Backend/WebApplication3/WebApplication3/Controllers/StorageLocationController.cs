using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    public class StorageLocationController : ControllerBase
    {
        private readonly ILogger<StorageLocationController> _logger;
        private readonly IMapper _mapper;
        private readonly IStorageLocationRepository _storageLocationRepository;
        private APIResponse _apiResponse;

        public StorageLocationController(ILogger<StorageLocationController> logger, IStorageLocationRepository storageLocationRepository, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _storageLocationRepository = storageLocationRepository;
            _apiResponse = new();
        }

        [HttpGet]
        [Route("All", Name = "GetAllStorageLocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetStorageLocationsAsync()
        {
            try
            {
                // _logger.LogInformation("GetDocuments method started");
                var storageloactions = await _storageLocationRepository.GetAllAsync();
                //mapper
                _apiResponse.Data = _mapper.Map<List<StorageLocationDTO>>(storageloactions);
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
        [Route("{id:int}", Name = "GetStorageLocationById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetStorageLocationByIDAsync(int id)
        {
            try
            {
                //BadRequest - 400 - Client error
                if (id <= 0)
                {
                    //_logger.LogWarning("Bad Request");
                    return BadRequest();
                }
                var storagelocation = await _storageLocationRepository.GetAsync(storagelocation => storagelocation.Id == id);
                //NotFound - 400 -NotFound - Client error
                if (storagelocation == null)
                {
                    //_logger.LogError("Document not found with given Id");
                    return NotFound($"The StorageLocation with id {id} not found");
                }
                _apiResponse.Data = _mapper.Map<StorageLocationDTO>(storagelocation);
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

        public async Task<ActionResult<APIResponse>> CreateStorageLocationAsync([FromBody] StorageLocationDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest();
                }
                //mapper
                StorageLocation storagelocation = _mapper.Map<StorageLocation>(dto);

                var storagelocationAfterCreation = await _storageLocationRepository.CreateAsync(storagelocation);
                dto.Id = storagelocationAfterCreation.Id;

                _apiResponse.Data = dto;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                //Status - 201
                //https://localhost:7190/api/Documnet/2

                return CreatedAtRoute("GetStorageLocationById", new { id = dto.Id }, _apiResponse);
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
        public async Task<ActionResult<APIResponse>> UpdateStorageLocationAsync([FromBody] StorageLocationDTO dto)
        {
            try
            {
                if (dto == null || dto.Id <= 0)
                {
                    return BadRequest();
                }

                var existingStorageLocation = await _storageLocationRepository.GetAsync(storagelocation => storagelocation.Id == dto.Id, true);
                if (existingStorageLocation == null)
                {
                    return NotFound();
                }

                var newRecord = _mapper.Map<StorageLocation>(dto);
                await _storageLocationRepository.UpdateAsync(newRecord);

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
        public async Task<ActionResult<APIResponse>> UpdateStorageLocationPartialAsync(int id, [FromBody] JsonPatchDocument<StorageLocationDTO> patchDocument)
        {
            try
            {
                if (patchDocument == null || id <= 0)
                {
                    return BadRequest();
                }

                var existingStorageLocation = await _storageLocationRepository.GetAsync(student => student.Id == id, true);
                if (existingStorageLocation == null)
                {
                    return NotFound();
                }

                var storagelocationDTO = _mapper.Map<StorageLocationDTO>(existingStorageLocation);
                patchDocument.ApplyTo(storagelocationDTO, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                existingStorageLocation = _mapper.Map<StorageLocation>(storagelocationDTO);
                await _storageLocationRepository.UpdateAsync(existingStorageLocation);

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

        [HttpDelete("Delete/{id}", Name = "DeleteStorageLocationById")]
        //api/student/delete/1
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteStorageLocationtAsync(int id)
        {
            try
            {
                //BadRequest - 400 - Client error
                if (id <= 0)
                {
                    return BadRequest();
                }
                var storagelocation = await _storageLocationRepository.GetAsync(storagelocation => storagelocation.Id == id);
                //NotFound - 400 -NotFound - Client error
                if (storagelocation == null)
                {
                    return NotFound($"The storagelocation with id {id} not found");
                }

                await _storageLocationRepository.DeleteAsync(storagelocation);
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
