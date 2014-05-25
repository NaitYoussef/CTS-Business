using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace CTSConsole.Models.Log
{

    class EnregistrementDetail
    {
        public DateTime DateEnregistrement { get; set; }
        public override string ToString()
        {
            return "Enregistrment Detail : " + DateEnregistrement + " " + Longitude + " " + Latitude + " " + Sense + " " + Etiquette1 + " " + Etiquette2 + " " + Rssi + " " + Ber + " " +La;
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
            EnregistrementDetail ed = (EnregistrementDetail) obj;
            return (this.Latitude.Equals(ed.Latitude) && this.DateEnregistrement.Equals(ed.DateEnregistrement) &&
                    this.Longitude.Equals(ed.Longitude) && this.Etiquette1.Equals(ed.Etiquette1) &&
                    this.Etiquette2.Equals(ed.Etiquette2) &&
                    this.La.Equals(ed.La) && this.Ber.Equals(ed.Ber) && this.Rssi.Equals(ed.Rssi));

        }

// override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            throw new NotImplementedException();
            return base.GetHashCode();
        }
        public double Longitude { get; set; }
        public int Rssi { get; set; }
        public int Ber { get; set; }
        public int La { get; set; }
        public double Latitude { get; set; }
        public char Sense { get; set; }

        public int Etiquette1 { get; set; }
        public int Etiquette2 { get; set; }
        
        public EnregistrementDetail(DateTime dateEnregistrement, double longitude, double latitude, char sense, int etiquette1, int etiquette2,int rssi, int ber, int la)
        {
            this.DateEnregistrement = dateEnregistrement;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Sense = sense;
            this.Etiquette1 = etiquette1;
            this.Etiquette2 = etiquette2;
            this.Rssi = rssi;
            this.La = la;
            this.Ber = ber;
        }
        
        public EnregistrementDetail()
        {
            this.DateEnregistrement = new DateTime();
            this.Latitude = 0;
            this.Longitude = 0;
            this.Sense = (char) 0;
            this.Etiquette1 = 0;
            this.Rssi = 0;
            this.Ber = 0;
            this.La = 0;
            this.Etiquette2 = 0;
        }

        public EnregistrementDetail(EnregistrementDetail ed)
        {
            this.Latitude = ed.Latitude;
            this.Longitude = ed.Longitude;
            this.Sense = ed.Sense;
            this.DateEnregistrement = ed.DateEnregistrement;
            this.Etiquette1 = ed.Etiquette1;
            this.Etiquette2 = ed.Etiquette2;
            this.La = ed.La;
            this.Ber = ed.Ber;
            this.Rssi = ed.Rssi;
        }
    }
}
