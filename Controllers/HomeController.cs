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
        // GET: OpenWeatherMapMvc
        [HttpGet]
        public ActionResult Index()
        {
            ModelForView model = new ModelForView();
            model.responseWeather = GetWeatherByCiti("Kyiv"); // Информция о погоде в Киеве отображается по умолчанию.
                                                                      //можно было бы использовать определение города по IP юзера но это коректно работает только с развернутым сайтом а не при использовании локального сервер
            return View("~/Views/Home/Index.cshtml", model); //вызов представления и передача модели

        }
        [HttpPost]
        public ActionResult Index(ModelForView model)
        {
            if (ModelState.IsValid) //проверяем валидность
            {
                model.responseWeather = GetWeatherByCiti(model.Term);//ищем погоду по названию города
                if (model.responseWeather != null)//проверяем получили ли мы данные о погоде в указанном нами городе
                {
                    return View("~/Views/Home/Index.cshtml", model);//вызов представления и передача модели
                }
            }
            return Content("You did not specify a city or the specified city was not found!");//Error

        }
        private static ResponseWeather GetWeatherByCiti(string cities)
        {
            ResponseWeather rootObject;
            HttpWebRequest apiRequest = WebRequest.Create("https://api.openweathermap.org/data/2.5/weather?q="
                                        + cities + "&appid=" + ApiKey + "&units=metric") as HttpWebRequest;//запрос по названию города

            string apiResponse = "";
            try
            {
                using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)//используем using для подключению в к вyешнему Api
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());//создаём поток для чтения данных
                    apiResponse = reader.ReadToEnd();//считываем данные и получаем JSON
                }
            }
            catch (Exception)
            {
                return null;
            }

            rootObject = JsonConvert.DeserializeObject<ResponseWeather>(apiResponse);//преобразовываем получиный JSON в модель ResponseWeather
            return rootObject;
        }
    }
}