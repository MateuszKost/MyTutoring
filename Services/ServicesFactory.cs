using Autofac;
using Microsoft.Extensions.Configuration;
using MyTutoring.Services.PasswordHasher;
using MyTutoring.Services.TokenGenerators;
using MyTutoring.Services.TokenValidators;
using Services.EmailService;
using Services.PasswordGenerators;

namespace Services
{
    public class ServicesFactory
    {
        private static IContainer Container { get; }
        static ServicesFactory()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<BCryptPasswordHasher>().As<IPasswordHasher>();
            builder.RegisterType<AccessTokenGenerator>().SingleInstance();
            builder.RegisterType<RefreshTokenGenerator>().SingleInstance();
            builder.RegisterType<RefreshTokenValidator>().SingleInstance();
            builder.RegisterType<AccessTokenValidator>().SingleInstance();
            builder.RegisterType<PasswordGenerator>();
            builder.RegisterType<EmailSender>().As<IEmailSender>();
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
        public static AccessTokenValidator CreateAccessTokenValidator(IConfiguration configuration)
        {
            return Container.Resolve<AccessTokenValidator>(new TypedParameter(typeof(IConfiguration), configuration));
        }
        public static PasswordGenerator CreatePasswordGenerator()
        {
            return Container.Resolve<PasswordGenerator>();
        }
        public static IEmailSender CreateEmailSender(EmailConfiguration configuration)
        {
            return Container.Resolve<IEmailSender>(new TypedParameter(typeof(EmailConfiguration), configuration));
        }
    }
}