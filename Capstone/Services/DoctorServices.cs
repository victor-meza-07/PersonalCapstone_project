using Capstone.Data;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Services
{
    public class DoctorServices
    {
        ApplicationDbContext _context;
        public DoctorServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public void RegisterNewDoctor(DoctorRegisterViewModel RegisteringDoctor) 
        {
            //first register his address.
            AddressModel AddressToAdd = new AddressModel();
            AddressToAdd.BuildingNumber = RegisteringDoctor.BuildingNumber;
            AddressToAdd.StreetName = RegisteringDoctor.StreetName;
            AddressToAdd.City = RegisteringDoctor.City;
            AddressToAdd.State = RegisteringDoctor.State;
            AddressToAdd.ZipCode = RegisteringDoctor.ZipCode;

            _context.Addresses.Add(AddressToAdd);
            _context.SaveChanges();

            //get the PK of the building.
            var AddressPk = _context.Addresses.Where(a => a.StreetName == RegisteringDoctor.StreetName
                                                            && a.BuildingNumber == RegisteringDoctor.BuildingNumber
                                                            && a.City == RegisteringDoctor.City
                                                            && a.State == RegisteringDoctor.State
                                                            && a.ZipCode == RegisteringDoctor.ZipCode).Select(a => a.PrimaryKey).FirstOrDefault();

            DoctorModel doctor = new DoctorModel();

            doctor.DoctorHospitalBuildingKey = AddressPk;
            doctor.UserId = RegisteringDoctor.UserId;

            _context.Doctors.Add(doctor);
            _context.SaveChanges();

        }

        public bool CheckCurrentDoctorRegisterComplete(string id) 
        {
            bool registered = false;

            var doctor = _context.Doctors.Where(d => d.UserId == id).FirstOrDefault();
            if (doctor != null) 
            {
                registered = true;
            }
            return registered;
        }
    }
}
