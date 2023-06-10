using DataBulkAPI.DataRepository;
using DataBulkAPI.Models;
using DataBulkAPI.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace DataBulkAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {

#pragma warning disable CS8604 // Possible null reference return.



        private readonly IConfiguration _config;
        private readonly DataBulkDbContext _db;


        public AccountController(DataBulkDbContext db,
            IConfiguration config)
        {
            _db = db;
            _config = config;
        }

     










        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserRequestModel newUser)
        {



            if (ModelState.IsValid)
            {
                try
                {
                  
                    var inUseEmail = _db.Users.FirstOrDefault(e => e.EmailAddress == newUser.Email);
                    if (inUseEmail != null)
                    {
                        ModelState.AddModelError("", "Email is in use");
                        return BadRequest(ModelState);
                    }
                    var user = new UserModel
                    {

                        Username = newUser.Email,
                        EmailAddress = newUser.Email,
                        Password = newUser.Password,
                    
                        Role = "Default"
                        };
                        await _db.Users.AddAsync(user);
                        await _db.SaveChangesAsync();   
                        return Ok($"User added {newUser.Email}");
                    
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }




        private UserModel Authenticate(UserLoginRequestModel userlogin)
        {

           
            var currentUser=_db.Users.FirstOrDefault(u=>u.Username.ToLower() == userlogin.Username.ToLower()&&u.Password== userlogin.Password);
            if (currentUser != null) { return currentUser; } return null;
            
     
        }




        private string Generate(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            {
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                new Claim(ClaimTypes.Email,user.EmailAddress),
                new Claim(ClaimTypes.Role,user.Role)
                };



                var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"],
                  claims,
                  expires: DateTime.Now.AddMinutes(15),
                  signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(UserLoginRequestModel userlogin)
        {
            try
            {
            if (ModelState.IsValid)
            {


                var user = Authenticate(userlogin);
                if (user != null)
                {
                    var token = Generate(user);
                    return Ok(token);
                }
                return NotFound("User not exist");
            }
            return NotFound(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

      
    }
}
