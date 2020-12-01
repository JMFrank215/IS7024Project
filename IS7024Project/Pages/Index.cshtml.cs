using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using CrimeSpace;
using ParkSpace;

namespace IS7024Project.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            using (var webClient = new WebClient())
            { 
                //Consuming Weather data
                String key = System.IO.File.ReadAllText("WeatherAPIKey.txt"); 
                String weatherString = webClient.DownloadString("https://api.weatherbit.io/v2.0/current?&city=Cincinnati&country=USA&key=" + key);
                WeatherSpace.Weather weatherWeather = WeatherSpace.Weather.FromJson(weatherString);

                //precipitation
                long precip = 0;
                foreach(WeatherSpace.Datum weather in weatherWeather.Data)
                {
                    precip = weather.Precip;
                }
                if (precip < 1)
                {
                    ViewData["WeatherMessage"] = "Not looking like rain!";
                } else
                {
                    ViewData["WeatherMessage"] = "Rain expected, be careful out there!";
                } 

                //Consuming Parks data
                string crimesJSON = webClient.DownloadString("https://raw.githubusercontent.com/JMFrank215/IS7024Project/master/Parks_Data.txt");
                var parks = Parks.FromJson(crimesJSON);
                ViewData["Parks"] = parks;
                ParkSpace.Parks[] Parksparks = ParkSpace.Parks.FromJson(crimesJSON); 


                //Consuming Crime data
                string jsonString = webClient.DownloadString("https://raw.githubusercontent.com/JMFrank215/IS7024Project/master/PDI_Crime_Data.txt");


                //Validation received data
                string crimeschema = System.IO.File.ReadAllText("CrimeSchema.json");
                JSchema cschema = JSchema.Parse(crimeschema);

                JArray cjsonObject = JArray.Parse(jsonString);

                if (cjsonObject.IsValid(cschema))
                {
                    var crimes = Crime.FromJson(jsonString);
                    ViewData["Crime"] = crimes;

                }

            }


        }
    }
}
