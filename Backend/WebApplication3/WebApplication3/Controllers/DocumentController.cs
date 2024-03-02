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
    //[EnableCors(PolicyName = "AllowOnlyGoogle")]
    public class DocumentController : ControllerBase
    {
        private readonly ILogger<DocumentController> _logger;
        private readonly IMapper _mapper;
        private readonly IDocumentRepository _documentRepository;
        private APIResponse _apiResponse;

        //public DocumentController(ILogger<DemoController> logger, IDocumentRepository documentRepository, IMapper mapper)
        public DocumentController(ILogger<DocumentController> logger, IDocumentRepository documentRepository, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _documentRepository = documentRepository;
            _apiResponse = new();
        }

        [HttpGet]
        [Route("All", Name = "GetAllDocuments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetDocumentsAsync()
        {
            try
            {
                _logger.LogInformation("GetAllDocuments method started");
                var documents = await _documentRepository.GetAllAsync();
                //mapper
                _apiResponse.Data = _mapper.Map<List<DocumentDTO>>(documents);
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
        [Route("{id:int}", Name = "GetDocumentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetDocumentByIDAsync(int id)
        {
            try
            {
                //BadRequest - 400 - Client error
                if (id <= 0)
                {
                    //_logger.LogWarning("Bad Request");
                    return BadRequest();
                }
                var document = await _documentRepository.GetAsync(document => document.Id == id);
                //NotFound - 400 -NotFound - Client error
                if (document == null)
                {
                    //_logger.LogError("Document not found with given Id");
                    return NotFound($"The document with id {id} not found");
                }
                _apiResponse.Data = _mapper.Map<DocumentDTO>(document);
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

        /*[HttpGet]
        [Route("{name:alpha", Name = "GetDocumentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetStudentByNameAsync(string name)
        {
            try
            {
                //BadRequest - 400 - Client error
                if (string.IsNullOrEmpty(name))
                {
                    return BadRequest();
                }
                var document = await _documentRepository.GetAsync(document => document.docname.ToLower().Contains(name.ToLower()));
                //NotFound - 400 -NotFound - Client error
                if (document == null)
                {
                    return NotFound($"The document with name {name} not found");
                }
                _apiResponse.Data = _mapper.Map<DocumentDTO>(document);
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
        }*/

        [HttpPost]
        [Route("Create")]
        //api/student/create
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<APIResponse>> CreatedocumentAsync([FromBody] DocumentDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest();
                }
                //mapper
                Document document = _mapper.Map<Document>(dto);

                var documentAfterCreation = await _documentRepository.CreateAsync(document);
                dto.Id = documentAfterCreation.Id;

                _apiResponse.Data = dto;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                //Status - 201
                //https://localhost:7190/api/Documnet/2

                return CreatedAtRoute("GetDocumentById", new { id = dto.Id }, _apiResponse);
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
        public async Task<ActionResult<APIResponse>> UpdateDocumentAsync([FromBody] DocumentDTO dto)
        {
            try
            {
                if (dto == null || dto.Id <= 0)
                {
                    return BadRequest();
                }

                var existingDocument = await _documentRepository.GetAsync(document => document.Id == dto.Id, true);
                if (existingDocument == null)
                {
                    return NotFound();
                }

                var newRecord = _mapper.Map<Document>(dto);
                await _documentRepository.UpdateAsync(newRecord);

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
        public async Task<ActionResult<APIResponse>> UpdateDocumentPartialAsync(int id, [FromBody] JsonPatchDocument<DocumentDTO> patchDocument)
        {
            try
            {
                if (patchDocument == null || id <= 0)
                {
                    return BadRequest();
                }

                var existingStudent = await _documentRepository.GetAsync(student => student.Id == id, true);
                if (existingStudent == null)
                {
                    return NotFound();
                }

                var documentDTO = _mapper.Map<DocumentDTO>(existingStudent);
                patchDocument.ApplyTo(documentDTO, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                existingStudent = _mapper.Map<Document>(documentDTO);
                await _documentRepository.UpdateAsync(existingStudent);

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

        [HttpDelete("Delete/{id}", Name = "DeleteDocumentById")]
        //api/student/delete/1
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteDocumentAsync(int id)
        {
            try
            {
                //BadRequest - 400 - Client error
                if (id <= 0)
                {
                    return BadRequest();
                }
                var document = await _documentRepository.GetAsync(document => document.Id == id);
                //NotFound - 400 -NotFound - Client error
                if (document == null)
                {
                    return NotFound($"The document with id {id} not found");
                }

                await _documentRepository.DeleteAsync(document);
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
