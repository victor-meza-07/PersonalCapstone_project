using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Symptoms
    {
        public Symptoms()
        {

        }
        [Key]
        public int SymptomsPK { get; set; }

        public string SymptomOne { get; set; }
        public string SymptomTwo { get; set; }
        public string SyptomThree { get; set; }

    }
}
