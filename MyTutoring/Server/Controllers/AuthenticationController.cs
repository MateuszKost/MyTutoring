using DataAccessLayer;
using DataEntities;
using Microsoft.AspNetCore.Mvc;
using Models;
using MyTutoring.Server.Services.Authenticators;
using MyTutoring.Server.Services.PasswordHasher;
using MyTutoring.Server.Services.TokenGenerators;
using MyTutoring.Server.Services.TokenValidators;

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

        public AuthenticationController(IConfiguration configuration, IPasswordHasher passwordHasher, RefreshTokenValidator refreshTokenValidator, Authenticator authenticator)
        {
            _uow = DataAccessLayerFactory.CreateUnitOfWork();
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            _refreshTokenValidator = refreshTokenValidator;
            _authenticator = authenticator;
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
                    AuthenticatedUserResponse response = await _authenticator.Authenticate(user, _uow);
                    return Ok(response);
                }
            }

            return Unauthorized();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
        {
            if(refreshRequest == null)
            {
                return BadRequest("Invalid client request");
            }

            bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshRequest.RefreshToken);
            if(!isValidRefreshToken)
            {
                return BadRequest("Invalid refresh token");
            }

            UserRefreshToken? userRefreshToken = await _uow.UserRefreshTokenRepo.SingleOrDefaultAsync(rt => rt.Token == refreshRequest.RefreshToken);
            if(userRefreshToken == null)
            {
                return NotFound("Invalid refresh token");
            }
            _uow.UserRefreshTokenRepo.Remove(userRefreshToken);
            await _uow.CompleteAsync();

             User? user = await _uow.UserRepo.SingleOrDefaultAsync(u => u.Id == userRefreshToken.UserId);
            if(user == null)
            {
                return NotFound("User not found");
            }

            AuthenticatedUserResponse response = await _authenticator.Authenticate(user, _uow);
            return Ok(response); ;
        }
    }
}
