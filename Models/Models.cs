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
        [MinLength(1,ErrorMessage = "Enter the name of the city!")]
        [RegularExpression("[a-zA-Z]+",ErrorMessage = "The name of the city should contain only letters!")]
        [Display(Name = "Enter citi name")]
        public string Term { get; set; }
        public ResponseWeather responseWeather { get; set; }
        public ModelForView()
        {
            Term = "";
            responseWeather = new ResponseWeather();
        }
    }

    public class Clouds
    {
        public int all { get; set; }
    }
    public class Coord //координаты
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }
    public class Main
    {
        public double temp { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
    }
    public class ResponseWeather
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }
    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public double message { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }
    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
    public class Wind
    {
        public double speed { get; set; }
        public double deg { get; set; }
    }
}