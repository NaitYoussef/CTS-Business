using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace CTSConsole.Models.Log
{
    class Traget
    {
        public char Sense { get; set; }
        public int NumeroLine { get; set; }
        public int NumeroBus { get; set; }
        private List<EnregistrementDetail> enregistrements=new List<EnregistrementDetail>();

       

        public static double DistanceTo(double lat1, double lon1, double lat2, double lon2, string unit)
        {
            var rlat1 = Math.PI * lat1 / 180;
            var rlat2 = Math.PI * lat2 / 180;
            var rlon1 = Math.PI * lon1 / 180;
            var rlon2 = Math.PI * lon2 / 180;

            var theta = lon1 - lon2;
            var rtheta = Math.PI * theta / 180;

            var dist = Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            if (unit == "K") { dist = dist * 1.609344; }
            if (unit == "M") { dist = dist * 1.609344 * 1000; }
            if (unit == "N") { dist = dist * 0.8684; }
            return dist;
        }

        public int FindPositionToInsetInBuses(Traget t,int startPos, EnregistrementDetail ed )
        {
            if (t.enregistrements.Count > startPos && startPos>=0)
            {
                double minDistance = DistanceTo(t.enregistrements[startPos].Latitude, t.enregistrements[startPos].Longitude,
                    ed.Latitude, ed.Latitude, "M");
                int pos = startPos;
                int i = startPos + 1;
                while (i<t.enregistrements.Count && DistanceTo(t.enregistrements[i].Latitude, t.enregistrements[i].Longitude,
                    ed.Latitude, ed.Latitude, "M")<minDistance)
                {
                    i++;
                }
                return i;
            }
            return -1;
        }
        public int FindPositionToInsetInTrams(Traget t, int startPos, EnregistrementDetail ed)
        {
            if (t.enregistrements.Count > startPos && startPos >= 0)
            {
                for (int i = startPos; i < t.enregistrements.Count; i++)
                {
                    if (ed.Etiquette1 == t.enregistrements[i].Etiquette1 )
                    {
                        return i;
                    }
                }
         
            }
            return -1;
        }
        public int GetQualiteFromRssi(int rssi)
        {
            int qualite = 0;
            if (rssi == 1 || rssi == 2)
            {
                qualite = 1;
            }
            if (rssi == 3 || rssi == 4)
            {
                qualite = 2;
            }
            if (rssi == 5)
            {
                qualite = 3;
            }
            return qualite;
        }

        public Traget SummarizeBus()
        {
            Traget result = new Traget();
            result.NumeroBus = this.NumeroBus;
            result.Sense = this.Sense;
            result.NumeroLine = this.NumeroLine;
            if (this.enregistrements.Count > 0)
            {
                EnregistrementDetail ed = this.enregistrements[0];
                int lastQualite = GetQualiteFromRssi(ed.Rssi);
                result.AddEnregistrement(new EnregistrementDetail(ed.DateEnregistrement, ed.Longitude, ed.Latitude, ed.Sense, ed.Etiquette1, ed.Etiquette2, lastQualite, ed.Ber, ed.La));
                for (int i = 1; i < this.enregistrements.Count; i++)
                {
                    int currentQualite = GetQualiteFromRssi(this.enregistrements[i].Rssi);
                    if (currentQualite != lastQualite)
                    {
                        result.AddEnregistrement(new EnregistrementDetail(this.enregistrements[i].DateEnregistrement, this.enregistrements[i].Longitude, this.enregistrements[i].Latitude, this.enregistrements[i].Sense, this.enregistrements[i].Etiquette1, this.enregistrements[i].Etiquette2, currentQualite, this.enregistrements[i].Ber, this.enregistrements[i].La));
                        lastQualite = currentQualite;
                    }
                }
            }
            return result;
        }
        public Traget SummarizeTram()
        {
            Traget result = new Traget();
            result.NumeroBus = this.NumeroBus;
            result.Sense = this.Sense;
            result.NumeroLine = this.NumeroLine;
            if (this.enregistrements.Count > 0)
            {
                foreach (var ed in this.enregistrements)
                {
                    EnregistrementDetail enreg =   result.enregistrements.Find(delegate(EnregistrementDetail e)
                    {
                        return e.Etiquette1 == ed.Etiquette1;
                    }
                    );
                    if(enreg==null)
                    {
                        List<EnregistrementDetail> enregisResult = this.enregistrements.FindAll(delegate(EnregistrementDetail e)
                        {
                            return e.Etiquette1 == ed.Etiquette1;
                        }
                      );
                        int totalRssi = 0;
                        int cp=0;
                        foreach (var enregistrementDetail in enregisResult)
                        {
                            totalRssi += enregistrementDetail.Rssi;
                            cp++;
                        }
                        int qualite=0;
                        if(cp!=0)
                         qualite= (int)(totalRssi/cp);
                        
                        result.AddEnregistrement(new EnregistrementDetail(ed.DateEnregistrement,ed.Longitude,ed.Latitude,ed.Sense,ed.Etiquette1,ed.Etiquette2,GetQualiteFromRssi(qualite),ed.Ber,ed.La));
                    }
                }
            }
            return result;
        }
        public Traget Summarize()
        {
            if (this.NumeroBus > 1000)
            {
                return SummarizeTram();
            }
            else
            {
                return SummarizeBus();
            }
        }

        
        public Traget Fusion(Traget target)
        {
            int j = 0, i = 0, countThis = 0, countTarget = 0;
            List<EnregistrementDetail> listThis, listTarget;
            listTarget = target.enregistrements;
            listThis = this.enregistrements;
            countTarget = target.enregistrements.Count;
            countThis = this.enregistrements.Count;
            Traget result = new Traget();
            result.NumeroLine = (this.NumeroLine == 0 ? target.NumeroLine : this.NumeroLine);
            result.NumeroBus = (this.NumeroBus == 0 ? target.NumeroBus : this.NumeroBus);
            result.Sense = (this.Sense == (char)0 ? target.Sense : this.Sense);
            foreach (var ed in listThis)
            {
                result.AddEnregistrement(ed);
            }
            int pos = 0;
            foreach (var ed in listTarget)
            {
                if (this.NumeroBus > 1000)
                    result.AddEnregistrement(ed);
                else
                    pos = FindPositionToInsetInTrams(result, pos, ed);
                if (pos == -1)
                {
                    result.AddEnregistrement(ed);

                }
                else
                {
                    result.enregistrements.Insert(pos, ed);
                }
            }
            return result; 
        }
        public override string ToString()
        {

            string result = "Traget de line : " + NumeroLine + " Bus : " + NumeroBus + " Sense  : " + Sense;
            foreach (var e in enregistrements)
            {
                result += e.ToString() + '\n';
            }
            return result;
        }

        public Traget()
        {
            this.Sense = (char)0;
            this.NumeroBus = 0;
            this.NumeroLine = 0;
            
        }

        public void AddEnregistrement(EnregistrementDetail ed)
        {
            this.enregistrements.Add(ed);
        }
    }
}
