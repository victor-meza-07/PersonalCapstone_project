using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class ParameterMLModelJunction
    {
        public ParameterMLModelJunction()
        {

        }

        [Key]
        public int PrimaryKey { get; set; }

        public int ParameterJunctionPK { get; set; }
        public string pathToModelSaveLocation { get; set; }
    }
}
