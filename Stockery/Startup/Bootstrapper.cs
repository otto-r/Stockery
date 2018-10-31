using Autofac;
using Stockery.DataAccess;
using Stockery.ViewModel;

namespace Stockery.Startup
{
    public class Bootstrapper
    {
        public IContainer Boostrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<StockDbContext>().AsSelf();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<StockDataService>().As<IStockDataService>();

            return builder.Build(); 
        }
    }
}
