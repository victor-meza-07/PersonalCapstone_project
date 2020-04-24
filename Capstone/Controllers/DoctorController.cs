using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Capstone.Data;
using Capstone.Models;
using Capstone.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    public class DoctorController : Controller
    {
        ApplicationDbContext _context;
        DoctorServices _doctorServices;
        public DoctorController(ApplicationDbContext context)
        {
            _context = context;
            _doctorServices = new DoctorServices(_context);
        }
        public IActionResult Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (_doctorServices.CheckCurrentDoctorRegisterComplete(userId) == false)
            {
                //TODO: MAKE A DOCTOR VIEW ITEM.
                return Register(userId);
            }
            else 
            {
                DoctorViewModel doctor = new DoctorViewModel();
                var buildingPk = _context.Doctors.Where(d => d.UserId == userId).Select(d => d.DoctorHospitalBuildingKey).FirstOrDefault();

                doctor.DoctorBuildingPK = buildingPk;
                doctor.DoctorId = userId;
                return View(doctor);
            }
        }

        public IActionResult Register(string currentId) 
        {

            DoctorRegisterViewModel registerDoctor = new DoctorRegisterViewModel();
            registerDoctor.UserId = currentId;
            return View("RegisterDoctor", registerDoctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Doctor/RegisterDoctor")]
        public IActionResult ProcessRegisterForm(DoctorRegisterViewModel doctor) 
        {
            _doctorServices.RegisterNewDoctor(doctor);
            return RedirectToAction("Index", "Doctor");
        }

        public IActionResult AddAnewPatient(DoctorViewModel doctor) 
        {
            return View("RegisterPatient", doctor);
        }

        [HttpPost]
        [Route("/Doctor/RegisterPatient")]
        public IActionResult RegisterPatient(DoctorViewModel doctor) 
        {
            //this is where we begin registering the patients symptoms
            bool profileExists = _doctorServices.CheckIfPatientProfileExists(doctor);
            if (profileExists == true) 
            {
                //Send to a view that display's this patients Probability of death, that includes the CC of their pre existing symptom

                /* GET THE DATA RELEVANT TO US*/ 
            }
            else 
            {
                _doctorServices.RegisterPatientProfile(doctor);
                //once we have registered them, we send them the data we need haha!

            }
            return RedirectToAction("Index", "Doctor");
        }
    }
}