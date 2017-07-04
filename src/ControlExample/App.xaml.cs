// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   Interaction logic for App.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ControlExample
{
    using System.Windows;
    using ControlExample.IoC;
    using Ninject;

    /// <summary>
    /// Composition Root for ControlExample
    /// </summary>  
    public partial class App : Application
    {
        private IKernel container;

        /// <summary>
        /// Startup tasks
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Current.MainWindow.Show();
        }

        /// <summary>
        /// Bootstraps the IoC container
        /// </summary>
        private void ConfigureContainer()
        {
            this.container = new StandardKernel(new TrafficRegistry());
        }

        /// <summary>
        /// Set up the main window
        /// </summary>
        private void ComposeObjects()
        {
            Current.MainWindow = this.container.Get<MainWindow>();
        }
    }
}