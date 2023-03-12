namespace WpfProject.Applications.WebSearch.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;
    using WpfProject.Services;

    public class SearchViewModel : BaseViewModel
    {
        private readonly IRequestService requestService;
        private string result;
        private string location;
        private string selectedItem;
        private bool buttonAvailable;
        private ObservableCollection<string> suggestions;
        private bool isDropDownOpen;

        public SearchViewModel(IRequestService requestService)
        {
            this.requestService = requestService;

            this.SearchCommand = new RelayCommand(e => true, e => this.SearchElement());
            this.TextChangedCommand = new RelayCommand(e => true, e => this.GetSuggestionList());
            this.ButtonAvailable = true;
            this.IsDropDownOpen = false;
        }

        public ICommand SearchCommand { get; set; }

        public ICommand TextChangedCommand { get; set; }

        public bool ButtonAvailable
        {
            get => this.buttonAvailable;
            set
            {
                this.buttonAvailable = value;
                this.NotifyOfPropertyChange(() => this.ButtonAvailable);
            }
        }

        public bool IsDropDownOpen
        {
            get => this.isDropDownOpen;
            set
            {
                this.isDropDownOpen = value;
                this.NotifyOfPropertyChange(() => this.IsDropDownOpen);
            }
        }

        public string Location
        {
            get => this.location;
            set
            {
                this.location = value;
                this.NotifyOfPropertyChange(() => this.Location);
                this.GetSuggestionList();
            }
        }

        public string SelectedItem
        {
            get => this.selectedItem;
            set
            {
                this.selectedItem = value;
                this.NotifyOfPropertyChange(() => this.SelectedItem);
                this.GetSuggestionList();
            }
        }

        public string Result
        {
            get => this.result;
            set
            {
                this.result = value;
                this.NotifyOfPropertyChange(() => this.Result);
            }
        }

        public ObservableCollection<string> Suggestions
        {
            get => this.suggestions;
            set
            {
                this.suggestions = value;
                this.NotifyOfPropertyChange(() => this.Suggestions);
            }
        }

        private void SearchElement()
        {
            try
            {
                this.ButtonAvailable = false;
                this.Result = string.Empty;
                var results = this.requestService.GetAddressCandidates(this.Location);
                foreach (var res in results)
                {
                    this.Result += $"{res}\r\n";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.ButtonAvailable = true;
            }
        }

        private void GetSuggestionList()
        {
            this.Suggestions = this.requestService.GetSuggestions(this.Location);
            this.IsDropDownOpen = this.Suggestions.Count > 0;
        }
    }
}
