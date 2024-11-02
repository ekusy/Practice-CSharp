using System.Configuration;
using System.Data;
using System.Windows;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return this.Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ;
        }
    }

}
