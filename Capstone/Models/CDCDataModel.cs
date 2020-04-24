using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class CDCDataModel
    {
        public string AgeRange { get; set; }
        public string Gender { get; set; }

        public string Race { get; set; }

        public string Cause { get; set; }

        public string Year { get; set; }

        public string Deaths { get; set; }

        public string Population { get; set; }

        public List<string> Conditions { get; set; }

        public List<string> Symptoms { get; set; }
    }
}
