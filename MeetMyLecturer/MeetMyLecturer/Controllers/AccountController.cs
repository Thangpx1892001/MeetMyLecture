using BAL.Authentications;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Accounts;
using BAL.DTOs.Authentications;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using DAL.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MeetMyLecturer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IAccountDAO _accountDAO;
        private IOptions<JwtAuth> _jwtAuthOptions;

        public AccountController(IAccountDAO accountDAO, IOptions<JwtAuth> jwtAuthOptions)
        {
            _accountDAO = accountDAO;
            _jwtAuthOptions = jwtAuthOptions;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetAccount> accounts = _accountDAO.GetAll();
                return Ok(new
                {
                    Data = accounts
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                GetAccount account = _accountDAO.Get(id);
                return Ok(new
                {
                    Data = account
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateAccount create)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _accountDAO.Create(create);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateAccount update)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _accountDAO.Update(id, update);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _accountDAO.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public IActionResult Post([FromBody] AuthenticationAccount authenAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                GetAccount getAccount = _accountDAO.Login(authenAccount, _jwtAuthOptions.Value);
                return Ok(new
                {
                    Data = getAccount
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            try
            {
                HttpContext.SignOutAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }
        [HttpGet("GoogleLogin")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("GoogleResponse")]
        public async Task<IActionResult> GoogleResponse()
        {
            try
            {
                var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                var claims = result.Principal.Identities.FirstOrDefault()
                    .Claims.Select(claim => new
                    {
                        claim.Issuer,
                        claim.OriginalIssuer,
                        claim.Type,
                        claim.Value
                    });

                var authenAccount = new AuthenticationAccount
                {
                    Email = claims.FirstOrDefault(c => c.Type == "emailaddress").Value,
                    Password = "1",
                };
                GetAccount getAccount = _accountDAO.Login(authenAccount, _jwtAuthOptions.Value);
                return Ok(new
                {
                    Data = getAccount
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }
    }
}
