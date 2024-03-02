using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentTepository;
        private APIResponse _apiResponse;

        public StudentController(ILogger<StudentController> logger, IStudentRepository studentTepository, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _studentTepository = studentTepository;
            _apiResponse = new();
        }

        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetStudentsAsync()
        {
            try
            {
                //var students = new List<StudentDTO>();
                //foreach (var item in CollegeRepository.Students)
                //{
                //    StudentDTO obj = new StudentDTO()
                //    {
                //        Id = item.Id,
                //        StudentName = item.StudentName,
                //        Address = item.Address,
                //       Email = item.Email
                //   };
                //   students.Add(obj);
                //}
                _logger.LogInformation("GetStudents method started");
                var students = await _studentTepository.GetAllAsync();
                //var students = await _dbContext.Students.Select(s => new StudentDTO()
                //{
                //    Id = s.Id,
                //    StudentName = s.StudentName,
                //    Address = s.Address,
                //    Email = s.Email,
                //    DOB = Convert.ToDateTime(s.DOB),
                //}).ToListAsync();

                //mapper
                _apiResponse.Data = _mapper.Map<List<StudentDTO>>(students);
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
        [Route("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetStudentByIdAsync(int id)
        {
            try
            {
                //BadRequest - 400 - Client error
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest();
                }
                var student = await _studentTepository.GetAsync(student => student.Id == id);
                //NotFound - 400 -NotFound - Client error
                if (student == null)
                {
                    _logger.LogError("Student not found with given Id");
                    return NotFound($"The student with id {id} not found");
                }
                //var studentDTO = new StudentDTO
                //{
                //    Id = student.Id,
                //    StudentName = student.StudentName,
                //    Email = student.Email,
                //    Address = student.Address
                //};
                //mapper
                _apiResponse.Data = _mapper.Map<StudentDTO>(student);
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

        [HttpGet("{name:alpha}", Name = "GetStudentByName")]
        public async Task<ActionResult<APIResponse>> GetStudentByNameAsync(string name)
        {
            try
            {
                //BadRequest - 400 - Client error
                if (string.IsNullOrEmpty(name))
                {
                    return BadRequest();
                }
                var student = await _studentTepository.GetAsync(student => student.StudentName.ToLower().Contains(name.ToLower()));
                //NotFound - 400 -NotFound - Client error
                if (student == null)
                {
                    return NotFound($"The student with name {name} not found");
                }
                //var studentDTO = new StudentDTO
                //{
                //    Id = student.Id,
                //    StudentName = student.StudentName,
                //    Email = student.Email,
                //    Address = student.Address
                //};
                //mapper
                _apiResponse.Data = _mapper.Map<StudentDTO>(student);
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

        [HttpPost]
        [Route("Create")]
        //api/student/create
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<APIResponse>> CreateStudentAsync([FromBody] StudentDTO dto)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}
                if (dto == null)
                {
                    return BadRequest();
                }

                //if(model.AdmissionDate < DateTime.Now)
                //{
                //    //1. Directly adding error message to modelstate
                //    //2. Using custom attribute
                //    ModelState.AddModelError("AdmissionDate Error", " Admission date must be greater than or equal to todays date");
                //    return BadRequest(ModelState);
                //}

                //int newId = _dbContext.Students.LastOrDefault().Id + 1;

                //Student student = new Student
                //{
                //Id = newId,
                //    StudentName = model.StudentName,
                //    Address = model.Address,
                //    Email = model.Email,
                //    DOB = Convert.ToDateTime(model.DOB),
                //};

                //mapper
                Student student = _mapper.Map<Student>(dto);

                var studentAfterCreation = await _studentTepository.CreateAsync(student);
                dto.Id = studentAfterCreation.Id;

                _apiResponse.Data = dto;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                //dto.Id = id;
                //Status - 201
                //https://localhost:7190/api/Stusdent/2
                //Newstudent detail
                return CreatedAtRoute("GetStudentById", new { id = dto.Id }, _apiResponse);
                //return Ok(model);
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
        public async Task<ActionResult<APIResponse>> UpdateStudentAsync([FromBody] StudentDTO dto)
        {
            try
            {
                if (dto == null || dto.Id <= 0)
                {
                    return BadRequest();
                }

                var existingStudent = await _studentTepository.GetAsync(student => student.Id == dto.Id, true);

                if (existingStudent == null)
                {
                    return NotFound();
                }

                /*var newRecord = new Student()
                {
                    Id = existingStudent.Id,
                    StudentName = model.StudentName,
                    Email = model.Email,
                    Address = model.Address,
                    DOB = model.DOB
                };*/

                var newRecord = _mapper.Map<Student>(dto);
                await _studentTepository.UpdateAsync(newRecord);
                //_dbContext.Students.Update(newRecord);

                //existingStudent.StudentName = model.StudentName;
                //existingStudent.Email = model.Email;
                //existingStudent.Address = model.Address;
                //existingStudent.DOB = Convert.ToDateTime(model.DOB);

                //await _dbContext.SaveChangesAsync();

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
        public async Task<ActionResult<APIResponse>> UpdateStudentPartialAsync(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            try
            {
                if (patchDocument == null || id <= 0)
                {
                    return BadRequest();
                }

                var existingStudent = await _studentTepository.GetAsync(student => student.Id == id, true);
                if (existingStudent == null)
                {
                    return NotFound();
                }

                /*var studentDTO = new StudentDTO
                {
                    Id = existingStudent.Id,
                    StudentName = existingStudent.StudentName,
                    Email = existingStudent.Email,
                    Address = existingStudent.Address
                };*/

                var studentDTO = _mapper.Map<StudentDTO>(existingStudent);

                patchDocument.ApplyTo(studentDTO, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                existingStudent = _mapper.Map<Student>(studentDTO);

                await _studentTepository.UpdateAsync(existingStudent);


                //_dbContext.Students.Update(existingStudent);

                //existingStudent.StudentName = studentDTO.StudentName;
                //existingStudent.Email = studentDTO.Email;
                //existingStudent.Address = studentDTO.Address;
                //existingStudent.DOB = Convert.ToDateTime(studentDTO.DOB);

                //await _dbContext.SaveChangesAsync();

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

        [HttpDelete("Delete/{id}", Name = "DeleteStudentById")]
        //api/student/delete/1
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteStudentAsync(int id)
        {
            try
            {
                //BadRequest - 400 - Client error
                if (id <= 0)
                {
                    return BadRequest();
                }
                var student = await _studentTepository.GetAsync(student => student.Id == id);
                //NotFound - 400 -NotFound - Client error
                if (student == null)
                {
                    return NotFound($"The student with id {id} not found");
                }

                await _studentTepository.DeleteAsync(student);
                _apiResponse.Data = true;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                //var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
                //_dbContext.Students.Remove(student);
                //await _dbContext.SaveChangesAsync();

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
