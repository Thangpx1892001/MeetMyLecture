using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Requests;
using DAL.Models;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BAL.DAOs.Implementations
{
    public class RequestDAO : IRequestDAO
    {
        private RequestRepository _requestRepo;
        private SubjectRepository _subjectRepo;
        private AccountRepository _AccountRepo;
        private IMapper _mapper;

        public RequestDAO(IRequestRepository requestRepo, ISubjectRepository subjectRepo, IAccountRepository accountRepo, IMapper mapper)
        {
            _requestRepo = (RequestRepository)requestRepo;
            _subjectRepo = (SubjectRepository)subjectRepo;
            _AccountRepo = (AccountRepository)accountRepo;
            _mapper = mapper;
        }

        public void Create(CreateRequest create)
        {
            try
            {
                var checkStudentId = _AccountRepo.GetByID(create.StudentId);
                var checkLecturerId = _AccountRepo.GetByID(create.LecturerId);
                var checkSubjectId = _subjectRepo.GetByID(create.SubjectId);
                if (checkStudentId == null)
                {
                    throw new Exception("Account Id does not exist in the system.");
                }

                if (checkLecturerId == null)
                {
                    throw new Exception("Lecturer Id does not exist in the system.");
                }

                if (checkSubjectId == null)
                {
                    throw new Exception("Subject Id does not exist in the system.");
                }

                Request request = new Request()
                {
                    StudentId = create.StudentId,
                    LecturerId = create.LecturerId,
                    SubjectId = create.SubjectId,
                    Description = create.Description,
                    CreatedAt = DateTime.Now,
                    Status = "Pending",
                };
                _requestRepo.Insert(request);
                _requestRepo.Commit();

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
                Request existedRequest = _requestRepo.GetByID(key);
                if (existedRequest == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }
                existedRequest.Status = "Denied";
                _requestRepo.Update(existedRequest);
                _requestRepo.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetRequest Get(int key)
        {
            try
            {
                Request request = _requestRepo.GetByID(key);
                if (request == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }
                return _mapper.Map<GetRequest>(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetRequest> GetAllById(int key)
        {
            try
            {
                List<GetRequest> list = _mapper.Map<List<GetRequest>>(_requestRepo.GetAll().Where(r => r.StudentId == key || r.LecturerId == key));
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int key, UpdateRequest update)
        {
            try
            {
                var checkStudentId = _AccountRepo.GetByID(update.StudentId);
                var checkLecturerId = _AccountRepo.GetByID(update.LecturerId);
                var checkSubjectId = _subjectRepo.GetByID(update.SubjectId);
                if (checkStudentId == null)
                {
                    throw new Exception("Account Id does not exist in the system.");
                }

                if (checkLecturerId == null)
                {
                    throw new Exception("Lecturer Id does not exist in the system.");
                }

                if (checkSubjectId == null)
                {
                    throw new Exception("Subject Id does not exist in the system.");
                }

                Request existedRequest = _requestRepo.GetByID(key);
                if (existedRequest == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                existedRequest.StudentId = update.StudentId;
                existedRequest.LecturerId = update.LecturerId;
                existedRequest.SubjectId = update.SubjectId;
                existedRequest.Status = update.Status;
                _requestRepo.Update(existedRequest);
                _requestRepo.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}