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
        
        static void Project1()
        {
            LinesManager lm = new LinesManager("..\\..\\resources\\fichier.log");
            lm.ProcessFileAndStore();
            
            List<ResultBus> l = lm.Lines.GetEnregistrementsByLine(4);
            foreach (var traget in l)
            {
                Console.WriteLine(traget);
                Console.WriteLine("===============================================");
            }
           Console.ReadLine();
        }

     
        static void Project2()
        {
            ResultsManager rm = new ResultsManager("..\\..\\resources\\fichier.his");
            rm.ProcessFileAndStore();
            rm.Results.PrintInfectedBorns();
            Console.WriteLine("############");
            List<Results.BornResults> r = rm.Results.GetBornAnomalies();
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
