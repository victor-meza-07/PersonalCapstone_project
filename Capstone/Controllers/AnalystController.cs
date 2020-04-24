using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    public class AnalystController : Controller
    {
        

        public IActionResult Index()
        {
            AnalystViewModel viewmodel = new AnalystViewModel();
            return View(viewmodel);
        }

        [HttpPost]
        public IActionResult ShowMap(AnalystViewModel model) 
        {
            AnalystService service = new AnalystService();
            model.ZipCodeAtRisk =  service.GetZipCode(model.PatientCondition);
            return View(model);
        }
    }
}