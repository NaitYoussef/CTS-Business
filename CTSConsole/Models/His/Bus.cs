using System;
using System.Collections.Generic;

namespace CTSConsole.Models.His
{
    class Bus
    {
        private string typeVehicule;
        private int numeroBus=-1;
        private List<Born> borns=new List<Born>();
        private string lastBorn= "";
        private DateTime lastTime;
        private Dictionary<string,Anomalie> anomalies=new Dictionary<string, Anomalie>();

        public Dictionary<string,Anomalie> Anomalies
        {
            get
            {
                return this.anomalies;
            }
            set
            {
                this.anomalies = value;
            }
        }

        public List<string> InfectedBorns()
        {
            List<string> result = new List<string>();
            foreach (var anomaly in Anomalies)
            {
                result.Add(anomaly.Key);
            }
            return result;
        }

        public List<DateTime> GetAnomaliesRange(string born)
        {
            List<DateTime> result = new List<DateTime>();
            Anomalie a = anomalies[born];
            result.Add(a.DateOperations[0]);
            result.Add(a.DateOperations[a.DateOperations.Count-1]);
            return result;
        } 

        public override string ToString()
        {
            string s;
            s = "Bus N " + numeroBus + " type " + this.typeVehicule + "Inscrit dans " + this.borns.Count + "\n";
            foreach (var born in borns)
            {
                s += born + "\n";
            }
            return s;
        }

        public void AddBorn(string born, string typeOperation, DateTime dateOperation)
        {

            if (lastBorn.Equals(born) && (dateOperation - lastTime).TotalMinutes < 60 && (dateOperation - lastTime).TotalMinutes >0)
            {
                Anomalie anomalie;
                if (anomalies.TryGetValue(born, out anomalie))
                {
                    anomalie.AddAnomalie(dateOperation);
                }
                else
                {
                    anomalies.Add(born, new Anomalie(born,dateOperation));
                }
                    
            }
            lastBorn = born;
            lastTime = dateOperation;
            Born result= borns.Find(delegate(Born b)
                {
                    return b.RefBorn == born;
                }
            );
            if (result != null)
            {
                result.AddOperation(dateOperation, typeOperation);
            }
            else
            {
                this.borns.Add(new Born(born, new Operation(dateOperation, typeOperation)));
            }

                /*
            foreach (var b in borns)
            {
                if (b.RefBorn.Equals(born))
                {
                    b.AddOperation(dateOperation, typeOperation);
                    return;
                }
            }
            
            this.borns.Add(new Born(born,new Operation(dateOperation, typeOperation)));*/
        }
        public int NumeroBus
        {
            get
            {
                return this.numeroBus;
            }
            set
            {
                this.numeroBus = value;
            }
        }
        public string TypeVehicule
        {
            get
            {
                return this.typeVehicule;
            }
            set
            {
                this.typeVehicule = value;
            }
        }

        public string PrintAnomalies()
        {
            string result = "Bus " + this.numeroBus + " Nombre de born " + anomalies.Count+ "\n";
            foreach (var anomaly in anomalies)
            {
                result += anomaly + "\n";
            }
            return result;
        }
        public Bus(string born, int numeroBus, string typeOperation, DateTime dateOperation, string typeVehicule)
        {
            this.NumeroBus = numeroBus;
            this.lastBorn = born;
            this.lastTime = dateOperation;
            this.borns.Add(new Born(born, new Operation(dateOperation,typeOperation)));
            this.typeVehicule = typeVehicule;
        }
        public List<Born> Borns
        {
            get
            {
                return this.borns;
            }
            set
            {
                this.borns = value;
            }
        }

        public Bus(int numeroBus)
        {
            this.numeroBus = numeroBus;
        }

        public Bus(Bus result)
        {
            this.numeroBus = result.numeroBus;
            foreach (var born in result.Borns)
            {
                this.borns.Add(born);   
            }
            this.typeVehicule = result.typeVehicule;
        }
    }
}
