using Autofac;
using DataAccessLayer;
using Microsoft.Extensions.Configuration;
using MyTutoring.MiddleLayer.Authenticators;

namespace MiddleLayer
{
    public class MiddleLayerFactory
    {
        private static IContainer Container { get; }
        static MiddleLayerFactory()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<Authenticator>().SingleInstance();
           // builder.RegisterType<RefreshAccessToken>();
            Container = builder.Build();
        }

        //public static RefreshAccessToken CreateRefreshAccessToken(IConfiguration configuration)
        //{
        //    return Container.Resolve<RefreshAccessToken>(new TypedParameter(typeof(IUnitOfWork), DataAccessLayerFactory.CreateUnitOfWork()),new TypedParameter(typeof(Authenticator), CreateAuthenticator(configuration)), new TypedParameter(typeof(IConfiguration), configuration));
        //}

        public static Authenticator CreateAuthenticator(IConfiguration configuration)
        {
            return Container.Resolve<Authenticator>(new TypedParameter(typeof(IConfiguration), configuration));
        }
    }
}