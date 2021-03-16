using KFZApp.View;
using BusinessLogic;
using System.Windows;
using KFZApp.ViewModel;

namespace KFZApp
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            IView window = new MainWindow();
            KFZModel model = new KFZModel();
            MainWindowViewModel viewModel = new MainWindowViewModel(window, model);
        }
    }
}
