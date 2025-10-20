using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchaLatteReviews.Application.Services
{
    public class CountryService
    {
        private ICountryRepository _countryRepository;

        public CountryService()
        {
            _countryRepository = Injector.CreateInstance<ICountryRepository>();
        }

        public void Add(Country country)
        {
            var genres = _countryRepository.GetAll();
            if (genres.FirstOrDefault(g => g.Name.Equals(country.Name)) != null)
            {
                throw new InvalidOperationException("Country already exists!");
            }
            _countryRepository.Add(country);
        }

    }
}
