using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Terms;
using BAL.DTOs.Slots;
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
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BAL.DAOs.Implementations
{
    public class SlotDAO : ISlotDAO
    {
        private AccountRepository _AccountRepo;
        private SlotRepository _slotRepo;
        private BookingRepository _bookingRepo;
        private NotificationRepository _notificationRepo;
        private IMapper _mapper;

        public SlotDAO(IAccountRepository accountRepo, ISlotRepository slotRepo, IBookingRepository bookingRepo, 
            INotificationRepository notificationRepo , IMapper mapper)
        {
            _AccountRepo = (AccountRepository)accountRepo;
            _slotRepo = (SlotRepository)slotRepo;
            _bookingRepo = (BookingRepository)bookingRepo;
            _notificationRepo = (NotificationRepository)notificationRepo;
            _mapper = mapper;
        }

        public List<GetSlot> CheckStatus(int key)
        {
            try
            {
                List<Slot> list = _slotRepo.GetAll().Where(s => s.LecturerId == key && s.EndDatetime <= DateTime.Now && s.Status == "Not Book").ToList();
                if(list.Count > 0) { 
                    foreach (var item in list)
                    {
                            item.Status = "Finish";
                            _slotRepo.Update(item);
                            _slotRepo.Commit();
                    }
                }
                return _mapper.Map<List<GetSlot>>(list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Create(CreateSlot create)
        {
            try
            {
                var checkAccountId = _AccountRepo.GetByID(create.LecturerId);
                var checkSlot = _slotRepo.GetAll().FirstOrDefault(s => s.LecturerId == create.LecturerId && s.Status == "Not Book" && s.StartDatetime.Date == create.Date && s.StartDatetime.TimeOfDay == create.StartDateTime.TimeOfDay
                                                                || s.Location == create.Location && s.StartDatetime.Date == create.Date && s.EndDatetime.TimeOfDay >= create.StartDateTime.TimeOfDay);
                var listLocation = _slotRepo.GetAll().Where(s => s.Location != "Google Meet" && s.Status == "Not Book");
                var checkLocation = listLocation.FirstOrDefault(s => s.Location == create.Location && s.StartDatetime.Date == create.Date && s.StartDatetime.TimeOfDay == create.StartDateTime.TimeOfDay
                                                               || s.Location == create.Location && s.StartDatetime.Date == create.Date && s.EndDatetime.TimeOfDay >= create.StartDateTime.TimeOfDay);
                
                if (checkSlot != null)
                {
                    throw new Exception("Time overlaps with the start time from another slot.");
                }

                if (checkAccountId == null)
                {
                    throw new Exception("Account does not exist in the system.");
                }

                if (checkLocation != null)
                {
                    throw new Exception("The location or time overlaps with another lecturer's slot.");
                }

                TimeSpan duration = create.EndDateTime.Subtract(create.StartDateTime);
                if (duration.TotalMinutes < 15 || duration.TotalMinutes > 180)
                {
                    throw new Exception("Duration time need from 15 minute to 3 hour.");
                }

                if (create.Repeat == "Daily")
                {
                    DateTime endDaily = create.Date.AddDays(6);
                    for (DateTime date = create.Date; date <= endDaily; date = date.AddDays(1))
                    {
                        Slot slotDaily = new Slot()
                        {
                            LecturerId = create.LecturerId,
                            Location = create.Location,
                            Code = create.Code,
                            LimitBooking = create.LimitBooking,
                            StartDatetime = new DateTime(date.Year, date.Month, date.Day, create.StartDateTime.Hour, create.StartDateTime.Minute, create.StartDateTime.Second),
                            EndDatetime = new DateTime(date.Year, date.Month, date.Day, create.EndDateTime.Hour, create.EndDateTime.Minute, create.EndDateTime.Second),
                            Mode = create.Mode,
                            CreatedAt = DateTime.Now,
                            Status = "Not Book",
                        };
                        _slotRepo.Insert(slotDaily);
                        _slotRepo.Commit();
                    }
                }
                else if (create.Repeat == "Weekly")
                {
                    var checkTerm = GetTermForDate(create.Date);
                    if(checkTerm != null) 
                    {
                        for (DateTime date = create.Date; date <= checkTerm.EndDate; date = date.AddDays(7))
                        {
                            Slot slotDaily = new Slot()
                            {
                                LecturerId = create.LecturerId,
                                Location = create.Location,
                                Code = create.Code,
                                LimitBooking = create.LimitBooking,
                                StartDatetime = new DateTime(date.Year, date.Month, date.Day, create.StartDateTime.Hour, create.StartDateTime.Minute, create.StartDateTime.Second),
                                EndDatetime = new DateTime(date.Year, date.Month, date.Day, create.EndDateTime.Hour, create.EndDateTime.Minute, create.EndDateTime.Second),
                                Mode = create.Mode,
                                CreatedAt = DateTime.Now,
                                Status = "Not Book",
                            };
                            _slotRepo.Insert(slotDaily);
                            _slotRepo.Commit();
                        }
                    }
                }
                else 
                {
                    Slot slot = new Slot()
                    {
                        LecturerId = create.LecturerId,
                        Location = create.Location,
                        Code = create.Code,
                        LimitBooking = create.LimitBooking,
                        StartDatetime = create.Date.AddHours(create.StartDateTime.Hour).AddMinutes(create.StartDateTime.Minute).AddSeconds(create.StartDateTime.Second),
                        EndDatetime = create.Date.AddHours(create.EndDateTime.Hour).AddMinutes(create.EndDateTime.Minute).AddSeconds(create.EndDateTime.Second),
                        Mode = create.Mode,
                        CreatedAt = DateTime.Now,
                        Status = "Not Book",
                    };
                    _slotRepo.Insert(slot);
                    _slotRepo.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int key)
        {
            try
            {
                Slot existedSlot = _slotRepo.GetAll().Include(s => s.Lecturer).FirstOrDefault(s => s.Id == key);
                var checkBooking = _bookingRepo.GetAll().Include(b => b.Subject).FirstOrDefault(b => b.SlotId == key && b.Status == "Success");
                var bookingPending = _bookingRepo.GetAll().Where(b => b.SlotId == key && b.Status == "Pending");
                if (checkBooking != null)
                {
                    throw new Exception("Slot has booking in the system. Can't delete");
                }
                if (existedSlot == null)
                {
                    throw new Exception("Slot does not exist in the system.");
                }
                existedSlot.Status = "Unactive";
                _slotRepo.Update(existedSlot);
                _slotRepo.Commit();
                foreach(var item in bookingPending)
                {
                    item.Reason = "Slot was deleted";
                    item.Status = "Denied";
                    _bookingRepo.Update(item);
                    _bookingRepo.Commit();

                    Notification notification = new Notification()
                    {
                        BookingId = item.Id,
                        SendToId = item.StudentId,
                        Title = existedSlot.Lecturer.Fullname + " denied a booking slot Location: " + existedSlot.Location + " " +
                        item.Subject.SubjectCode + " " + existedSlot.StartDatetime.TimeOfDay + " - " + existedSlot.EndDatetime.TimeOfDay + " " +
                        existedSlot.StartDatetime.ToString("dd/MM/yyyy") + " Reason: " + item.Reason,
                        IsRead = false,
                        CreatedAt = DateTime.Now,
                    };
                    _notificationRepo.Insert(notification);
                    _notificationRepo.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetSlot Get(int key)
        {
            try
            {
                Slot slot = _slotRepo.GetAll().Include(s => s.Bookings).FirstOrDefault(s => s.Id == key);
                if (slot == null)
                {
                    throw new Exception("Slot does not exist in the system.");
                }
                return _mapper.Map<GetSlot>(slot);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetSlot> GetAllById(int key)
        {
            try
            {
                List<GetSlot> list= _mapper.Map<List<GetSlot>>(_slotRepo.GetAll().Include(s => s.Bookings).Where(s => s.LecturerId == key));
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int key, UpdateSlot update)
        {
            try
            {
                var checkAccountId = _AccountRepo.GetByID(update.LecturerId);
                if (checkAccountId == null)
                {
                    throw new Exception("Account does not exist in the system.");
                }

                TimeSpan duration = update.EndDateTime.Subtract(update.StartDateTime);
                if (duration.TotalMinutes < 15 || duration.TotalMinutes > 180)
                {
                    throw new Exception("Duration need from 15 minute to 3 hour.");
                }

                Slot existedSlot = _slotRepo.GetByID(key);
                if (existedSlot == null)
                {
                    throw new Exception("Slot does not exist in the system.");
                }

                existedSlot.LecturerId = update.LecturerId;
                existedSlot.Location = update.Location;
                existedSlot.Code = update.Code;
                existedSlot.LimitBooking = update.LimitBooking;
                existedSlot.Mode = update.Mode;
                existedSlot.StartDatetime = update.Date.AddHours(update.StartDateTime.Hour).AddMinutes(update.StartDateTime.Minute).AddSeconds(update.StartDateTime.Second);
                existedSlot.EndDatetime = update.Date.AddHours(update.EndDateTime.Hour).AddMinutes(update.EndDateTime.Minute).AddSeconds(update.EndDateTime.Second);
                if (existedSlot.EndDatetime <= DateTime.Now)
                {
                    existedSlot.Status = "Finish";
                }
                _slotRepo.Update(existedSlot);
                _slotRepo.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Term GetTermForDate(DateTime date)
        {
            List<Term> terms = new List<Term>()
            {
                new Term("Fall2023", new DateTime(2023, 9, 4), new DateTime(2023, 12, 31)),
                new Term("Spring2024", new DateTime(2024, 1, 1), new DateTime(2024, 5, 5)),
                new Term("Summer2024", new DateTime(2024, 5, 8), new DateTime(2024, 9, 1)),
                new Term("Fall2024", new DateTime(2024, 9, 4), new DateTime(2024, 12, 31)),
            };

            foreach (Term term in terms)
            {
                if (date >= term.StartDate && date <= term.EndDate)
                {
                    return term;
                }
            }
            return null;
        }
    }
}