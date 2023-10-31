using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Feedbacks;
using BAL.DTOs.Notifications;
using DAL.Models;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Implementations
{
    public class NotificationDAO : INotificationDAO
    {
        private BookingRepository _bookingRepo;
        private NotificationRepository _notificationRepo;
        private IMapper _mapper;

        public NotificationDAO(IBookingRepository bookingRepo, INotificationRepository notificationRepo, IMapper mapper)
        {
            _bookingRepo = (BookingRepository)bookingRepo;
            _notificationRepo = (NotificationRepository)notificationRepo;
            _mapper = mapper;
        }

        public void Create(CreateNotification create)
        {
            try
            {
                var checkBookingId = _bookingRepo.GetByID(create.BookingId);
                if (checkBookingId == null)
                {
                    throw new Exception("Booking Id does not exist in the system.");
                }

                Notification notification = new Notification()
                {
                    BookingId = create.BookingId,
                    Title = create.Title,
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

        public GetNotification Get(int key)
        {
            try
            {
                Notification notification = _notificationRepo.GetByID(key);
                if (notification == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }
                return _mapper.Map<GetNotification>(notification);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetNotification> GetAll()
        {
            try
            {
                List<GetNotification> list = _mapper.Map<List<GetNotification>>(_notificationRepo.GetAll().Include(n => n.Booking)
                    .Include(n => n.Booking.Slot));
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int key, UpdateNotification update)
        {
            try
            {
                var checkBookingId = _bookingRepo.GetByID(update.BookingId);
                if (checkBookingId == null)
                {
                    throw new Exception("Booking Id does not exist in the system.");
                }

                Notification existedNotification = _notificationRepo.GetByID(key);
                if (existedNotification == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                existedNotification.BookingId = update.BookingId;
                existedNotification.IsRead = true;
                _notificationRepo.Update(existedNotification);
                _notificationRepo.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
