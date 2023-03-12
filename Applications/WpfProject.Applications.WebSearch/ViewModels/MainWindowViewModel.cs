namespace WpfProject.Applications.WebSearch.ViewModels
{
    using Caliburn.Micro;

    public class MainWindowViewModel : Conductor<object>
    {
        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            this.ActivateItemAsync(IoC.Get<SearchViewModel>());
        }
    }
}
