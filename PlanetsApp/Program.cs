using System.Net.Http;
using PlanetsApp.Services;
using PlanetsApp.Views;

class Program
{
    static async Task Main(string[] args)
    {
        using var httpClient = new HttpClient();
        var service = new APIService(httpClient);
        var view = new ConsoleView();

        var planets = await service.GetAllPlanetsAsync();
        view.DisplayPlanets(planets);

        while (true)
        {
            int selectedIndex = view.GetPlanetSelection(planets);
            var selectedPlanet = planets[selectedIndex];

            Console.WriteLine($"\nSelected planet: {selectedPlanet.Name}");

            if (selectedPlanet.Residents == null || selectedPlanet.Residents.Length == 0)
            {
                Console.WriteLine("This planet has no known residents.");
            }
            else
            {
                Console.WriteLine("Fetching residents...\n");
                var residents = await service.GetResidentsAsync(selectedPlanet.Residents);
                view.DisplayResidents(residents);
            }

            Console.WriteLine("\nPress Enter to continue or type 'exit' to quit.");
            var input = Console.ReadLine();
            if (input?.Trim().ToLower() == "exit")
                break;

            Console.Clear();
            view.DisplayPlanets(planets);
        }
    }
}