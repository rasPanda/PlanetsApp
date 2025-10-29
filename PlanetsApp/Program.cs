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
            Console.WriteLine("\nEnter the ID number of the planet to view its residents (or type 'exit' to quit)");
            var input = Console.ReadLine();
            if (input?.Trim().ToLower() == "exit")
                break;

            if (!int.TryParse(input, out int inputId))
            {
                Console.WriteLine("An ID needs to be a whole number. Try again.");
                continue;
            }

            var selectedPlanet = planets.FirstOrDefault(p => p.Id == inputId);
            if (selectedPlanet == null)
            {
                Console.WriteLine("Perhaps the Archives are incomplete. Try again.");
                continue;
            }

            Console.WriteLine($"\nSelected planet: {selectedPlanet.Name}");

            Console.WriteLine("Fetching residents...\n");
            var residents = await service.GetResidentsAsync(selectedPlanet.Residents);
            view.DisplayResidents(residents);
        }
    }
}