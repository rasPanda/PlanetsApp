using System.Net.Http;
using PlanetsApp.Services;
using PlanetsApp.Views;

class Program
{
    static async Task Main()
    {
        // init httpClient, service, and views
        // dependency injection for httpClient
        using var httpClient = new HttpClient();
        var service = new APIService(httpClient);
        var view = new ConsoleView();

        // fetch all planets, store data locally, and display all at start of program
        var planets = await service.GetAllPlanetsAsync();
        view.DisplayPlanets(planets);

        // main program loop
        while (true)
        {
            // main program prompt, provide option to exit
            Console.WriteLine("\nEnter the ID number of the planet to view its residents (or type 'exit' to quit)");
            var input = Console.ReadLine();
            if (input?.Trim().ToLower() == "exit")
                break;

            // validate input is an int --TODO move into validator class?
            if (!int.TryParse(input, out int inputId))
            {
                Console.WriteLine("An ID needs to be a whole number. Try again.");
                continue;
            }

            // validate input is a valid ID for num of planets --TODO move into validator class?
            var selectedPlanet = planets.FirstOrDefault(p => p.Id == inputId);
            if (selectedPlanet == null)
            {
                Console.WriteLine("Perhaps the Archives are incomplete. Try again.");
                continue;
            }

            // display correctly selected planet + residents
            Console.WriteLine($"\nSelected planet: {selectedPlanet.Name}");

            Console.WriteLine("Fetching residents...\n");
            var residents = await service.GetResidentsAsync(selectedPlanet.Residents ?? Array.Empty<string>());
            view.DisplayResidents(residents);
        }
    }
}