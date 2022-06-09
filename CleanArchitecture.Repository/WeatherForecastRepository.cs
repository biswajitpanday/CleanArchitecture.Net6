using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Repository.Base;
using CleanArchitecture.Repository.DatabaseContext;

namespace CleanArchitecture.Repository
{
    public class WeatherForecastRepository : BaseRepository<WeatherForecastEntity>, IWeatherForecastRepository
    {
        public WeatherForecastRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
