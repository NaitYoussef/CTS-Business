using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CTSConsole.Models.His;

namespace CTSConsole.Models.Log
{
    class Lines
    {
        private Dictionary<int,Line> lines = new Dictionary<int,Line>();

        // a implementer
        public Enregistrement GetEnregistrementsByBus(int numeroBus)
        {
            Enregistrement results = new Enregistrement("CY");
            foreach (var line in lines)
            {
                //expetion a gére
                BusLog bus;
                if (line.Value.Buses.TryGetValue(numeroBus, out bus)) { 
                    results.Details.AddRange(bus.Enregistrements[0].Details);
                }
            }
            return results;
        }
        

        
        public List<Traget> MyInteligence(int numeroLine)
        {
            //List<Traget> result = GetEnregistrementsByLine(numeroLine);
           // List<BusLog> buses = this.lines[numeroLine].Buses;
            return new List<Traget>();
        }
        public List<ResultBus> GetEnregistrementsByLine(int numeroLine)
        {
            List<ResultBus> results = new List<ResultBus>();
            Dictionary<int, BusLog> buses = this.lines[numeroLine].Buses;
            foreach (var bus in buses)
            {
                //exception a gére du enregistrement[0] 
                Traget aller = new Traget();
                Traget retour = new Traget();
                List<EnregistrementDetail> ed = bus.Value.Enregistrements[0].Details;
                int i = 0;
                while (i<ed.Count)
                {
                    Traget t = new Traget();
                    char sense='0';
                    t.NumeroBus = bus.Key;
                    t.NumeroLine = numeroLine;
                    t.Sense = ed[i].Sense;
                    t.AddEnregistrement(ed[i]);
                    i++;
                    while (i<ed.Count && (ed[i].DateEnregistrement-ed[i-1].DateEnregistrement).TotalMinutes<4 && ed[i].Sense == ed[i-1].Sense )
                    {
                        t.AddEnregistrement(ed[i]);
                        sense = ed[i].Sense;
                        i++;
                        
                    }
                    if (sense == 'A')
                    {
                        aller = aller.Fusion(t);
                    }
                    else if (sense == 'R')
                    {
                        retour = retour.Fusion(t);
                    }
                }

                results.Add(new ResultBus(bus.Key,aller.Summarize(),retour.Summarize()));
               

            }
            return results;
        }
        public Lines()
        {
            
        }
        public Lines(Lines l)
        {
            foreach (var line in l.lines)
            {
                this.lines.Add(line.Key, line.Value);
            }
        }

        public void AddLine(int numeroLine, int numeroBus, string nature, double latitude, double longitude, char sense, DateTime dateEnregistrement, int etiquette1, int etiquette2, int rssi, int ber, int la)
        {
            Line line;
            if (lines.TryGetValue(numeroLine, out line))
                 line.AddBus(numeroBus, nature, latitude, longitude, sense, dateEnregistrement, etiquette1, etiquette2, rssi, ber, la);
            else
                this.lines.Add(numeroLine,new Line(numeroLine,new BusLog(numeroBus, new Enregistrement(nature, new EnregistrementDetail(dateEnregistrement, longitude, latitude, sense, etiquette1, etiquette2, rssi, ber ,la )))));
        }

       
    }
}
