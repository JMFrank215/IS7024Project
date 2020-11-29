﻿using System;
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
            using(var webClient = new WebClient())
            {
                //Consuming Parks data
                string parksJSON = webClient.DownloadString("https://raw.githubusercontent.com/JMFrank215/IS7024Project/master/Parks_Data.txt");
                var park = Parks.FromJson(parksJSON);
                ViewData["Parks"] = park;
                ParkSpace.Parks[] Parksparks = ParkSpace.Parks.FromJson(parksJSON);







                //Consuming Crime data
                string jsonString = webClient.DownloadString("https://raw.githubusercontent.com/JMFrank215/IS7024Project/master/PDI_Crime_Data.txt");                


                //Validation received data
                string crimeschema = System.IO.File.ReadAllText("CrimeSchema.json");
                JSchema cschema = JSchema.Parse(crimeschema);
               
                JArray cjsonObject = JArray.Parse(jsonString);

                if (cjsonObject.IsValid(cschema))
                {
                    var crime = Crime.FromJson(jsonString);
                    ViewData["Crime"] = crime;
                    
                }

            }
            

        }
    }
}
