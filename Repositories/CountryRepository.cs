using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.Domain.RepositoryInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchaLatteReviews.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly IPersistenceContext _context;
        private readonly JsonSerializerSettings _serializerSettings;

        public CountryRepository(IPersistenceContext context)
        {
            _context = context;
            _serializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                TypeNameHandling = TypeNameHandling.Auto
            };
        }

        public Country Add(Country country)
        {
            var countries = GetAll().ToList();

            if (countries.Any(g => g.Name == country.Name))
            {
                throw new InvalidOperationException($"Country with name {country.Name}  already exists.");
            }
            countries.Add(country);
            SaveAll(countries);
            return country;
        }

        public void DeleteById(string id)
        {
            var countries = GetAll().ToList();
            var country = countries.FirstOrDefault(g => g.Id == id);

            if (country != null)
            {
                countries.Remove(country);
                SaveAll(countries);
            }
        }

        public IEnumerable<Country> GetAll()
        {
            string content = _context.LoadContent();
            var countries = JsonConvert.DeserializeObject<List<Country>>(content, _serializerSettings)
                          ?? new List<Country>();
            return countries;
        }

        public Country GetById(string id)
        {
            return GetAll().FirstOrDefault(g => g.Id == id);
        }

        public void SaveAll(IEnumerable<Country> countries)
        {
            string content = JsonConvert.SerializeObject(countries, Formatting.Indented, _serializerSettings);
            _context.SaveContent(content);
        }

        public Country Update(Country updatedCountry)
        {
            var countries = GetAll().ToList();
            var existingCountry = countries.FirstOrDefault(g => g.Id == updatedCountry.Id);

            if (existingCountry == null)
            {
                throw new InvalidOperationException($"Country with ID {updatedCountry.Id} not found.");
            }

            countries.Remove(existingCountry);
            countries.Add(updatedCountry);
            SaveAll(countries);

            return updatedCountry;
        }

    }
}
