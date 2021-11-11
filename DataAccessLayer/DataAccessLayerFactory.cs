using Autofac;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class DataAccessLayerFactory
    {
        private static IContainer Container { get; }
        static DataAccessLayerFactory()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<DbContextOptions<MyTutoringContext>>();
            builder.RegisterType<MyTutoringContext>().As<IMyTutoringContext>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            Container = builder.Build();
        }

        public static IUnitOfWork CreateUnitOfWork()
        {
            return Container.Resolve<IUnitOfWork>();
        }
    }
}