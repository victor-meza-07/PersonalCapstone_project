using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class PatientDetailsViewModel
    {
        public PatientDetailsViewModel()
        {

        }

        public string DoctorId { get; set; }
        public string PatientAge { get; set; }
        public string PatientRace { get; set; }
        public string PatientGender { get; set; }
        public Dictionary<string, Dictionary<string,double> > PatientCCList { get; set; }
        public List<string> PatientPossibleIllness { get; set; }
        public Dictionary<string, double> PatientMortalityProbability { get; set; }

    }
}
