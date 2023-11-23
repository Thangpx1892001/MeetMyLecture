using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Subjects;
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
    public class SubjectDAO : ISubjectDAO
    {
        private SubjectRepository _SubjectRepo;
        private IMapper _mapper;

        public SubjectDAO(ISubjectRepository subjectRepo, IMapper mapper)
        {
            _SubjectRepo = (SubjectRepository)subjectRepo;
            _mapper = mapper;
        }

        public void Create(CreateSubject create)
        {
            try
            {
                var checkName = _SubjectRepo.GetAll().FirstOrDefault(s => s.Name == create.Name);
                var checkCode = _SubjectRepo.GetAll().FirstOrDefault(s => s.SubjectCode == create.SubjectCode);
                if (checkName != null)
                {
                    throw new Exception("Subject name already exist.");
                }
                if (checkCode != null)
                {
                    throw new Exception("Subject code already exist.");
                }
                Subject account = new Subject()
                {
                    SubjectCode = create.SubjectCode,
                    Name = create.Name,
                    CreatedAt = DateTime.Now,
                    Status = "Active",
                };
                _SubjectRepo.Insert(account);
                _SubjectRepo.Commit();
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
                Subject existedSubject = _SubjectRepo.GetByID(key);
                if (existedSubject == null)
                {
                    throw new Exception("Subject does not exist in the system.");
                }
                existedSubject.Status = "Unactive";
                _SubjectRepo.Update(existedSubject);
                _SubjectRepo.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetSubject Get(int key)
        {
            try
            {
                Subject subject = _SubjectRepo.GetByID(key);
                if (subject == null)
                {
                    throw new Exception("Subject does not exist in the system.");
                }
                return _mapper.Map<GetSubject>(subject);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetSubject> GetAll()
        {
            try
            {
                List<GetSubject> listSubject = _mapper.Map<List<GetSubject>>(_SubjectRepo.GetAll());
                return listSubject;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int key, UpdateSubject update)
        {
            try
            {
                var checkName = _SubjectRepo.GetAll().FirstOrDefault(s => s.Name == update.Name && s.Id != key && s.Status == "Active");
                var checkCode = _SubjectRepo.GetAll().FirstOrDefault(s => s.SubjectCode == update.SubjectCode && s.Id != key && s.Status == "Active");
                if (checkName != null)
                {
                    throw new Exception("Subject name already exist.");
                }
                if (checkCode != null)
                {
                    throw new Exception("Subject code already exist.");
                }
                Subject existedSubject = _SubjectRepo.GetByID(key);
                if (existedSubject == null)
                {
                    throw new Exception("Subject does not exist in the system.");
                }

                existedSubject.SubjectCode = update.SubjectCode;
                existedSubject.Name = update.Name;
                _SubjectRepo.Update(existedSubject);
                _SubjectRepo.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
