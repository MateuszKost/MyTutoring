using DataAccessLayer;
using DataEntities;
using Models;
using MyTutoring.Server.Services.TokenGenerators;

#nullable disable

namespace MyTutoring.Server.Services.Authenticators
{
    public class Authenticator
    {
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;

        public Authenticator(AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
        }

        public async Task<LoginResult> Authenticate(User user, IUnitOfWork unitOfWork)
        {
            UserRole userRole = await unitOfWork.UserRoleRepo.SingleOrDefaultAsync(role => role.Id == user.RoleId);
            string accessToken = _accessTokenGenerator.GenerateToken(user, userRole);
            string refreshToken = _refreshTokenGenerator.GenerateToken();

            UserRefreshToken userRefreshToken = await unitOfWork.UserRefreshTokenRepo.SingleOrDefaultAsync(urt => urt.UserId == user.Id);

            if (userRefreshToken != null)
            {
                userRefreshToken.Token = refreshToken;
                unitOfWork.UserRefreshTokenRepo.Update(userRefreshToken);
            }
            else
            {
                userRefreshToken = new UserRefreshToken
                {
                    Token = refreshToken,
                    UserId = user.Id
                };
                await unitOfWork.UserRefreshTokenRepo.AddAsync(userRefreshToken);
            }
            await unitOfWork.CompleteAsync();

            return new LoginResult
            {
                Successful = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<LoginResult> RefreshAccessToken(User user, string refreshToken, IUnitOfWork unitOfWork)
        {
            UserRole userRole = await unitOfWork.UserRoleRepo.SingleOrDefaultAsync(role => role.Id == user.RoleId);
            string accessToken = _accessTokenGenerator.GenerateToken(user, userRole);

            return new LoginResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
