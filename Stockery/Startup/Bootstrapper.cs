using Autofac;
using Prism.Events;
using Stockery.Data;
using Stockery.Data.LookUps;
using Stockery.Data.Repositories;
using Stockery.DataAccess;
using Stockery.View.Services;
using Stockery.ViewModel;

namespace Stockery.Startup
{
    public class Bootstrapper
    {
        public IContainer Boostrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<StockDbContext>().AsSelf();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<MainWindow>().AsSelf();

            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();

            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<StockDetailViewModel>().As<IStockDetailViewModel>();

            builder.RegisterType<LookUpDataService>().AsImplementedInterfaces();
            builder.RegisterType<StockRepository>().As<IStockRepository>();

            return builder.Build();
        }
    }
}
