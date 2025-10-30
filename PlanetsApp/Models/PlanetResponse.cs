using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetsApp.Models
{
    // model for storing planet responses
    // using a model so each page of response can get deserialized while collecting data from each page
    // this is a neater way of accomplishing the task of fetching multiple pages without sacrificing performance at larger scales
    // stores list of Results, each result a page of the planets data
    // also stores Next which is used for pagination
    public class PlanetResponse
    {
        public List<Planet>? Results { get; set; }
        public string? Next { get; set; }
    }
}
