using Autofac;
using DataAccessLayer.Mock;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class DataAccessLayerFactory
    {
        private static IContainer Container { get; set; }
        static DataAccessLayerFactory()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<DbContextOptions<MyTutoringContext>>();
            builder.RegisterType<MyTutoringContext>().As<IMyTutoringContext>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            Container = builder.Build();
        }

        public static void BuildContainerForTests()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<DbContextOptions<MyTutoringContext>>();
            builder.RegisterType<MockMyTutoringContext>().As<IMyTutoringContext>();
            builder.RegisterType<MockUnitOfWork>().As<IUnitOfWork>();
            Container = builder.Build();
        }

        public static IUnitOfWork CreateUnitOfWork()
        {
            return Container.Resolve<IUnitOfWork>();
        }
    }
}