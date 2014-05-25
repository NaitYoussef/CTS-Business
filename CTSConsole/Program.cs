using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CTSConsole.Models;
using CTSConsole.Models.His;
using CTSConsole.Models.Log;

namespace CTSConsole
{
    class Program
    {
        public static string GetNext(string s, ref int index)
        {
            string result = "";
            bool digit = char.IsDigit(s[index]);

            for (; index < s.Length; index++)
            {
                if (digit && char.IsDigit(s[index]))
                {
                    result += s[index];
                }
                else
                {
                    if (!digit && char.IsLetter(s[index]))
                    {
                        result += s[index];
                    }
                    else
                    {
                        return result;

                    }
                }
            }
            return result;
        }

        static void Project1()
        {
            StreamReader reader = File.OpenText("..\\..\\resources\\fichier.log");
            string line;
            Lines lines = new Lines();
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(';');
                if (items[14] != "" && items[3].Equals("CY"))
                {
                    int i = 0, etiquette1, etiquette2;
                    string sense = "", sNumeroBus = "", sNumeroLine = "";
                    double latitude, longitude;
                    DateTime dateOperation = Convert.ToDateTime(items[0]+ " " +items[1]);
                    items[5]=items[5].Replace('.', ',');
                    items[6]=items[6].Replace('.', ',');
                    int rssi, ber, la;
                    rssi = int.Parse(items[16]);
                    ber = int.Parse(items[17]);
                    la = int.Parse(items[18]);

                    if (items[5] != "")
                        latitude = double.Parse((items[5]));
                    else
                        latitude=0;
                    if (items[6] != "")
                        longitude = double.Parse((items[6]));
                    else
                        longitude=0;
                    if (items[9] != "")
                        etiquette1 = int.Parse((items[9]));
                    else
                        etiquette1=0;
                    if (items[10] != "")
                        etiquette2 = int.Parse((items[10]));
                    else
                        etiquette2=0;
                    
                    sNumeroLine = GetNext(items[14], ref i);
                    sense = GetNext(items[14], ref i);
                    sNumeroBus = items[2];
                    lines.AddLine(int.Parse(sNumeroLine), int.Parse(sNumeroBus), items[3], latitude, longitude, sense[0], dateOperation, etiquette1, etiquette2, rssi, ber, la);
                }

            }

            List<Traget> l = lines.GetEnregistrementsByLine(8);
            foreach (var traget in l)
            {
                Console.WriteLine(traget);
                Console.WriteLine("##############################################");
            }
           Console.ReadLine();
        }

        static DateTime ParseStringToDateTime(string date, string time)
        {
            string[] itemsDate = date.Split('-');
            string[] itemsTime = time.Split(':');
            return new DateTime(Convert.ToInt32(itemsDate[2])+2000, Convert.ToInt32(itemsDate[1]), Convert.ToInt32(itemsDate[0]), Convert.ToInt32(itemsTime[0]), Convert.ToInt32(itemsTime[1]), Convert.ToInt32(itemsTime[2]));
        }
        static void Project2()
        {
            StreamReader reader = File.OpenText("..\\..\\resources\\fichier.his");
            string line;
            Results results = new Results();
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split('\t',' ');
                if (items[0].Equals("B0500") && items[4].Equals("Inscr"))
                {

                    int numeroBus = Convert.ToInt32(items[5].Split(':')[1]);
                    string born = items[6].Split(':')[1], typeOpration = items[4], typeVehicule = "Bus et Tram";
                    results.AddResult(born , numeroBus, typeOpration, ParseStringToDateTime(items[1], items[2]), typeVehicule);
                }

            }
            results.PrintInfectedBorns();
            Console.WriteLine("############");
            List<Results.BornResults> r = results.GetBornAnomalies();
            foreach (var v in r)
            {
                Console.WriteLine(v.Born + " nombre de fois " + v.Buses.Count);
            }
            
            Console.ReadLine();

        }
        static void Main(string[] args)
        {
            Project1();

        }
    }
}
