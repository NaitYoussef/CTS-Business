using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CTSConsole.Models.His
{
    class ResultsManager
    {
        public string FilePath { get; set; }
        public Results Results { get; set; }

        public ResultsManager(string filePath)
        {
            FilePath = filePath;
            Results = new Results();
        }


        public DateTime ParseStringToDateTime(string date, string time)
        {
            string[] itemsDate = date.Split('-');
            string[] itemsTime = time.Split(':');
            return new DateTime(Convert.ToInt32(itemsDate[2]) + 2000, Convert.ToInt32(itemsDate[1]), Convert.ToInt32(itemsDate[0]), Convert.ToInt32(itemsTime[0]), Convert.ToInt32(itemsTime[1]), Convert.ToInt32(itemsTime[2]));
        }
        public void ProcessFileAndStore()
        {
            StreamReader reader = File.OpenText("..\\..\\resources\\fichier.his");
            string line;
            Results = new Results();
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split('\t', ' ');
                if (items[0].Equals("B0500") && items[4].Equals("Inscr"))
                {

                    int numeroBus = Convert.ToInt32(items[5].Split(':')[1]);
                    string born = items[6].Split(':')[1], typeOpration = items[4], typeVehicule = "Bus et Tram";
                    Results.AddResult(born, numeroBus, typeOpration, ParseStringToDateTime(items[1], items[2]), typeVehicule);
                }

            }
            
            

        }
       
    }
}
