using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeatherNew.Models
{
    public class ModelForView
    {
        [Required]
        [MinLength(1, ErrorMessage = "Enter the name of the city!")]
        [RegularExpression("[a-zA-Z]+", ErrorMessage = "The name of the city should contain only letters!")]
        [Display(Name = "Enter citi name")]
        public string Term { get; set; }
        public ModelDay ModelDay { get; set; }
        public City City { get; set; }
        public ModelForView()
        {

        }
        public ModelForView(RootObject rootObject,DateTime date)
        {

            Term = "";
            City = rootObject.city;
            ModelDay = GetListWhetherInDay(rootObject,date );
        }
        public ModelDay GetListWhetherInDay(RootObject rootObject, DateTime date)//делаем выборку погоды по дате
        {
            List<List> lists = rootObject.list.Where(x => DateTime.Parse(x.dt_txt).ToString("yyyy-MM-dd") == date.ToString("yyyy-MM-dd")).ToList<List>();

                ModelDay modelDay = new ModelDay(date, lists);
               
            return modelDay;
        }
    }
    public class ModelDay
    {
        public List<string> Date { get; set; }
        public List<List> ListWhetherInDay { get; set; }

        public ModelDay(DateTime date,List<List> list)
        {
            Date = CreateListDate(date);
            ListWhetherInDay = list;
        }

        private List<string> CreateListDate(DateTime date)
        {
            date = DateTime.Now;
            List<string> dates = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                dates.Add(date.AddDays(i).ToString("yyyy-MM-dd"));
            }
            return dates;
        }
    }

    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public Coord coord { get; set; }
        public string country { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public double pressure { get; set; }
        public double sea_level { get; set; }
        public double grnd_level { get; set; }
        public int humidity { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public double deg { get; set; }
    }

    public class Sys
    {
        public string pod { get; set; }
    }

    public class List
    {
        public int dt { get; set; }
        public Main main { get; set; }
        public List<Weather> weather { get; set; }
        public Clouds clouds { get; set; }
        public Wind wind { get; set; }
        public Sys sys { get; set; }
        public string dt_txt { get; set; }
    }

    public class RootObject
    {
        public string cod { get; set; }
        public double message { get; set; }
        public City city { get; set; }
        public int cnt { get; set; }
        public List<List> list { get; set; }
    }
}