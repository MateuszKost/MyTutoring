using DataAccessLayer;
using DataEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiddleLayer;
using Models;
using MyTutoring.MiddleLayer.Authenticators;
using MyTutoring.Services.PasswordHasher;
using MyTutoring.Services.TokenValidators;
using Services;
using System.Security.Claims;

namespace MyTutoring.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IConfiguration _configuration;
        private readonly Authenticator _authenticator;
        private readonly RefreshTokenValidator _refreshTokenValidator;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
            _uow = DataAccessLayerFactory.CreateUnitOfWork();
            _passwordHasher = ServicesFactory.CreateBCryptPasswordHasher();
            _authenticator = MiddleLayerFactory.CreateAuthenticator(_configuration);
            _refreshTokenValidator = ServicesFactory.CreateRefreshTokenValidator(_configuration);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest("Invalid client request");
            }
            User? user = await _uow.UserRepo.SingleOrDefaultAsync(u => u.Email == loginModel.Email);

            if (user != null)
            {
                UserIdentity userIdentity = await _uow.UserIdentityRepo.SingleOrDefaultAsync(ui => ui.UserId == user.Id);
                string salt = userIdentity.Salt;
                string pepper = _configuration.GetValue<string>("PasswordSettings:Pepper");
                string passwordHash = _passwordHasher.Hash(loginModel.Password + pepper, salt);

                if (user.Password == passwordHash)
                {
                    LoginResult response = await _authenticator.Authenticate(user, _uow);
                    return Ok(response);
                }
            }

            return BadRequest(new LoginResult { Successful = false, Error = "Username and password are invalid." });
        }

        [HttpGet("refresh")]
        [Authorize]
        public async Task<IActionResult> Refresh()
        {
            string userId = HttpContext.User.FindFirstValue("id");
            UserRefreshToken? userRefreshToken = await _uow.UserRefreshTokenRepo.SingleOrDefaultAsync(rt => rt.UserId == Guid.Parse(userId));

            if (userRefreshToken == null)
            {
                return BadRequest("Invalid client request");
            }

            bool isValidRefreshToken = _refreshTokenValidator.Validate(userRefreshToken.Token);
            if(!isValidRefreshToken)
            {
                return BadRequest("Invalid refresh token");
            }
                
            User? user = await _uow.UserRepo.SingleOrDefaultAsync(u => u.Id == userRefreshToken.UserId);
            if(user == null)
            {
                return NotFound("User not found");
            }

            LoginResult response = await _authenticator.RefreshAccessToken(user, userRefreshToken.Token, _uow);
            return Ok(response); ;
        }

        [Authorize]
        [HttpDelete("logout")]
        public async Task<IActionResult> Logout()
        {
            string rawUserId = HttpContext.User.FindFirstValue("id");
            if(!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }
            UserRefreshToken userRefreshToken = await _uow.UserRefreshTokenRepo.SingleOrDefaultAsync(urt => urt.UserId == userId);
            _uow.UserRefreshTokenRepo.Remove(userRefreshToken);
            await _uow.CompleteAsync();

            return Ok();
        }
    }
}
