using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Forrest.Managers;
using Forrest.Models;
using RestSharp;

namespace Forrest.Controllers
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
            return mgr.GetPlaceDetails(search);
        }
    }
}
