﻿using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Bookings;
using BAL.DTOs.Requests;
using BAL.DTOs.Slots;
using DAL.Models;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Implementations
{
    public class BookingDAO : IBookingDAO
    {
        private BookingRepository _bookingRepo;
        private SubjectRepository _subjectRepo;
        private AccountRepository _AccountRepo;
        private SlotRepository _slotRepo;
        private IMapper _mapper;

        public BookingDAO(IBookingRepository bookingRepo, ISubjectRepository subjectRepo, 
            IAccountRepository accountRepo, ISlotRepository slotRepo, IMapper mapper)
        {
            _bookingRepo = (BookingRepository)bookingRepo;
            _subjectRepo = (SubjectRepository)subjectRepo;
            _AccountRepo = (AccountRepository)accountRepo;
            _slotRepo = (SlotRepository)slotRepo;
            _mapper = mapper;
        }

        public List<GetBooking> CheckStatus(int key)
        {
            try
            {
                List<Booking> list = _bookingRepo.GetAll().Include(b => b.Slot)
                                                .Where(b => b.StudentId == key && b.Status != "Denied").ToList();
                foreach (var item in list)
                {
                    if (item.Slot.EndDatetime <= DateTime.Now)
                    {
                        item.Status = "Finish";
                        _bookingRepo.Update(item);
                        _bookingRepo.Commit();
                    }
                }
                return _mapper.Map<List<GetBooking>>(list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Create(CreateBooking create)
        {
            try
            {
                var checkStudentId = _AccountRepo.GetByID(create.StudentId);
                var checkSlotId = _slotRepo.GetByID(create.SlotId);
                var checkSubjectId = _subjectRepo.GetByID(create.SubjectId);

                List<Booking> checkBookings = _bookingRepo.GetAll().Where(b => b.SlotId == create.SlotId && b.Status == "Success").ToList();
                var countBooking = checkBookings.Count();
                if (countBooking == checkSlotId.LimitBooking)
                {
                    throw new Exception("Slot is full in the system.");
                }

                if (checkStudentId == null)
                {
                    throw new Exception("Account Id does not exist in the system.");
                }

                if (checkSlotId == null)
                {
                    throw new Exception("Slot Id does not exist in the system.");
                }

                if (checkSubjectId == null)
                {
                    throw new Exception("Subject Id does not exist in the system.");
                }

                var checkBooking = _bookingRepo.GetAll().FirstOrDefault(b => b.StudentId == create.StudentId && b.SlotId == create.SlotId);
                if (checkBooking != null)
                {
                    throw new Exception("You book slot already.");
                }

                if (create.Status.IsNullOrEmpty()) { 
                    Booking booking = new Booking()
                    {
                        StudentId = create.StudentId,
                        SlotId = create.SlotId,
                        SubjectId = create.SubjectId,
                        Description = create.Description,
                        CreatedAt = DateTime.Now,
                        Status = "Pending",
                    };
                    _bookingRepo.Insert(booking);
                    _bookingRepo.Commit();
                }
                else
                {
                    Booking booking = new Booking()
                    {
                        StudentId = create.StudentId,
                        SlotId = create.SlotId,
                        SubjectId = create.SubjectId,
                        Description = create.Description,
                        CreatedAt = DateTime.Now,
                        Status = create.Status,
                    };
                    _bookingRepo.Insert(booking);
                    _bookingRepo.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateByCode(CreateByCode createByCode)
        {
            try
            {

                var checkStudentId = _AccountRepo.GetByID(createByCode.StudentId);
                var checkSlotId = _slotRepo.GetByID(createByCode.SlotId);
                var checkSubjectId = _subjectRepo.GetByID(createByCode.SubjectId);
                if (checkStudentId == null)
                {
                    throw new Exception("Account Id does not exist in the system.");
                }

                if (checkSlotId == null)
                {
                    throw new Exception("Slot Id does not exist in the system.");
                }

                if (checkSubjectId == null)
                {
                    throw new Exception("Subject Id does not exist in the system.");
                }

                var checkBooking = _bookingRepo.GetAll().FirstOrDefault(b => b.StudentId == createByCode.StudentId && b.SlotId == createByCode.SlotId);
                if (checkBooking != null)
                {
                    throw new Exception("You book slot already.");
                }

                if (!checkSlotId.Code.Equals(createByCode.Code))
                {
                    throw new Exception("Wrong code of slot.");
                }

                List<Booking> bookings = _bookingRepo.GetAll().Where(b => b.SlotId == checkSlotId.Id && b.Status == "Success").ToList();
                var countBooking = bookings.Count();
                if (countBooking < checkSlotId.LimitBooking)
                {
                    Booking booking = new Booking()
                    {
                        StudentId = createByCode.StudentId,
                        SlotId = createByCode.SlotId,
                        SubjectId = createByCode.SubjectId,
                        Description = "Don't have Description",
                        CreatedAt = DateTime.Now,
                        Status = "Success",
                    };
                    _bookingRepo.Insert(booking);
                    _bookingRepo.Commit();
                    bookings.Add(booking);
                    countBooking = bookings.Count();
                    if (countBooking == checkSlotId.LimitBooking)
                    {
                        checkSlotId.Status = "Full";
                        _slotRepo.Update(checkSlotId);
                        _slotRepo.Commit();
                    }
                }
                else
                {
                    throw new Exception("Slot is full in the system.");
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
                Booking existedBooking = _bookingRepo.GetByID(key);
                if (existedBooking == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }
                _bookingRepo.Delete(key);
                _bookingRepo.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetBooking Get(int key)
        {
            try
            {
                Booking booking = _bookingRepo.GetAll().Include(b => b.Slot).FirstOrDefault(b => b.Id == key);
                if (booking == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }
                return _mapper.Map<GetBooking>(booking);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetBooking> GetAllById(int key)
        {
            try
            {
                List<GetBooking> list = _mapper.Map<List<GetBooking>>(_bookingRepo.GetAll().Include(b => b.Slot)
                                                                                  .Where(b => b.StudentId == key || b.Slot.LecturerId == key));
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int key, UpdateBooking update)
        {
            try
            {
                var checkStudentId = _AccountRepo.GetByID(update.StudentId);
                var checkSlotId = _slotRepo.GetByID(update.SlotId);
                var checkSubjectId = _subjectRepo.GetByID(update.SubjectId);
                if (checkStudentId == null)
                {
                    throw new Exception("Account Id does not exist in the system.");
                }

                if (checkSlotId == null)
                {
                    throw new Exception("Slot Id does not exist in the system.");
                }

                if (checkSubjectId == null)
                {
                    throw new Exception("Subject Id does not exist in the system.");
                }

                Booking existedBooking = _bookingRepo.GetAll().Include(b => b.Slot).FirstOrDefault(b => b.Id == key);
                if (existedBooking == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                existedBooking.StudentId = update.StudentId;
                existedBooking.SlotId = update.SlotId;
                existedBooking.SubjectId = update.SubjectId;
                existedBooking.Description = update.Description;
                existedBooking.Reason = update.Reason;
                existedBooking.Status = update.Status;
                _bookingRepo.Update(existedBooking);
                _bookingRepo.Commit();

                List<Booking> bookings = _bookingRepo.GetAll().Where(b => b.SlotId == checkSlotId.Id && b.Status == "Success").ToList();
                var countBooking = bookings.Count();
                if (countBooking == checkSlotId.LimitBooking)
                {
                    checkSlotId.Status = "Full";
                    _slotRepo.Update(checkSlotId);
                    _slotRepo.Commit();
                    List<Booking> checkBookings = _bookingRepo.GetAll().Include(b => b.Slot).Where(b => b.SlotId == checkSlotId.Id && b.Status == "Pending").ToList();
                    if(checkBookings.Count() > 0)
                    {
                        foreach (var booking in checkBookings)
                        {
                            booking.Status = "Denied";
                            booking.Reason = "Slot is full";
                            _bookingRepo.Update(booking);
                            _bookingRepo.Commit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
