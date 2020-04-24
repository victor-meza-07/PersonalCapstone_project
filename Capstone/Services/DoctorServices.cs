﻿using Capstone.Data;
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
        GetData _GetData;
        public DoctorServices(ApplicationDbContext context)
        {
            _context = context;
            _GetData = new GetData();
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

        public bool CheckIfPatientProfileExists(DoctorViewModel doctorView) 
        {
            bool exists = false;

            var symptomsMixMatching = _context.SymptomsMix.Where(s => s.SymptomOne == doctorView.PatientSymptomOne &&
                                                                s.SymptomTwo == doctorView.PatientSymptomTwo &&
                                                                s.SyptomThree == doctorView.PatientSymptomThree);

            var DemographicMixMatching = _context.DemographicClassification.Where(d => d.Age == Int32.Parse(doctorView.PatientAge) &&
                                                                                       d.Gender == doctorView.PatientGender&&
                                                                                       d.Race == doctorView.PatientRace);
            if ((symptomsMixMatching != null)&&(DemographicMixMatching != null)) 
            {
                exists = true;
            }

            return exists;
        }

        public void RegisterPatientProfile(DoctorViewModel doctor) 
        {
            DemographicClassifications demos = new DemographicClassifications();
            PreExistingConditions conditions = new PreExistingConditions();
            Symptoms symptoms = new Symptoms();

            demos.Age = Int32.Parse(doctor.PatientAge);
            demos.Gender = doctor.PatientGender;
            demos.Race = doctor.PatientRace;

            conditions.ConditionOne = doctor.PatientConditionOne;
            conditions.ConditionTwo = doctor.PatientConditionTwo;
            conditions.ConsitionThree = doctor.PatientConditionThree;

            symptoms.SymptomOne = doctor.PatientSymptomOne;
            symptoms.SymptomTwo = doctor.PatientSymptomTwo;
            symptoms.SyptomThree = doctor.PatientSymptomThree;

            _context.SymptomsMix.Add(symptoms);
            _context.ConditionsMix.Add(conditions);
            _context.DemographicClassification.Add(demos);

            _context.SaveChanges();
        }

        public List<PatientDetailsViewModel> GetDefaultPatients(string docId, List<CDCDataModel> data) 
        {
            List<PatientDetailsViewModel> patients = new List<PatientDetailsViewModel>();

            PatientDetailsViewModel patientOne = new PatientDetailsViewModel { DoctorId = docId, PatientAge = "45", PatientGender = "Male", PatientRace = "White"};
            DoctorViewModel docModel = new DoctorViewModel { PatientConditionOne = "Obesity", PatientConditionTwo = "Asthma", PatientConditionThree = "Asbestosis",
                                                             PatientAge = patientOne.PatientAge, PatientRace = patientOne.PatientRace, PatientGender = patientOne.PatientGender,
                                                             PatientSymptomOne = "Chest Pain", PatientSymptomTwo = "Cough", PatientSymptomThree = "null"};

            var cause = data.Where(d => (d.Symptoms[0] == docModel.PatientSymptomOne) &&
                                        (d.Symptoms[1] == docModel.PatientSymptomTwo) &&
                                        (d.Symptoms[2] == docModel.PatientSymptomThree)).Select(d => d.Cause).FirstOrDefault();

            Dictionary<string, double> probability = new Dictionary<string, double>();
            Dictionary<string, double> CCList = _GetData.GetCC(data, docModel);
            Dictionary<string, Dictionary<string, double>> IllCCList = new Dictionary<string, Dictionary<string, double>>();

            double pVal = _GetData.GetMortalityProbability(docModel, data);
            List<string> possibleIllnesses = new List<string>();
            
            
            possibleIllnesses.Add(cause);

            probability.Add(cause, pVal);
            IllCCList.Add(cause, CCList);

            patientOne.PatientMortalityProbability = probability;
            patientOne.PatientPossibleIllness = possibleIllnesses;
            patientOne.PatientCCList = IllCCList;
            
            patients.Add(patientOne);

            return patients;
        }
    }
}
