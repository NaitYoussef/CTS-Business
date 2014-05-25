using System;
using System.Collections.Generic;
using CTSConsole.Models.His;

namespace CTSConsole.Models.Log
{
    class Line
    {
        public int Numero { get; set; }
        private Dictionary<int, BusLog> buses=new Dictionary<int, BusLog>();
        public override string ToString()
        {
            return "Line : " + Numero;
        }

        
        public void AddBus(int numero, string nature, double latitude, double longitude, char sense, DateTime dateEnregistrement, int etiquette1, int etiquette2, int rssi, int ber, int la)
        {
            BusLog bus;
            if (buses.TryGetValue(numero, out bus))
            {
                bus.AddEnregistrement(nature, latitude, longitude, sense, dateEnregistrement, etiquette1, etiquette2, rssi, ber, la);
            }
            else
            {
                this.buses.Add(numero, new BusLog(numero, new Enregistrement(nature, new EnregistrementDetail(dateEnregistrement, longitude, latitude, sense, etiquette1, etiquette2, rssi, ber, la))));    
            }
           
            
        }
        public Dictionary<int,BusLog> Buses
        {
            set
            {
                this.buses = value;
            }
            get
            {
                return this.buses;
            }
        } 

        public Line()
        {
            this.Numero = -1;
        }

        public Line(int numeroLine)
        {
            this.Numero = numeroLine;
        }
        public Line(int numeroLine, BusLog bus)
        {
            this.Numero = numeroLine;
            this.buses.Add(bus.Numero, bus);
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
            Line line = (Line) obj;
            return this.Numero == line.Numero;
        }



    }
}
