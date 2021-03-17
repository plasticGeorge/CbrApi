using CBRapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using X.PagedList;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace CBRapi.Controllers
{
    public class CBRController : Controller
    {
        private readonly ILogger<CBRController> _logger;
        public CBRController(ILogger<CBRController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Currencies(int? page)
        {
            return View(GetListValutes().Result.ToPagedList(page ?? 1, 10));
        }

        [HttpGet]
        public IActionResult Currency(string id)
        {
            return View(GetListValutes().Result.Find(match => match.CharCode == id) ?? 
            new Valute
            {
                ID = "undefinded",
                NumCode = "undefinded",
                CharCode = "undefinded",
                Nominal = 0,
                Name = "undefinded",
                Value = 0,
                Previous = 0
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public static async Task<List<Valute>> GetListValutes()
        {
            List<Valute> valuteList = new List<Valute>();
            using (WebClient webClient = new WebClient())
            {
                string response = await webClient.DownloadStringTaskAsync("https://www.cbr-xml-daily.ru/daily_json.js");
                IJEnumerable<JToken> tokens = JObject.Parse(response)["Valute"].Children().Children<JToken>();
                foreach (var t in tokens)
                {
                    valuteList.Add(t.ToObject<Valute>());
                }
            }
            return valuteList;
        }
    }
}
