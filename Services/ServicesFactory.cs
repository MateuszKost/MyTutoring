using Autofac;
using Microsoft.Extensions.Configuration;
using MyTutoring.Services.PasswordHasher;
using MyTutoring.Services.TokenGenerators;
using MyTutoring.Services.TokenValidators;

namespace Services
{
    public class ServicesFactory
    {
        private static IContainer Container { get; }
        static ServicesFactory()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<BCryptPasswordHasher>().As<IPasswordHasher>().SingleInstance();
            builder.RegisterType<AccessTokenGenerator>().SingleInstance();
            builder.RegisterType<RefreshTokenGenerator>().SingleInstance();
            builder.RegisterType<RefreshTokenValidator>().SingleInstance();
            Container = builder.Build();
        }

        public static IPasswordHasher CreateBCryptPasswordHasher()
        {
            return Container.Resolve<IPasswordHasher>();
        }
        public static AccessTokenGenerator CreateAccessTokenGenerator(IConfiguration configuration)
        {
            return Container.Resolve<AccessTokenGenerator>(new TypedParameter(typeof(IConfiguration), configuration));
        }
        public static RefreshTokenGenerator CreateRefreshTokenGenerator(IConfiguration configuration)
        {
            return Container.Resolve<RefreshTokenGenerator>(new TypedParameter(typeof(IConfiguration), configuration));
        }
        public static RefreshTokenValidator CreateRefreshTokenValidator(IConfiguration configuration)
        {
            return Container.Resolve<RefreshTokenValidator>(new TypedParameter(typeof(IConfiguration), configuration));
        }
    }
}