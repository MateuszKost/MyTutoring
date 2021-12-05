using Autofac;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MyTutoring.Client.Services;

namespace MyTutoring.Client
{
    public class ClientFactory
    {
        private static IContainer Container { get; }
        static ClientFactory()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<RefreshService>().As<IRefreshService>();
            Container = builder.Build();
        }

        public static IRefreshService CreateRefreshService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            return Container.Resolve<IRefreshService>(new TypedParameter(typeof(HttpClient), httpClient),
                new TypedParameter(typeof(AuthenticationStateProvider), authenticationStateProvider),
                new TypedParameter(typeof(ILocalStorageService), localStorage));
        }
    }
}