using DataAccessLayer;
using DataEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Services.PasswordHasher;

namespace MyTutoring.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration, IPasswordHasher passwordHasher)
        {
            _uow = DataAccessLayerFactory.CreateUnitOfWork();
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest("Invalid client request");
            }
            User? user = await _uow.UserRepo.SingleOrDefaultAsync(u => u.Email == loginModel.Email);      

            if (user != null)
            {
                UserIdentity? userIdentity = await _uow.UserIdentityRepo.SingleOrDefaultAsync(ui => ui.UserId == user.Id);
                string salt = userIdentity.Salt;
                string pepper = _configuration.GetValue<string>("PasswordSettings:Pepper");
                string passwordHash = _passwordHasher.Hash(loginModel.Password + pepper, salt);

                if (user.Password == passwordHash)
                {
                    UserRole? userRole = await _uow.UserRoleRepo.SingleOrDefaultAsync(role => role.Id == user.RoleId);
                    var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSettings:SecretKey"))), SecurityAlgorithms.HmacSha256);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, loginModel.Email),
                        new Claim(ClaimTypes.Role, userRole.Name)
                    };

                    var tokenOptions = new JwtSecurityToken(
                        issuer: "https://localhost:7120",
                        audience: "https://localhost:7120",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signingCredentials
                        );

                    var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    return Ok(new { Token = token });
                }
            }

            return Unauthorized();
        }
    }
}
