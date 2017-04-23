using PatientData.Areas.HelpPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver.Linq;

namespace PatientData
{
    public static class MongoConfig
    {
        public static void Seed()
        {
            var patients = PatientDb.Open();

            if (!patients.AsQueryable().Any(c => c.Name == "Scott"))
            {
                var data = new List<Patient> {
                    new Patient { Name = "Scott",
                        Ailments = new List<Ailment> { new Ailment {Name = "Crazy"} },
                        Medications = new List<Medication> { new Medication { Name = "Aspirin", Doses = 2 }}
                    },
                    new Patient { Name = "Alex",
                        Ailments = new List<Ailment> { new Ailment {Name = "Zanoza"} },
                        Medications = new List<Medication> { new Medication { Name = "Vitamin", Doses = 3 }}
                    }
                };

                patients.InsertBatch(data);
            }
        }
    }
}