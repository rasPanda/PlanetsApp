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
        // view to loop through all fetched planets and log
        public void DisplayPlanets(List<Planet> planets)
        {
            Console.WriteLine("All known planets in the galaxy:");
            foreach (var planet in planets)
            {
                Console.WriteLine($"{planet.Id}. {planet.Name} ({planet.Residents?.Length ?? 0} residents)");
            }

        }

        // view to loop through all residents provided and log
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
