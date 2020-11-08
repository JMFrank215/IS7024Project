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
using QuickType;

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
                string jsonString = webClient.DownloadString("https://raw.githubusercontent.com/JMFrank215/IS7024Project/master/Parks_Data.txt");
                JSchema schema = JSchema.Parse(System.IO.File.ReadAllText("ParkSchema.json"));
                JArray jsonArray = JArray.Parse(jsonString);
                IList<string> validationEvents = new List<string>();
                if (jsonArray.IsValid(schema, out validationEvents))
                {
                    var park = Park.FromJson(jsonString);
                    ViewData["Park"] = park;
                    // not needed? List<QuickType.Park> parksList = (List<QuickType.Park>)ViewData["Park"];
                } else
                {
                    foreach(string evt in validationEvents)
                    {
                        Console.WriteLine(evt);
                    }
                    // not sure how to add ViewData["Park"] = new List<?>();
                }

            }
        }
    }
}
