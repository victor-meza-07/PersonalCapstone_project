using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class DemographicClassifications
    {
        public DemographicClassifications()
        {

        }

        [Key]
        public int DemographicClassificationsKey { get; set; }

        public string Gender { get; set; }
        public string Race { get; set; }
        public int Age { get; set; }
    }
}
