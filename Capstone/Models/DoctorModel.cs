using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class DoctorModel
    {
        [Key]
        public int PK { get; set; }
        
        public string UserId { get; set; }
        public int DoctorHospitalBuildingKey { get; set; }
    }
}
