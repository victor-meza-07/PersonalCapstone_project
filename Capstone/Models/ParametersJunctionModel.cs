using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class ParametersJunctionModel
    {
        public ParametersJunctionModel()
        {

        }

        [Key]
        public int ParameterJunctionPrmaryKey { get; set; }

        public int SymptomsPk { get; set; }
        public int DemographicClassificationPK { get; set; }
        public int PreExistingConditionPK { get; set; }
        public int CountOfRepetitions { get; set; }
    }
}
