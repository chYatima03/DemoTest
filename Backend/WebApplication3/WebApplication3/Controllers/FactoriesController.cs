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
    public class FactoriesController : ControllerBase
    {
        private readonly ILogger<FactoriesController> _logger;
        private readonly IMapper _mapper;
        private readonly IFactoryRepository _factoryRepository;
        private APIResponse _apiResponse;
        public FactoriesController(ILogger<FactoriesController> logger, IFactoryRepository factoryRepository, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _factoryRepository = factoryRepository;
            _apiResponse = new();

        }
        [HttpGet]
        [Route("All", Name = "GetAllFactory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetFactoriesAsync()
        {
            try
            {
                // _logger.LogInformation("GetDocuments method started");
                var factories = await _factoryRepository.GetAllAsync();
                //mapper
                _apiResponse.Data = _mapper.Map<List<FactoriesDTO>>(factories);
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
        [Route("{id:int}", Name = "GetFactoriesById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetFactoriesByIDAsync(int id)
        {
            try
            {
                //BadRequest - 400 - Client error
                if (id <= 0)
                {
                    //_logger.LogWarning("Bad Request");
                    return BadRequest();
                }
                var factories = await _factoryRepository.GetAsync(factories => factories.Id == id);
                //NotFound - 400 -NotFound - Client error
                if (factories == null)
                {
                    //_logger.LogError("Document not found with given Id");
                    return NotFound($"The factory with id {id} not found");
                }
                _apiResponse.Data = _mapper.Map<FactoriesDTO>(factories);
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

        public async Task<ActionResult<APIResponse>> CreateFactoriesAsync([FromBody] DocumentDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest();
                }
                //mapper
                Factory factories = _mapper.Map<Factory>(dto);

                var factoriesAfterCreation = await _factoryRepository.CreateAsync(factories);
                dto.Id = factoriesAfterCreation.Id;

                _apiResponse.Data = dto;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                //Status - 201
                //https://localhost:7190/api/Documnet/2

                return CreatedAtRoute("GetFactoriesById", new { id = dto.Id }, _apiResponse);
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
        public async Task<ActionResult<APIResponse>> UpdateFactoriesAsync([FromBody] DocumentDTO dto)
        {
            try
            {
                if (dto == null || dto.Id <= 0)
                {
                    return BadRequest();
                }

                var existingFactories = await _factoryRepository.GetAsync(factories => factories.Id == dto.Id, true);
                if (existingFactories == null)
                {
                    return NotFound();
                }

                var newRecord = _mapper.Map<Factory>(dto);
                await _factoryRepository.UpdateAsync(newRecord);

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
        public async Task<ActionResult<APIResponse>> UpdateFactoriesPartialAsync(int id, [FromBody] JsonPatchDocument<FactoriesDTO> patchDocument)
        {
            try
            {
                if (patchDocument == null || id <= 0)
                {
                    return BadRequest();
                }

                var existingFactories = await _factoryRepository.GetAsync(factories => factories.Id == id, true);
                if (existingFactories == null)
                {
                    return NotFound();
                }

                var factoriesDTO = _mapper.Map<FactoriesDTO>(existingFactories);
                patchDocument.ApplyTo(factoriesDTO, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                existingFactories = _mapper.Map<Factory>(factoriesDTO);
                await _factoryRepository.UpdateAsync(existingFactories);

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

        [HttpDelete("Delete/{id}", Name = "DeleteFactoriesById")]
        //api/student/delete/1
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteFactoriesAsync(int id)
        {
            try
            {
                //BadRequest - 400 - Client error
                if (id <= 0)
                {
                    return BadRequest();
                }
                var factories = await _factoryRepository.GetAsync(factories => factories.Id == id);
                //NotFound - 400 -NotFound - Client error
                if (factories == null)
                {
                    return NotFound($"The factories with id {id} not found");
                }

                await _factoryRepository.DeleteAsync(factories);
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
