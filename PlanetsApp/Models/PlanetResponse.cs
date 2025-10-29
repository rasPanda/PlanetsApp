using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetsApp.Models
{
    public class PlanetResponse
    {
        public List<Planet>? Results { get; set; }
        public string? Next { get; set; }
    }
}
