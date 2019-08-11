using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WeatherNew.Models;

namespace WeatherNew.Controllers
{
    public class HomeController : Controller
    {
        private static string ApiKey = "f53b600213f56cf6284a15e540b85796"; //ключ для доступа к OpenWeatherMap
         private const string pathFile = @"D:\GitHub\WeatherNew\Temp\Wheather.json";// !!!!!----- необходимо указать путь для сохранения данных запроса
        // GET: OpenWeatherMapMvc
        [HttpGet]
        public ActionResult Index()
        {
           
           RootObject rootObject = GetWeatherByCiti("Kyiv"); // Информция о погоде в Киеве отображается по умолчанию.
            ModelForView model = new ModelForView(rootObject,DateTime.Now);           //можно было бы использовать определение города по IP юзера но это коректно работает только с развернутым сайтом а не при использовании локального сервер
            return View("~/Views/Home/Index.cshtml", model); //вызов представления и передача модели

        }
        [HttpPost]
        public ActionResult Index(ModelForView model)
        {
            if (ModelState.IsValid) //проверяем валидность
            {
                RootObject rootObject = GetWeatherByCiti(model.Term);//ищем погоду по названию города
                if (rootObject != null)//проверяем получили ли мы данные о погоде в указанном нами городе
                {
                    model = new ModelForView(rootObject, DateTime.Now); //формируем модель погоды на один день
                    return View("~/Views/Home/Index.cshtml", model);//вызов представления и передача модели
                }
            }
            return Content("You did not specify a city or the specified city was not found!");//Error
        }
        private static RootObject GetWeatherByCiti(string cities)
        {
            RootObject rootObject;
            HttpWebRequest apiRequest = WebRequest.Create("https://api.openweathermap.org/data/2.5/forecast?q="
                                        + cities + "&appid=" + ApiKey + "&lang=ru&units=metric") as HttpWebRequest;//запрос по названию города

            string apiResponse = "";
            try
            {
                using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)//используем using для подключению в к вyешнему Api
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());//создаём поток для чтения данных
                    apiResponse = reader.ReadToEnd();//считываем данные и получаем JSON
                }
                System.IO.File.WriteAllText(pathFile, apiResponse);
            }
            catch (Exception)
            {
                return null;
            }

            rootObject = JsonConvert.DeserializeObject<RootObject>(apiResponse);//преобразовываем получиный JSON в модель ResponseWeather

            return rootObject;
        }
       
        
        public ActionResult GetWeather(string date)
        {
            RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(System.IO.File.ReadAllText(pathFile));

            ModelForView model = new ModelForView(rootObject, DateTime.Parse(date));
            return View("~/Views/Home/Index.cshtml", model);
        }
        
    }
}