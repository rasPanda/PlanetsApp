using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetsApp.Models
{
    // simple model for planet
    // only storing data points required for app, more can be added
    public class Planet
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string[]? Residents { get; set; }
    }
}
