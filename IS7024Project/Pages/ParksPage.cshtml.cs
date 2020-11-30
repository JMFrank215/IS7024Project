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
    public class ParksPageModel : PageModel
    {
        private readonly ILogger<ParksPageModel> _logger;

        public ParksPageModel(ILogger<ParksPageModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            using (var webClient = new WebClient())
            {
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