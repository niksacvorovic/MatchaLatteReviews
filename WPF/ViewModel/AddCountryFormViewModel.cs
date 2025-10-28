using MatchaLatteReviews.Application.Services;
using MatchaLatteReviews.Application.Utilities;
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MatchaLatteReviews.WPF.ViewModel
{
    public class AddCountryFormViewModel : INotifyPropertyChanged
    {
        private readonly CountryService _countryService;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public AddCountryFormViewModel(Action close)
        {
            _countryService = Injector.CreateInstance<CountryService>();
            AddCountryCommand = new RelayCommand(_ => AddCountry());
            AllCountries = new ObservableCollection<Country>(_countryService.GetAll());
            _close = close;
        }

        private Action _close;
        public ICommand AddCountryCommand { get; }
        public ObservableCollection<Country> AllCountries { get; set; }

        public string Name { get; set; }

        private void AddCountry()
        {
            Country newCountry = new Country(Name);
            try
            {
                _countryService.Add(newCountry);
                MessageHelper.ShowInfo($"Country of {newCountry.Name} added!");
                _close();
            }
            catch (Exception exception)
            {
                MessageHelper.ShowError($" failed: {exception.Message}");
            }
        }
    }
}
