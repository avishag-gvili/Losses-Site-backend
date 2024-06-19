using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Project.Common.Dto;
using Project.Repository.Entities;
using Project.Repository.Repository;
using Project.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.WebApi.Controlles
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService<UserDto> service;
        private readonly ISendEmailService service1;
        private readonly IConfiguration config;
        public UserController(IUserService<UserDto> service,IConfiguration config, ISendEmailService service1)
        {
            this.service = service;
            this.service1 = service1;
            this.config = config;
        }
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<UserDto> Get(int id)
        {
            return await service.GetByIdAsync(id);
        }
        [HttpPost("signIn")]
        public async Task<UserDto> Post(UserDto user)
        {
            return await service.AddAsync(user);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<UserDto> Put(int id,UserDto user)
        {
           return await service.UpdateAsync(id,user);
        }
        [HttpPut()]
        public async Task<UserDto> Put([FromBody] UserLogin userLogin)
        {
            return await service.UpdateAsync(userLogin.Email,userLogin.Password);
        }
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<UserDto> Delete(int id)
        {
            return await service.DeleteByIdAsync(id);
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDetails>> LogIn([FromBody] UserLogin userLogin)
        {
            var user = await Authenticate(userLogin);
            if (user != null)
            {
                var token = Generate(user);
                var userDetails = new UserDetails
                {
                    Id=user.Id,
                    UserName = user.Name, // assuming user object contains UserName and Phone properties
                    Phone = user.Phone,
                    Token = token
                };
                return Ok(userDetails);
            }
            return NoContent();
        }
        private string Generate(UserDto user)
        {
            //מפתח להצפנה
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            //אלגוריתם להצפנה
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.Name),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.OtherPhone,user.Phone),
            new Claim(ClaimTypes.UserData,user.Password)
            };
            var token = new JwtSecurityToken(config["Jwt:Issuer"],config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<UserDto?> Authenticate(UserLogin userLogin)
        {
            return (await service.GetAllUsersAsync()).FirstOrDefault(
                user => userLogin.GetType().GetProperties().All(prop => prop.GetValue(userLogin)?.ToString() ==
                user.GetType().GetProperty(prop.Name)?.GetValue(user)?.ToString()));
        }

        [HttpGet]
        [Authorize]
        public UserDto? GetByToken()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaim = identity.Claims;
                return new UserDto()
                {
                    Name = userClaim.FirstOrDefault(x => x.Type == ClaimTypes.Name)!.Value,
                    Email = userClaim.FirstOrDefault(x => x.Type == ClaimTypes.Email)!.Value,
                    Id = int.Parse(userClaim.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value),
                    Password = userClaim.FirstOrDefault(x => x.Type == ClaimTypes.UserData)!.Value,
                    Phone = userClaim.FirstOrDefault(x => x.Type == ClaimTypes.OtherPhone)!.Value,
                };
            }
            return null;
        }

        private string GenerateRandomPassword()
        {
            // יצירת סיסמה חדשה רנדומלית, לדוגמה:
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] newPassword = new char[8];
            for (int i = 0; i < newPassword.Length; i++)
            {
                newPassword[i] = chars[random.Next(chars.Length)];
            }
            return new string(newPassword);
        }

        private UserDto IsExistEmail(string email)
        {
            List<UserDto> users = service.GetAllUsersAsync().Result.ToList();
            foreach (var item in users)
            {
                if(item.Email==email)
                    return item;
            }
            return null;
        }

        [HttpGet("forgetPassword")]
        public string ForgetPassword([FromQuery(Name = "email")] string email)
        {
            UserDto user = IsExistEmail(email);
            if (user != null)
            {
                string password = GenerateRandomPassword();
                service1.SendEmail(email, "hello", password, "An email from chayki and Avishag's project");
                return password;
            }
            return null;
        }
    }
}
