namespace KFZApp.View
{
    public interface IView
    {
        object DataContext { get; set; }
        void Show();
        void Close();
    }
}