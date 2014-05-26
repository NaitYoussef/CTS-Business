using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTSConsole.Models.His
{
    class Results
    {
        public class BornResults
        {
            public string Born { get; set; }
            public List<int> Buses { get; set; }
            
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public BornResults()
            {
                Born = "";
                this.Buses = new List<int>();;
            }
            public BornResults(string born, List<int> buses)
            {
                Born = born;
                this.Buses = buses;
            }
            public BornResults(string born, List<Bus> buses)
            {
                Born = born;
                this.Buses=new List<int>();
                if(buses.Count>0)
                {
                    this.StartDate = buses[0].GetAnomaliesRange(born)[0];
                    this.EndDate = buses[0].GetAnomaliesRange(born)[1];
                    foreach (var buse in buses)
                    {
                        this.Buses.Add(buse.NumeroBus);
                        if ((StartDate - buse.GetAnomaliesRange(born)[0]).TotalMinutes > 0)
                        {
                            StartDate = buse.GetAnomaliesRange(born)[0];
                        }
                        if ((buse.GetAnomaliesRange(born)[1] - EndDate ).TotalMinutes > 0)
                        {
                            EndDate = buse.GetAnomaliesRange(born)[1];
                        }
                    }
                }
            }
        }
        private List<Bus> results = new List<Bus>();
        private List<string> borns = new List<string>();//pour teste si y'a des probleme en commune pour tous les bus
        public List<Bus> ListeResult 
        {
            get
            {
                return this.results;
            }
            set
            {
                this.results = value;
            } 
        } 
        public void Print()
        {
            Console.WriteLine("Nombre de bus : "  + results.Count );
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
            
        }

        public void PrintInfectedBorns()
        {
            foreach (var born in borns)
            {
                
                List<Bus> res=results.FindAll(delegate(Bus b)
                {
                    return b.InfectedBorns().Contains(born);
                }
                );
                Console.WriteLine("Born " + born  + " Nombre de bus avec ce probleme " + res.Count);

            }
        }

        public List<BornResults> GetBornAnomalies()
        {
            List<BornResults> r = new List<BornResults>();
            foreach (var born in borns)
            {

                List<Bus> res = results.FindAll(delegate(Bus b)
                {
                    return b.InfectedBorns().Contains(born);
                }
                );
                r.Add(new BornResults(born, res));
               

            }
            return r;
        }
        
        public void PrintAnomalies()
        {
            int cp = 0;
            Console.WriteLine("Nombre de bus " + this.results.Count);
            foreach (var result in results)
            {
                if (result.Anomalies.Count > 0)
                {
                    Console.WriteLine(result.PrintAnomalies());
                    cp++;
                }
                    
            }
            Console.WriteLine("Nombre de bus avec Anomalies " + cp);
        }

        public void AddResult(string born, int numeroBus, string typeOperation, DateTime dateOperation, string typeVehicule)
        {
            if(! borns.Contains(born))
                borns.Add(born);
            foreach (var r in results)
            {
                if (r.NumeroBus == numeroBus)
                {
                    r.AddBorn(born, typeOperation, dateOperation);
                    return;
                }
            }
            Bus result = new Bus(born, numeroBus, typeOperation, dateOperation, typeVehicule);
            this.results.Add(result);
        }
    }
}
