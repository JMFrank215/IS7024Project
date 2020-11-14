﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using QuickType;
using QuickTypeParks;

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
                var parks = Parks.FromJson(parksJSON);
                ViewData["Parks"] = parks;
                QuickTypeParks.Parks[] Parksparks = QuickTypeParks.Parks.FromJson(parksJSON);


                //Consuming Crime data
                string crimesJSON = webClient.DownloadString("https://raw.githubusercontent.com/JMFrank215/IS7024Project/master/PDI_Crime_Data.txt");
                var crimes = Crime.FromJson(crimesJSON);
                ViewData["Crime"] = crimes;
            }

        }
    }
}
