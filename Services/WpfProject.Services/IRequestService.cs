namespace WpfProject.Services
{
    using System.Collections.ObjectModel;

    public interface IRequestService
    {
        ObservableCollection<string> GetSuggestions(string name);

        ObservableCollection<string> GetAddressCandidates(string name);
    }
}
