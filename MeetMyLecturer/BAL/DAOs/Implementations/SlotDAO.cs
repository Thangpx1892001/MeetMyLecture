using AutoMapper;
using BAL.DAOs.Interfaces;
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

namespace BAL.DAOs.Implementations
{
    public class SlotDAO : ISlotDAO
    {
        private AccountRepository _AccountRepo;
        private SlotRepository _slotRepo;
        private IMapper _mapper;

        public SlotDAO(IAccountRepository accountRepo, ISlotRepository slotRepo, IMapper mapper)
        {
            _AccountRepo = (AccountRepository)accountRepo;
            _slotRepo = (SlotRepository)slotRepo;
            _mapper = mapper;
        }

        public List<GetSlot> CheckStatus(int key)
        {
            try
            {
                List<Slot> list = _slotRepo.GetAll().Where(s => s.LecturerId == key && s.Status != "Unactive").ToList();
                foreach (var item in list)
                {
                    if(item.EndDatetime <= DateTime.Now)
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
                var checkSlot = _slotRepo.GetAll().FirstOrDefault(s => s.LecturerId == create.LecturerId && s.Status == "Not Book" && s.StartDatetime.Date == create.Date && s.StartDatetime.TimeOfDay == create.StartDateTime.TimeOfDay);
                var listLocation = _slotRepo.GetAll().Where(s => s.Location != "Google Meet" && s.Status == "Not Book");
                var checkLocation = listLocation.FirstOrDefault(s => s.Location == create.Location && s.StartDatetime.Date == create.Date && s.StartDatetime.TimeOfDay == create.StartDateTime.TimeOfDay);
                
                if (checkSlot != null)
                {
                    throw new Exception("The start time overlaps with the start time from another slot.");
                }

                if (checkAccountId == null)
                {
                    throw new Exception("Account Id does not exist in the system.");
                }

                if (checkLocation != null)
                {
                    throw new Exception("The location and start time overlaps with another lecturer's slot.");
                }

                TimeSpan duration = create.EndDateTime.Subtract(create.StartDateTime);
                if (duration.TotalMinutes < 15 || duration.TotalMinutes > 180)
                {
                    throw new Exception("Duration time need from 15 minute to 3 hour.");
                }

                Slot slot = new Slot()
                {
                    LecturerId = create.LecturerId,
                    Location = create.Location,
                    Code = create.Code,
                    LimitBooking = create.LimitBooking,
                    StartDatetime = new DateTime(create.Date.Year, create.Date.Month, create.Date.Day, create.StartDateTime.Hour, create.StartDateTime.Minute, create.StartDateTime.Second),
                    EndDatetime = new DateTime(create.Date.Year, create.Date.Month, create.Date.Day, create.EndDateTime.Hour, create.EndDateTime.Minute, create.EndDateTime.Second),
                    Mode = create.Mode,
                    CreatedAt = DateTime.Now,
                    Status = "Not Book",
                };
                _slotRepo.Insert(slot);
                _slotRepo.Commit();
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
                Slot existedSlot = _slotRepo.GetByID(key);
                if (existedSlot == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }
                existedSlot.Status = "Unactive";
                _slotRepo.Update(existedSlot);
                _slotRepo.Commit();
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
                    throw new Exception("Id does not exist in the system.");
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
                    throw new Exception("Account Id does not exist in the system.");
                }

                TimeSpan duration = update.EndDateTime.Subtract(update.StartDateTime);
                if (duration.TotalMinutes < 15 || duration.TotalMinutes > 180)
                {
                    throw new Exception("Duration need from 15 minute to 3 hour.");
                }

                Slot existedSlot = _slotRepo.GetByID(key);
                if (existedSlot == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                existedSlot.LecturerId = update.LecturerId;
                existedSlot.Location = update.Location;
                existedSlot.Code = update.Code;
                existedSlot.LimitBooking = update.LimitBooking;
                existedSlot.Mode = update.Mode;
                existedSlot.StartDatetime = new DateTime(update.Date.Year, update.Date.Month, update.Date.Day, update.StartDateTime.Hour, update.StartDateTime.Minute, update.StartDateTime.Second);
                existedSlot.EndDatetime = new DateTime(update.Date.Year, update.Date.Month, update.Date.Day, update.EndDateTime.Hour, update.EndDateTime.Minute, update.EndDateTime.Second);
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
    }
}