using Autofac;
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
            Container = builder.Build();
        }

        public static Authenticator CreateAuthenticator(IConfiguration configuration)
        {
            return Container.Resolve<Authenticator>(new TypedParameter(typeof(IConfiguration), configuration));
        }
    }
}