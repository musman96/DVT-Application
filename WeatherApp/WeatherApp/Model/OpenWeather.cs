using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Model
{
    public class OpenWeather
    {
        [JsonProperty("coord")]
        public Coordinates coord { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("main")]
        public Main main { get; set; }
        [JsonProperty("weather")]
        public List<Weather> weather { get; set; }
        
    }
}
