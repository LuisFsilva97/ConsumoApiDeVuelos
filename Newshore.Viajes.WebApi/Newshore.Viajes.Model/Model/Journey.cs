using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newshore.Viajes.Model.Model
{
    public class Journey
    {
        public List<Flight> Flights { get; set; } = new List<Flight>();

        public string Origin { get; set; }

        public string Destination { get; set; }

        public Double Price { get; set; }
    }
}
