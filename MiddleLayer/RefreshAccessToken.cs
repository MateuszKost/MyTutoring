//using DataAccessLayer;
//using DataEntities;
//using Microsoft.Extensions.Configuration;
//using Models;
//using MyTutoring.MiddleLayer.Authenticators;
//using MyTutoring.Services.TokenValidators;
//using Services;

//namespace MiddleLayer
//{
//    public class RefreshAccessToken
//    {
//        private IUnitOfWork _uow;
//        private readonly Authenticator _authenticator;
//        private readonly RefreshTokenValidator _refreshTokenValidator;
//        public RefreshAccessToken(IUnitOfWork uow, Authenticator authenticator, IConfiguration configuration)
//        {
//            _uow = uow;
//            _authenticator = authenticator;
//            _refreshTokenValidator = ServicesFactory.CreateRefreshTokenValidator(configuration);
//        }

//        public async void Refresh(RefreshRequest refreshRequest)
//        {
//            if (refreshRequest == null)
//            {
//                return BadRequest("Invalid client request");
//            }

//            bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshRequest.RefreshToken);
//            if (!isValidRefreshToken)
//            {
//                return BadRequest("Invalid refresh token");
//            }

//            UserRefreshToken? userRefreshToken = await _uow.UserRefreshTokenRepo.SingleOrDefaultAsync(rt => rt.Token == refreshRequest.RefreshToken);
//            if (userRefreshToken == null)
//            {
//                return NotFound("Invalid refresh token");
//            }

//            User? user = await _uow.UserRepo.SingleOrDefaultAsync(u => u.Id == userRefreshToken.UserId);
//            if (user == null)
//            {
//                return NotFound("User not found");
//            }

//            LoginResult response = await _authenticator.RefreshAccessToken(user, userRefreshToken.Token, _uow);
//            return Ok(response); ;
//        }
//    }
//}