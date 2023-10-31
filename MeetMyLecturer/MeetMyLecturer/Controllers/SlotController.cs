using BAL.Authentications;
using BAL.DAOs.Implementations;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Requests;
using BAL.DTOs.Slots;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetMyLecturer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        public ISlotDAO _slotDAO;

        public SlotController(ISlotDAO slotDAO)
        {
            _slotDAO = slotDAO;
        }

        [HttpGet("GetAllById/{id}")]
        //[PermissionAuthorize("Lecturer", "Student")]
        public IActionResult GetAllById(int id)
        {
            try
            {
                List<GetSlot> slots = _slotDAO.CheckStatus(id);
                slots = _slotDAO.GetAllById(id);
                return Ok(new
                {
                    Data = slots
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
        //[PermissionAuthorize("Lecturer")]
        public IActionResult Get(int id)
        {
            try
            {
                GetSlot slot = _slotDAO.Get(id);
                return Ok(new
                {
                    Data = slot
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
        //[PermissionAuthorize("Lecturer")]
        public IActionResult Post([FromBody] CreateSlot create)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _slotDAO.Create(create);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        //[PermissionAuthorize("Lecturer")]
        public IActionResult Put(int id, [FromBody] UpdateSlot update)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _slotDAO.Update(id, update);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        //[PermissionAuthorize("Lecturer")]
        public IActionResult Delete(int id)
        {
            try
            {
                _slotDAO.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
