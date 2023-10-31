using BAL.Authentications;
using BAL.DAOs.Implementations;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Accounts;
using BAL.DTOs.Subjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetMyLecturer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        public ISubjectDAO _subjectDAO;

        public SubjectController(ISubjectDAO subjectDAO)
        {
            _subjectDAO = subjectDAO;
        }

        [HttpGet]
        //[PermissionAuthorize("Admin")]
        public IActionResult Get()
        {
            try
            {
                List<GetSubject> subjects = _subjectDAO.GetAll();
                return Ok(new
                {
                    Data = subjects
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        //[PermissionAuthorize("Admin")]
        public IActionResult Get(int id)
        {
            try
            {
                GetSubject subject = _subjectDAO.Get(id);
                return Ok(new
                {
                    Data = subject
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        //[PermissionAuthorize("Admin")]
        public IActionResult Post([FromBody] CreateSubject create)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _subjectDAO.Create(create);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        //[PermissionAuthorize("Admin")]
        public IActionResult Put(int id, [FromBody] UpdateSubject update)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _subjectDAO.Update(id, update);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        //[PermissionAuthorize("Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                _subjectDAO.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
