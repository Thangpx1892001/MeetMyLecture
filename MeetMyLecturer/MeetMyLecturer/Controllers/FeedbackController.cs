using BAL.DAOs.Implementations;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Bookings;
using BAL.DTOs.Feedbacks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetMyLecturer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        public IFeedbackDAO _feedbackDAO;

        public FeedbackController(IFeedbackDAO feedbackDAO)
        {
            _feedbackDAO = feedbackDAO;
        }

        [HttpGet("GetAllById/{id}")]
        public IActionResult GetAllById(int id)
        {
            try
            {
                List<GetFeedback> feedbacks = _feedbackDAO.GetAllById(id);
                return Ok(new
                {
                    Data = feedbacks
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
        public IActionResult Get(int id)
        {
            try
            {
                GetFeedback feedback = _feedbackDAO.Get(id);
                return Ok(new
                {
                    Data = feedback
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
        public IActionResult Post([FromBody] CreateFeedback create)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _feedbackDAO.Create(create);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
