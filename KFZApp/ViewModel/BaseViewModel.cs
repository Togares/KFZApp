using Utility;
using KFZApp.View;
using BusinessLogic;

namespace KFZApp.ViewModel
{
    class BaseViewModel : BindableBase
    {
        protected IView View { get; set; }
        protected IModel Model { get; set; }
    }
}
