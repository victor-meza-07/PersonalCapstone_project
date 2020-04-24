using Capstone.Models;
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
        public Dictionary<string, double> GetCC(List<CDCDataModel> data) 
        {
            Dictionary<string, double> correlationCoeficients = new Dictionary<string, double>();
            
            
            
            
            return correlationCoeficients;
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

    }
}
