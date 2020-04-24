using Capstone.Models;
using Meta.Numerics.Statistics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Services
{
    public class GetData
    {
        public List<CDCDataModel> GetCDCData() 
        {
            List<CDCDataModel> dataset = parseData();
            
            return dataset;
        }
        public Dictionary<string, double> GetCC(List<CDCDataModel> data, DoctorViewModel doctorCurrentPatient) 
        {
            Dictionary<string, double> correlationCoeficients = new Dictionary<string, double>();
            List<string> conditions = new List<string>();

            //calculate the mean of the times this particular pre existing condition shows up in the data.
            //when it's related to the age group and descease of the current patient.

            string conditionOne = doctorCurrentPatient.PatientConditionOne;
            string conditionTwo = doctorCurrentPatient.PatientConditionTwo;
            string conditionThree = doctorCurrentPatient.PatientConditionThree;

            conditions.Add(conditionOne);
            conditions.Add(conditionTwo);
            conditions.Add(conditionThree);

            foreach (string condition in conditions) 
            {
                //all deaths associated with this condition will be list one
                //all deaths associated with this condition and demographic specs will be list 2.

                var listAllDeathsWithCondition = data.Where(d => d.Conditions.Contains(condition)).Select(d=>double.Parse(d.Deaths)).ToList();
                var listAllSpecificDeaths = data.Where(d => d.Conditions.Contains(condition) &&
                                                            double.Parse(d.AgeRange.Substring(0, 2)) < double.Parse(doctorCurrentPatient.PatientAge) &&
                                                            double.Parse(d.AgeRange.Substring(3, 2)) > double.Parse(doctorCurrentPatient.PatientAge) &&
                                                            d.Gender == doctorCurrentPatient.PatientGender &&
                                                            d.Race == doctorCurrentPatient.PatientRace).Select(d => double.Parse(d.Deaths)).ToList();


                
                Double[] specificDeathsArr = listAllSpecificDeaths.ToArray();
                while (listAllDeathsWithCondition.Count != listAllSpecificDeaths.Count) 
                {
                    listAllDeathsWithCondition.Remove(listAllDeathsWithCondition[listAllDeathsWithCondition.Count - 1]);
                }
                Double[] allDeathsArr = listAllDeathsWithCondition.ToArray();

                var corr = Correlation(specificDeathsArr, allDeathsArr);

                correlationCoeficients.Add(condition, corr);
            }

            
            

            return correlationCoeficients;
        }

        public double GetMortalityProbability(DoctorViewModel currentPatient, List<CDCDataModel> data) 
        {
            double pValue = 0.0;
            //calculate all the deaths of this patients age range for the symptoms they have
           
            int patientAge = Int32.Parse(currentPatient.PatientAge);
            var cause = data.Where(d => (d.Symptoms[0] == currentPatient.PatientSymptomOne) &&
                                            (d.Symptoms[1] == currentPatient.PatientSymptomTwo) &&
                                            (d.Symptoms[2] == currentPatient.PatientSymptomThree)).Select(d => d.Cause).FirstOrDefault();
            if (patientAge >= 85)
            {
                var deathList = data.Where(d => Int32.Parse(d.AgeRange.Substring(0, 2)) <= patientAge &&
                                                            currentPatient.PatientGender == d.Gender &&
                                                            currentPatient.PatientRace == d.Race &&
                                                            cause == d.Cause).Select(d => Int32.Parse(d.Deaths));
                var populationList = data.Where(d => Int32.Parse(d.AgeRange.Substring(0, 2)) <= patientAge &&
                                                            currentPatient.PatientGender == d.Gender &&
                                                            currentPatient.PatientRace == d.Race &&
                                                            cause == d.Cause).Select(d => Int32.Parse(d.Population));

                var sumsOfDeaths = deathList.Sum();
                var sumOfPopulation = populationList.Sum();


                pValue = (sumsOfDeaths / sumOfPopulation);

            }
            else 
            {
                string deaths = "";
                string pops = "";
                //linq for anyone else.
                var deathLists = data.Where(d => (Int32.Parse(d.AgeRange.Substring(0, 2)) <= patientAge) && 
                                                 (Int32.Parse(d.AgeRange.Substring(3, 2)) >= patientAge) &&
                                                 (currentPatient.PatientGender == d.Gender) &&
                                                 (currentPatient.PatientRace == d.Race) &&
                                                 (cause == d.Cause)).Select(d => Int32.Parse(( deaths = d.Deaths.Replace(",",""))));

                var populationList = data.Where(d => (Int32.Parse(d.AgeRange.Substring(0, 2)) <= patientAge) &&
                                                 (Int32.Parse(d.AgeRange.Substring(3, 2)) >= patientAge) &&
                                                 (currentPatient.PatientGender == d.Gender) &&
                                                 (currentPatient.PatientRace == d.Race) &&
                                                 (cause == d.Cause)).Select(d => Int32.Parse((pops = d.Population.Replace(",", ""))));

                var sumsOfDeaths = deathLists.Sum();
                var sumOfPopulation = populationList.Sum();


                pValue = (sumsOfDeaths / sumOfPopulation);
            }
            //we can use linq
            

            return pValue;
        }
        
        
        
        private List<CDCDataModel> parseData() 
        {
            List<CDCDataModel> models = new List<CDCDataModel>();
            int FieldCounter = 0;
        
            CDCDataModel _model = new CDCDataModel();
            TextFieldParser parser = new TextFieldParser(@"C:\Users\victo\Desktop\DevCode\testData.csv");
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            while (!parser.EndOfData)
            {
                if (models.Count > 504)
                {
                    Console.WriteLine(models[models.Count - 1]);
                }
                string[] fields = parser.ReadFields();
                foreach (string field in fields)
                {
                    if ((field.Contains('-') || field.Contains('+')) && (FieldCounter != 3))
                    {
                        _model = new CDCDataModel();
                        _model.AgeRange = field;
                        FieldCounter = 0;
                        FieldCounter++;
                    }
                    else if (FieldCounter == 1)
                    {
                        _model.Gender = field;
                        FieldCounter++;
                    }
                    else if (FieldCounter == 2)
                    {
                        _model.Race = field;
                        FieldCounter++;
                    }
                    else if (FieldCounter == 3)
                    {
                        _model.Cause = field;
                        FieldCounter++;
                    }
                    else if (FieldCounter == 4)
                    {
                        _model.Year = field;
                        FieldCounter++;
                    }
                    else if (FieldCounter == 5)
                    {
                        _model.Deaths = field;
                        FieldCounter++;
                    }
                    else if (FieldCounter == 6)
                    {
                        _model.Population = field;
                        FieldCounter++;
                    }
                    else if (FieldCounter == 7)
                    {
                        _model.Conditions = new List<string>();
                        string[] conditions = field.Split(',');
                        _model.Conditions.Add(conditions[0]);
                        _model.Conditions.Add(conditions[1]);
                        _model.Conditions.Add(conditions[2]);
                        FieldCounter++;
                    }
                    else if (FieldCounter == 8)
                    {
                        _model.Symptoms = new List<string>();
                        string[] symptoms = field.Split(',');
                        _model.Symptoms.Add(symptoms[0]);
                        _model.Symptoms.Add(symptoms[1]);
                        _model.Symptoms.Add(symptoms[2]);


                        models.Add(_model);
                    }

                }
            }

            return models;
        }
        private double Correlation(IEnumerable<Double> xs, IEnumerable<Double> ys)
        {
            // sums of x, y, x squared etc.
            double sx = 0.0;
            double sy = 0.0;
            double sxx = 0.0;
            double syy = 0.0;
            double sxy = 0.0;

            int n = 0;

            using (var enX = xs.GetEnumerator())
            {
                using (var enY = ys.GetEnumerator())
                {
                    while (enX.MoveNext() && enY.MoveNext())
                    {
                        double x = enX.Current;
                        double y = enY.Current;

                        n += 1;
                        sx += x;
                        sy += y;
                        sxx += x * x;
                        syy += y * y;
                        sxy += x * y;
                    }
                }
            }

            // covariation
            double cov = sxy / n - sx * sy / n / n;
            // standard error of x
            double sigmaX = Math.Sqrt(sxx / n - sx * sx / n / n);
            // standard error of y
            double sigmaY = Math.Sqrt(syy / n - sy * sy / n / n);

            // correlation is just a normalized covariation
            return cov / sigmaX / sigmaY;
        }
    }
}
