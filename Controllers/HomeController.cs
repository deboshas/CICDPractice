using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Videowebapp.Models;

namespace Videowebapp.Controllers
{
    public class HomeController : Controller
    {
        private TelemetryClient aiClient;

        //custom telemetry
        public HomeController(TelemetryClient telClient)
        {
              this. aiClient = telClient;
            //this.aiClient.TrackEvent("commentSubmitted");
            //this.aiClient.TrackEvent("VideoUploaded", new Dictionary<string, string> { { "Category", "Sports" }, { "Format", "mp4" } });

            //track metrics
            //this.aiClient.GetMetric("SimultaneousPlays").TrackValue(5);

        }

        public IActionResult Index()
        {
            try
            {
                throw new Exception("Index Exception");
               
            }
            catch(Exception ex)
            {
                this.aiClient.TrackException(ex);
            }
            return View();
        }

        public IActionResult About()
        {
           

        
            try
            {
                ViewData["Message"] = "Your application description page.";
                throw new Exception("About Exception");                          
            }
            catch (Exception ex)
            {
                this.aiClient.TrackException(ex);
            }
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Like()
        {
            //adding custom event to mitor in app insght
            this.aiClient.TrackEvent("LikeClicked");
            ViewBag.Message = "Thank you for your like";
            return View("Index");
           
        }
    }
}
