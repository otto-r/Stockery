using Autofac;
using Stockery.ViewModel;

namespace Stockery.Startup
{
    public class Bootstrapper
    {
        public IContainer Boostrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<StockDataService>().As<IStockDataService>();

            return builder.Build(); 
        }
    }
}
