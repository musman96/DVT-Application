using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Model
{
    public class Coordinates
    {
        [JsonProperty("lon")]
        public string lon { get; set; }
        [JsonProperty("lat")]
        public string lat { get; set; }
    }
}
