using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Feedbacks;
using BAL.DTOs.Slots;
using DAL.Models;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Implementations
{
    public class FeedbackDAO : IFeedbackDAO
    {
        private BookingRepository _bookingRepo;
        private FeedbackRepository _feedbackRepo;
        private IMapper _mapper;

        public FeedbackDAO(IBookingRepository bookingRepo, IFeedbackRepository feedbackRepo, IMapper mapper)
        {
            _bookingRepo = (BookingRepository)bookingRepo;
            _feedbackRepo = (FeedbackRepository)feedbackRepo;
            _mapper = mapper;
        }

        public void Create(CreateFeedback create)
        {
            try
            {
                var checkBookingId = _bookingRepo.GetByID(create.BookingId);
                if (_bookingRepo != null)
                {
                    throw new Exception("Booking Id does not exist in the system.");
                }

                Feedback feedback = new Feedback()
                {
                    BookingId = create.BookingId,
                    Comment = create.Comment,
                    CreatedAt = DateTime.UtcNow,
                };
                _feedbackRepo.Insert(feedback);
                _feedbackRepo.Commit();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetFeedback Get(int key)
        {
            try
            {
                Feedback feedback = _feedbackRepo.GetByID(key);
                if (feedback == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }
                return _mapper.Map<GetFeedback>(feedback);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetFeedback> GetAllById(int key)
        {
            try
            {
                List<GetFeedback> list = _mapper.Map<List<GetFeedback>>(_feedbackRepo.GetAll().Where(f => f.Booking.Slot.LecturerId == key));
                if (list == null)
                {
                    throw new Exception("Doesn't have Feedback.");
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
