using System.Windows;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;

namespace Practice
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.Register<PracticeViews.SimpleCalc.SimpleCalcView, PracticeViewModels.Calc.CalcViewModel>();
            ViewModelLocationProvider.Register<PracticeViews.DropDownCalc.DropDownCalcView, PracticeViewModels.Calc.CalcViewModel>();
        }
    }

}
