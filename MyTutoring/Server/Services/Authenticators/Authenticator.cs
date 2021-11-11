using DataAccessLayer;
using DataEntities;
using Models;
using MyTutoring.Server.Services.TokenGenerators;

namespace MyTutoring.Server.Services.Authenticators
{
    public class Authenticator
    {
        //private readonly IUnitOfWork _uow;
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;

        public Authenticator(AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator)
        {
            //_uow = DataAccessLayerFactory.CreateUnitOfWork();
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
        }

        public async Task<AuthenticatedUserResponse> Authenticate(User user, IUnitOfWork unitOfWork)
        {
            IUnitOfWork _uow = unitOfWork;
            UserRole? userRole = await _uow.UserRoleRepo.SingleOrDefaultAsync(role => role.Id == user.RoleId);
            string accessToken = _accessTokenGenerator.GenerateToken(user, userRole);
            string refreshToken = _refreshTokenGenerator.GenerateToken();
            UserRefreshToken? userRefreshToken2 = await _uow.UserRefreshTokenRepo.SingleOrDefaultAsync(urt => urt.UserId == user.Id);
            UserRefreshToken userRefreshToken = new UserRefreshToken
            {
                Token = refreshToken,
                UserId = user.Id
            };
            await _uow.UserRefreshTokenRepo.AddAsync(userRefreshToken);
            await _uow.CompleteAsync();

            return new AuthenticatedUserResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
