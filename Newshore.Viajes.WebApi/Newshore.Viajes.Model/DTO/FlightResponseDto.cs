using Newshore.Viajes.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Newshore.Viajes.Model.DTO
{
    public class FlightResponseDto
    {
        public string DepartureStation { get; set; }

        public string ArrivalStation { get; set; }

        public double Price { get; set; }

        public string FlightCarrier { get; set; }

        public string FlightNumber { get; set; }

        public static Expression<Func<FlightResponseDto, Flight>> MapFlightResponseDtoToFlight => s => new Flight()
        {
            Origin = s.DepartureStation,
            Destination = s.ArrivalStation,
            Price = s.Price,
            Transport = new Transport() { FlightCarrier = s.FlightCarrier, FlightNumber = s.FlightNumber }
        };
    }
}
