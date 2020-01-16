using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PRProjekt1.Managers;
using PRProjekt1.Models;
using RestSharp;

namespace PRProjekt1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaceDetailsController : ControllerBase
    {
        private readonly ILogger<PlaceDetailsController> _logger;

        public PlaceDetailsController(ILogger<PlaceDetailsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IEnumerable<PlaceDetails> GetPlaceDetails([FromBody]string search)
        {
            GooglePlacesManager mgr = new GooglePlacesManager();
            var taskResult = mgr.GetPlaceDetails(search);
            if (taskResult == null) { return null; }
            List<PlaceDetails> list = new List<PlaceDetails>();
            list.Add(taskResult.Result?.result);
            return list;
        }
    }

    public class Place
    {
        public string Name { get; set; }
    }
}
