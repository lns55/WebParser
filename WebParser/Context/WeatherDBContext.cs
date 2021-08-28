using Microsoft.EntityFrameworkCore;

namespace WebParser.Context
{
    //Контекст базы данных. Для создание тестовой базы, чтобы было легче разрабатывать сам парсер.
    public class WeatherDBContext : DbContext
    {
        public WeatherDBContext(DbContextOptions<WeatherDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Helsinki> helsinkis { get; set; }
    }
}