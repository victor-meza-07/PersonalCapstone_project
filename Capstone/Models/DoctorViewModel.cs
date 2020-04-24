using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class DoctorViewModel
    {
        public string DoctorId { get; set; }
        public int DoctorBuildingPK { get; set; }
        public string PatientAge { get; set; }
        public string PatientGender { get; set; }
        public string PatientRace { get; set; }
        public string PatientConditionOne { get; set; }
        public string PatientConditionTwo { get; set; }
        public string PatientConditionThree { get; set; }
        public string PatientSymptomOne { get; set; }
        public string PatientSymptomTwo { get; set; }
        public string PatientSymptomThree { get; set; }
        public List<PatientDetailsViewModel> PatientDetails { get; set; }
    }
}