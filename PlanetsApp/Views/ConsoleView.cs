using PlanetsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PlanetsApp.Views
{
    public class ConsoleView
    {
        public void DisplayPlanets(List<Planet> planets)
        {
            Console.WriteLine("All known planets in the galaxy:");
            foreach (var planet in planets)
            {
                Console.WriteLine($"{planet.Id}. {planet.Name} ({planet.Residents?.Length ?? 0} residents)");
            }

        }

        public int GetPlanetSelection(List<Planet> planets)
        {
            Console.WriteLine("Enter the ID number of the planet to view its residents:");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int inputId))
                {
                    var planet = planets.FirstOrDefault(p => p.Id == inputId);
                    if (planet != null)
                    {
                        return planets.IndexOf(planet);
                    }
                }

                Console.WriteLine("Perhaps the Archives are incomplete. Try again with a valid planet ID.");
            }
        }

        public void DisplayResidents(List<Person> residents)
        {
            if (residents.Count == 0)
            {
                Console.WriteLine("This planet has no known residents.");
            }
            else
            {
                Console.WriteLine("Residents:");
                foreach (var resident in residents)
                {
                    Console.WriteLine($"- {resident.Name}");
                }
            }
        }
    }
}
