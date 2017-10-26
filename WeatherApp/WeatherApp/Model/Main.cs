using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Model
{
    public class Main
    {
        [JsonProperty("temp")]
        public string temp { get; set; }
        [JsonProperty("pressure")]
        public string pressure { get; set; }
        [JsonProperty("humidity")]
        public string humidity { get; set; }
        [JsonProperty("temp_min")]
        public string temp_min { get; set; }
        [JsonProperty("temp_max")]
        public string temp_max { get; set; }


    }
}
