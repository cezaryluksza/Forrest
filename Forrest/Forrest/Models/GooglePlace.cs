using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forrest.Models
{
    public class Candidate
    {
        public string formatted_address { get; set; }
        public string name { get; set; }
        public string place_id { get; set; }
    }

    public class GooglePlace
    {
        public List<Candidate> candidates { get; set; }
        public string status { get; set; }
    }
}
