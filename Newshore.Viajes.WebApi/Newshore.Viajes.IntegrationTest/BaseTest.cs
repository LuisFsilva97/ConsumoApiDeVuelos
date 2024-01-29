using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newshore.Viajes.Application.ApplicationService;
using Newshore.Viajes.Application.IApplicationService;
using Newshore.Viajes.Business.IServices;
using Newshore.Viajes.Business.Services;
using Newshore.Viajes.Communications.IServices;
using Newshore.Viajes.Communications.Services;
using Newshore.Viajes.Model.DTO;
using Newshore.Viajes.Model.Model;
using Newshore.Viajes.Repository;
using Newshore.Viajes.Repository.Data;
using Newshore.Viajes.Repository.IServices;
using Newshore.Viajes.Repository.Services;

namespace Newshore.Viajes.UnitTest
{
    public abstract class BaseTest
    {
        protected IServiceProvider serviceProvider { get; set; }
        protected ServiceCollection servicesCollection { get; set; }
        protected SqliteConnection sqliteConnection { get; set; }

        [TestInitialize]
        public void Init()
        {
            servicesCollection = new ServiceCollection();
            servicesCollection.AddMvc();

            sqliteConnection = new SqliteConnection("DataSource=:memory:");
            sqliteConnection.Open();

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(sqliteConnection);
            var options = dbContextOptionsBuilder.Options;

            var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            servicesCollection.AddSingleton<AppDbContext>(context);

            servicesCollection.AddDbContext<AppDbContext>(op => { op.UseSqlite(sqliteConnection); });

            var configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = configurationBuilder.Build();

            servicesCollection.AddSingleton<IConfiguration>(configuration);
            // Agregar Servicios al contenedor de dependencias
            servicesCollection.AddTransient<IApiFlightsService, ApiFlightsService>();
            servicesCollection.Decorate<IApiFlightsService, CachedApiFlightsService>();
            servicesCollection.AddTransient<ISearchFlightService, SearchFlightService>();
            servicesCollection.AddTransient<ISearchFlightApplicationService, SearchFlightApplicationService>();
            servicesCollection.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            servicesCollection.AddTransient<ISearchHistoryRespository, SearchHistoryRespository>();

            serviceProvider = servicesCollection.BuildServiceProvider();
        }

        public void ResetDbContext()
        {
            serviceProvider = serviceProvider.CreateScope().ServiceProvider;
        }

        public void InitDataSql(string file)
        {
            var patch = Environment.ProcessPath;
            patch = patch.Replace("\\bin\\Debug\\net6.0\\testhost.exe", string.Format(@"\Script\{0}.sql", file));
            string script = File.ReadAllText(patch);
            var dbContext = serviceProvider.GetService<AppDbContext>();
            dbContext.Database.ExecuteSqlRaw(script);
        }

        [TestCleanup]
        public void CleanUp()
        {
            sqliteConnection.Close();
        }

        public List<FlightResponseDto> GetFakeDataApiService()
        {
            List<FlightResponseDto> flightResponseDtos = new List<FlightResponseDto>();

            flightResponseDtos.Add(new FlightResponseDto()
            {
                DepartureStation = "MZL",
                ArrivalStation = "MDE",
                FlightCarrier = "CO",
                FlightNumber = "8001",
                Price = 200
            });
            flightResponseDtos.Add(new FlightResponseDto()
            {
                DepartureStation = "MZL",
                ArrivalStation = "CTG",
                FlightCarrier = "CO",
                FlightNumber = "8002",
                Price = 200
            });
            flightResponseDtos.Add(new FlightResponseDto()
            {
                DepartureStation = "PEI",
                ArrivalStation = "BOG",
                FlightCarrier = "CO",
                FlightNumber = "8003",
                Price = 200
            });
            flightResponseDtos.Add(new FlightResponseDto()
            {
                DepartureStation = "MDE",
                ArrivalStation = "BCN",
                FlightCarrier = "CO",
                FlightNumber = "8004",
                Price = 500
            });
            flightResponseDtos.Add(new FlightResponseDto()
            {
                DepartureStation = "CTG",
                ArrivalStation = "CAN",
                FlightCarrier = "CO",
                FlightNumber = "8005",
                Price = 300
            });
            flightResponseDtos.Add(new FlightResponseDto()
            {
                DepartureStation = "BOG",
                ArrivalStation = "MAD",
                FlightCarrier = "CO",
                FlightNumber = "8006",
                Price = 500
            });
            flightResponseDtos.Add(new FlightResponseDto()
            {
                DepartureStation = "BOG",
                ArrivalStation = "MEX",
                FlightCarrier = "CO",
                FlightNumber = "8007",
                Price = 300
            });
            flightResponseDtos.Add(new FlightResponseDto()
            {
                DepartureStation = "MZL",
                ArrivalStation = "PEI",
                FlightCarrier = "CO",
                FlightNumber = "8008",
                Price = 200
            });
            flightResponseDtos.Add(new FlightResponseDto()
            {
                DepartureStation = "MDE",
                ArrivalStation = "CTG",
                FlightCarrier = "CO",
                FlightNumber = "8009",
                Price = 200
            });
            flightResponseDtos.Add(new FlightResponseDto()
            {
                DepartureStation = "BOG",
                ArrivalStation = "CTG",
                FlightCarrier = "CO",
                FlightNumber = "8010",
                Price = 200
            });

            return flightResponseDtos;
        }

        public List<Flight> getFlightsFakeData() {
            return  new List<Flight>() {
                new Flight() {
                    Origin = "MZL",
                    Destination = "JFK",
                    Transport = new Transport() {
                        FlightCarrier = "CO",
                        FlightNumber = "8020"
                    },
                    Price = 200
                },
                new Flight() {
                    Origin = "JFK",
                    Destination = "BCN",
                    Transport = new Transport() {
                        FlightCarrier = "CO",
                        FlightNumber = "8030"
                    },
                    Price = 500
                }
            };
        }
    }
}