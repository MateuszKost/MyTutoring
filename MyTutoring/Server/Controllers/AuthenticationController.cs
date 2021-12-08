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
using Services.PasswordGenerators;
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
        private readonly AccessTokenValidator _accessTokenValidator;
        private readonly PasswordGenerator _passwordGenerator;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
            _uow = DataAccessLayerFactory.CreateUnitOfWork();
            _passwordHasher = ServicesFactory.CreateBCryptPasswordHasher();
            _authenticator = MiddleLayerFactory.CreateAuthenticator(_configuration);
            _refreshTokenValidator = ServicesFactory.CreateRefreshTokenValidator(_configuration);
            _accessTokenValidator = ServicesFactory.CreateAccessTokenValidator(_configuration);
            _passwordGenerator = ServicesFactory.CreatePasswordGenerator();
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register([FromBody] RegisterModel registerModel)
        {
            User? user = await _uow.UserRepo.SingleOrDefaultAsync(u => u.Email == registerModel.Email);
            if (user != null)
            {
                return BadRequest("User with that email exists");
            }
            UserRole userRole = await _uow.UserRoleRepo.SingleOrDefaultAsync(ur => ur.Name == registerModel.AccountType);
            if (userRole == null)
            {
                return BadRequest("Something gone wrong. Call user service!");
            }

            string password = _passwordGenerator.GeneratePassword();
            string salt = _passwordHasher.GenerateSalt();

            UserIdentity userIdentity = await _uow.UserIdentityRepo.SingleOrDefaultAsync(ui => ui.Salt == salt);
            if (userIdentity != null)
            {
                while (userIdentity.Salt == salt)
                {
                    salt = _passwordHasher.GenerateSalt();
                    userIdentity = await _uow.UserIdentityRepo.SingleOrDefaultAsync(ui => ui.Salt == salt);
                    if(userIdentity == null)
                    {
                        break;
                    }
                }
            }

            // send mail with password to user

            string pepper = _configuration.GetValue<string>("PasswordSettings:Pepper");
            string passwordHash = _passwordHasher.Hash(password + pepper, salt);

            User newUser = new User()
            {
                Email = registerModel.Email,
                Password = passwordHash,
                RoleId = userRole.Id,
                CreationDate = DateTime.Now
            };

            await _uow.UserRepo.AddAsync(newUser);
            await _uow.CompleteAsync();

            User createdUser = await _uow.UserRepo.SingleOrDefaultAsync(u => u.Email == registerModel.Email);

            UserIdentity createdUserIdentity = new UserIdentity()
            {
                UserId = createdUser.Id,
                Salt = salt
            };

            await _uow.UserIdentityRepo.AddAsync(createdUserIdentity);
            await _uow.CompleteAsync();

            if (registerModel.AccountType == "student")
            {
                Student student = new Student()
                {
                    UserId = createdUser.Id,
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    PhoneNumber = Int32.Parse(registerModel.PhoneNumber)
                };

                await _uow.StudentRepo.AddAsync(student);
                await _uow.CompleteAsync();                
            }
            else if (registerModel.AccountType == "teacher")
            {
                Teacher teacher = new Teacher()
                {
                    UserId = createdUser.Id,
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    PhoneNumber = Int32.Parse(registerModel.PhoneNumber)
                };

                await _uow.TeacherRepo.AddAsync(teacher);
                await _uow.CompleteAsync();
            }

            return Ok($"Poprawnie utworzono konto.");
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

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
        {
            if (refreshRequest == null)
            {
                return BadRequest("Invalid client request");
            }

            bool isValidAccessToken = _accessTokenValidator.Validate(refreshRequest.AccessToken);
            if (!isValidAccessToken)
            {
                return BadRequest("Invalid access token");
            }

            string userId = HttpContext.User.FindFirstValue("id");

            UserRefreshToken? userRefreshToken = await _uow.UserRefreshTokenRepo.SingleOrDefaultAsync(rt => rt.UserId == Guid.Parse(userId));
            if (userRefreshToken == null)
            {
                return NotFound("Invalid refresh token");
            }
            bool isValidRefreshToken = _refreshTokenValidator.Validate(userRefreshToken.Token);

            if (!isValidRefreshToken)
            {
                return BadRequest("Invalid access token");
            }

            User? user = await _uow.UserRepo.SingleOrDefaultAsync(u => u.Id == Guid.Parse(userId));
            if (user == null)
            {
                return NotFound("User not found");
            }

            LoginResult response = await _authenticator.RefreshAccessToken(user, userRefreshToken.Token, _uow);
            return Ok(response); 
        }

        [Authorize]
        [HttpDelete("logout")]
        public async Task<IActionResult> Logout()
        {
            string rawUserId = HttpContext.User.FindFirstValue("id");
            if (!Guid.TryParse(rawUserId, out Guid userId))
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
