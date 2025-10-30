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
        // init httpClient and JsonSerialzerOptions
        // use of JsonSerializerOptions here just to avoid any case issues between app and API
        private readonly HttpClient _httpClient;
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        // primary constructor for httpClient to be passed into class
        public APIService(HttpClient httpClient)
        {             
            _httpClient = httpClient;
        }

        // function to fetch all planets data 
        public async Task<List<Planet>> GetAllPlanetsAsync()
        {
            // init planets list and url string
            // url string nullable as it will be mutated to null as part of pagnination loop
            var planets = new List<Planet>();
            string? url = "https://swapi.dev/api/planets/";

            // main loop to fetch each page of planets from API
            // end loop when 'next' is null, which indicates end of pagination
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

            // sort planets alphabetically by name
            planets = planets.OrderBy(p => p.Name).ToList();

            // assign IDs based on sorted order
            for (int i = 0; i < planets.Count; i++)
            {
                planets[i].Id = i + 1;
            }

            return planets;
        }

        // function to fetch residents for a planet
        public async Task<List<Person>> GetResidentsAsync(string[] residentUrls)
        {
            // return empty list if no residents
            if (residentUrls == null || residentUrls.Length == 0)
            {
                return new List<Person>();
            }

            // init new list only if there are residents
            var residents = new List<Person>();

            // loop through residents API urls, fetch each resident
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
