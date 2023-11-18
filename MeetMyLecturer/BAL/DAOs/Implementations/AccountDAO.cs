using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Accounts;
using BAL.DTOs.Authentications;
using DAL.Models;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BAL.DAOs.Implementations
{
    public class AccountDAO : IAccountDAO
    {
        private AccountRepository _AccountRepo;
        private SubjectRepository _SubjectRepo;
        private IMapper _mapper;

        public AccountDAO(IAccountRepository accountRepo, ISubjectRepository subjectRepo, IMapper mapper)
        {
            _AccountRepo = (AccountRepository)accountRepo;
            _SubjectRepo = (SubjectRepository)subjectRepo;
            _mapper = mapper;
        }

        public void Create(CreateAccount create)
        {
            try
            {
                Account account = new Account()
                {
                    Username = create.Username,
                    Email = create.Email,
                    Password = create.Password,
                    Fullname = create.Fullname,
                    Dob = create.Dob,
                    Role = create.Role,
                    Status = "Active"
                };

                int checkRole = CheckMailForRole(create.Email);
                if (checkRole == 0)
                {
                    account.Role = "Admin";
                }
                else if (checkRole == 1)
                {
                    account.Role = "Lecturer";
                }
                else if (checkRole == 2)
                {
                    account.Role = "Student";
                }

                _AccountRepo.Insert(account);
                _AccountRepo.Commit();

                if (checkRole == 1)
                {
                    List<Subject> listSubject = new List<Subject>();
                    //var listSubjectId = create.SubjectId;
                    if (create.SubjectId != null)
                    {
                        foreach (var id in create.SubjectId)
                        {
                            var checkId = _SubjectRepo.GetByID(id);
                            if (checkId == null)
                            {
                                throw new Exception("Subject does not exist in the system.");
                            }
                            listSubject.Add(checkId);
                        }
                        foreach (var item in listSubject)
                        {
                            item.Lecturers.Add(account);
                            _SubjectRepo.Update(item);
                            _SubjectRepo.Commit();
                        }
                    }
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
                Account existedAccount = _AccountRepo.GetByID(key);
                if (existedAccount == null)
                {
                    throw new Exception("Account does not exist in the system.");
                }

                existedAccount.Status = "Unactive";
                _AccountRepo.Update(existedAccount);
                _AccountRepo.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetAccount Get(int key)
        {
            try
            {
                Account account = _AccountRepo.GetByID(key);
                if (account == null)
                {
                    throw new Exception("Account does not exist in the system.");
                }
                return _mapper.Map<GetAccount>(account);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetAccount> GetAll()
        {
            try
            {
                List<GetAccount> listAccount = _mapper.Map<List<GetAccount>>(_AccountRepo.GetAll());
                return listAccount;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int key, UpdateAccount update)
        {
            try
            {
                Account existedAccount = _AccountRepo.GetByID(key);
                if (existedAccount == null)
                {
                    throw new Exception("Account does not exist in the system.");
                }

                existedAccount.Username = update.Username;
                existedAccount.Email = update.Email;
                existedAccount.Password = update.Password;
                existedAccount.Dob = update.Dob;
                existedAccount.Fullname = update.Fullname;
                existedAccount.Role = update.Role;
                existedAccount.Subjects.Clear();
                _AccountRepo.Update(existedAccount);
                _AccountRepo.Commit();

                if (existedAccount.Role == "Lecturer")
                {
                    List<Subject> listSubject = new List<Subject>();
                    if (update.SubjectId != null)
                    {
                        foreach (var id in update.SubjectId)
                        {
                            var checkId = _SubjectRepo.GetByID(id);
                            if (checkId == null)
                            {
                                throw new Exception("Subject does not exist in the system.");
                            }
                            listSubject.Add(checkId);
                        }
                    }
                    foreach (var item in listSubject)
                    {
                        item.Lecturers.Add(existedAccount);
                        _SubjectRepo.Update(item);
                        _SubjectRepo.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetAccount Login(AuthenticationAccount authenAccount, JwtAuth jwtAuth)
        {
            try
            {
                Account existedAccount = _AccountRepo.GetAll()
                    .FirstOrDefault(x => x.Email == authenAccount.Email && x.Password.Equals(authenAccount.Password));

                if (existedAccount == null)
                {
                    throw new Exception("Email or Password is invalid.");
                }
                GetAccount getAccount = _mapper.Map<GetAccount>(existedAccount);
                return GenerateToken(getAccount, jwtAuth);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private GetAccount GenerateToken(GetAccount getAccount, JwtAuth jwtAuth)
        {
            try
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAuth.Key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new ClaimsIdentity(new[] {
                 new Claim(JwtRegisteredClaimNames.Sub, getAccount.Id.ToString()),
                 new Claim(JwtRegisteredClaimNames.Email, getAccount.Email),
                 new Claim(JwtRegisteredClaimNames.Name, getAccount.Username),
                 new Claim("Role", getAccount.Role),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             });

                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = credentials,
                };

                var token = jwtTokenHandler.CreateToken(tokenDescription);
                string accessToken = jwtTokenHandler.WriteToken(token);

                getAccount.AccessToken = accessToken;

                return getAccount;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CheckMailForRole(string? email)
        {
            bool check = Regex.IsMatch(email, @"^(.+)(sa|se|ss)\d{6}@fpt.edu.vn$");
            if (check)
            {
                return 2;
            } 
            else if (email.Equals("admin@fpt.edu.vn"))
            {
                return 0;
            } 
            else
            {
                return 1;
            }
        }
    }
}
