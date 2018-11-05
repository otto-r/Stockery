using Autofac;
using Stockery.Startup;
using System;
using System.Windows;

namespace Stockery
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Boostrap();

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unexpected error occured." + Environment.NewLine + e.Exception, "Unexpected error");
            e.Handled = true;
        }
    }
}
