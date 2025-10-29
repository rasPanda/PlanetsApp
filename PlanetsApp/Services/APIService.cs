using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PlanetsApp.Models;

namespace PlanetsApp.Services
{
    public class APIService
    {
        private readonly HttpClient _httpClient;
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public APIService(HttpClient httpClient)
        {             
            _httpClient = httpClient;
        }

        public async Task<List<Planet>> GetAllPlanetsAsync()
        {

            var planets = new List<Planet>();
            string? url = "https://swapi.dev/api/planets/";


            while (url != null)
            {
                var response = await _httpClient.GetStringAsync(url);
                var planetResponse = JsonSerializer.Deserialize<PlanetResponse>(response, _jsonOptions);

                if (planetResponse?.Results != null)
                {
                    planets.AddRange(planetResponse.Results);

                    url = planetResponse?.Next;
                }
                else
                {
                    url = null;
                }
            }

            // Sort planets alphabetically by name
            planets = planets.OrderBy(p => p.Name).ToList();

            // Assign IDs based on sorted order
            for (int i = 0; i < planets.Count; i++)
            {
                planets[i].Id = i + 1;
            }

            return planets;
        }

        public async Task<List<Person>> GetResidentsAsync(string[] residentUrls)
        {
            if (residentUrls == null || residentUrls.Length == 0)
            {
                return new List<Person>();
            }

            var residents = new List<Person>();

            foreach (var url in residentUrls)
            {
                var response = await _httpClient.GetStringAsync(url);
                var person = JsonSerializer.Deserialize<Person>(response, _jsonOptions);
                if (person != null)
                {
                    residents.Add(person);
                }
            }

            return residents;
        }

    }
}
