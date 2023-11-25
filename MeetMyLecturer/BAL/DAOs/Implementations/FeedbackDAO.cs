using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Feedbacks;
using BAL.DTOs.Slots;
using DAL.Models;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        private NotificationRepository _notificationRepo;
        private IMapper _mapper;

        public FeedbackDAO(IBookingRepository bookingRepo, IFeedbackRepository feedbackRepo, INotificationRepository notificationRepo, IMapper mapper)
        {
            _bookingRepo = (BookingRepository)bookingRepo;
            _feedbackRepo = (FeedbackRepository)feedbackRepo;
            _notificationRepo = (NotificationRepository)notificationRepo;
            _mapper = mapper;
        }

        public void Create(CreateFeedback create)
        {
            try
            {
                var checkBookingId = _bookingRepo.GetAll().Include(b => b.Subject).Include(b => b.Slot).FirstOrDefault(b => b.Id == create.BookingId);
                if (checkBookingId == null)
                {
                    throw new Exception("Booking does not exist in the system.");
                }

                var checkFeedBackId = _feedbackRepo.GetAll().FirstOrDefault(f => f.BookingId == create.BookingId);
                if (checkFeedBackId != null)
                {
                    throw new Exception("Feedback already exist in the system.");
                }

                Feedback feedback = new Feedback()
                {
                    BookingId = create.BookingId,
                    Comment = create.Comment,
                    CreatedAt = DateTime.Now,
                };
                _feedbackRepo.Insert(feedback);
                _feedbackRepo.Commit();

                Notification notification = new Notification()
                {
                    BookingId = checkBookingId.Id,
                    SendToId = checkBookingId.Slot.LecturerId,
                    Title = $"You received a feedback Location: {checkBookingId.Slot.Location}  ||  {checkBookingId.Subject.SubjectCode} \n{checkBookingId.Slot.StartDatetime.ToString("HH:mm")} - {checkBookingId.Slot.EndDatetime.ToString("HH:mm")}    {checkBookingId.Slot.StartDatetime.ToString("dd/MM/yyyy")}",
                    IsRead = false,
                    CreatedAt = DateTime.Now,
                };
                _notificationRepo.Insert(notification);
                _notificationRepo.Commit();
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
                    throw new Exception("Feedback does not exist in the system.");
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
                List<GetFeedback> list = _mapper.Map<List<GetFeedback>>(_feedbackRepo.GetAll().Include(x => x.Booking.Slot).Where(f => f.Booking.Slot.LecturerId == key));
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
