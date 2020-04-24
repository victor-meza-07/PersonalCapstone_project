using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class PopulationLocationModel
    {
        [Key]
        public int PK { get; set; }
        public string AgeRange { get; set; }
        public int Total { get; set; }
        public string Percent { get; set; }
        public int MaleTotal { get; set; }
        public string MalePercent { get; set; }
        public string FemaleTotal { get; set; }
        public string PercentFemale { get; set; }
        public string ZipCode { get; set; }
    }
}
