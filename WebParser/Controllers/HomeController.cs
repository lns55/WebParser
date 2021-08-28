using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WebParser.Context;

namespace WebParser.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WeatherDBContext _db;

        public HomeController(ILogger<HomeController> logger, WeatherDBContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            //Таймер для запуска парсинга каждые 5 секунд
            var timer = new Timer(
                e => Parse(),
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(05));

            //Список данных, которые метод спарсил в бд, выводит на главную страницу
            var DbData = _db.helsinkis.ToList();

            return View(DbData);
        }

        public void Parse()
        {
            string url = "https://www.gismeteo.ru/weather-helsinki-479/";

            WebClient client = new WebClient();

            client.Encoding = Encoding.UTF8;

            var htmlData = client.DownloadData(url);

            var htmlString = client.Encoding.GetString(htmlData);

            //Достаем текущую температуру в Хельсинки
            var weather = Regex.Match(htmlString, @"<span class=""unit unit_temperature_c"">\+(\d{2})<\/span>")
                .Groups[1]
                .Value
                .ToString();

            var data = new Helsinki
            {
                Degrees = weather
            };
            //Сохраняем температуру в бд
            _db.helsinkis.Add(data);

            _db.SaveChanges();
        }
    }
}
