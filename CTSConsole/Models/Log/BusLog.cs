using System;
using System.Collections.Generic;

namespace CTSConsole.Models.Log
{
    class BusLog
    {
        public int Numero { get; set; }
        private List<Enregistrement> entregistrements=new List<Enregistrement>();

        public List<Enregistrement> Enregistrements
        {
            set
            {
                this.entregistrements = value;
            }
            get
            {
                return this.entregistrements;
            }
        }
        public BusLog(int numero, Enregistrement e)
        {
            this.Numero = numero;
            this.entregistrements.Add(e);
        }
        public void AddEnregistrement(Enregistrement enregistrement)
        {
            foreach (var e in entregistrements)
            {
                if (e.Equals(enregistrement))
                {
                    return;
                }
            }
            this.entregistrements.Add(enregistrement);
        }

        public void AddEnregistrement(string nature, double latitude, double longitude, char sense, DateTime dateEnregistrement, int etiquette1, int etiquette2, int rssi, int ber, int la)
        {
            foreach (var e in entregistrements)
            {
                if (e.Nature.Equals(nature))
                {
                    e.AddEnregistrementDetail(new EnregistrementDetail(dateEnregistrement, longitude, latitude, sense,etiquette1, etiquette2, rssi, ber, la));
                    return;
                }
            }
            this.AddEnregistrement(new Enregistrement(nature, new EnregistrementDetail(dateEnregistrement, longitude, latitude, sense, etiquette1, etiquette2, rssi, ber, la)));
        }
        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // TODO: write your implementation of Equals() here
            BusLog bus = (BusLog) obj;
            return (this.Numero == bus.Numero);

        }

        public override string ToString()
        {
            return "Bus numero : " + Numero ;
        }

        public BusLog()
        {
            this.Numero = -1;
        }

        public BusLog(int numero)
        {
            this.Numero = numero;
        }

        public BusLog(BusLog bus)
        {
            this.Numero = bus.Numero;
        }
    }
}
