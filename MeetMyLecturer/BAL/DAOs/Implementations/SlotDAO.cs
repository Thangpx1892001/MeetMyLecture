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

        public void Create(CreateSlot create)
        {
            try
            {
                var checkAccountId = _AccountRepo.GetByID(create.LecturerId);
                if (checkAccountId != null)
                {
                    throw new Exception("Account Id does not exist in the system.");
                }

                string randomString = "abcdefghijklmnopqrstuvwxyz0123456789";
                string randomString10 = randomString.Substring(0, 10);

                Slot slot = new Slot()
                {
                    LecturerId = create.LecturerId,
                    Title = create.Title,
                    Location = create.Location,
                    Code = randomString10,
                    LimitBooking = create.LimitBooking,
                    StartDatetime = create.StartDatetime,
                    EndDatetime = create.EndDatetime,
                    Mode = create.Mode,
                    CreatedAt = DateTime.UtcNow,
                    Status = "Not booking",
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
                _slotRepo.Delete(key);
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
                Slot slot = _slotRepo.GetByID(key);
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
                List<GetSlot> list= _mapper.Map<List<GetSlot>>(_slotRepo.GetAll().Where(s => s.LecturerId == key));
                if (list == null)
                {
                    throw new Exception("Doesn't have Slot.");
                }
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
                if (checkAccountId != null)
                {
                    throw new Exception("Account Id does not exist in the system.");
                }

                Slot existedSlot = _slotRepo.GetByID(key);
                if (existedSlot == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                existedSlot.LecturerId = update.LecturerId;
                existedSlot.Title = update.Title;
                existedSlot.Location = update.Location;
                existedSlot.LimitBooking = update.LimitBooking;
                existedSlot.StartDatetime = update.StartDatetime;
                existedSlot.EndDatetime = update.EndDatetime;
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
