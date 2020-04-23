using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class PreExistingConditions
    {
        public PreExistingConditions()
        {

        }

        [Key]
        public int PreExistingConditionsPK { get; set; }

        public string ConditionOne { get; set; }
        public string ConditionTwo { get; set; }
        public string ConsitionThree { get; set; }
    }
}
