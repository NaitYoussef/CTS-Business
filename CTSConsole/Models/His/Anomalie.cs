using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTSConsole.Models.His
{
    class Anomalie
    {
        public string Born { get; set; }
        private List<DateTime> datesOperations =new List<DateTime>();

        public List<DateTime> DateOperations
        {
            get
            {
                return this.datesOperations;
            }
            set
            {
                this.datesOperations = value;
            }
        } 
        public Anomalie()
        {
            Born = "";
           
        }

        public List<DateTime> GetRange()
        {
            List<DateTime> result = new List<DateTime>();
            if (datesOperations.Count > 0) { 
                result.Add(datesOperations[0]);
                result.Add(datesOperations[datesOperations.Count-1]);
            }
            return result;
        } 
        public Anomalie(string born, DateTime dateOperation)
        {
            this.Born = born;
            this.datesOperations.Add(dateOperation);
        }

        public override string ToString()
        {
            return "Born " + this.Born + " Nombre d'anomalie " + this.datesOperations.Count;
        }

        public void AddAnomalie(DateTime dateAnomalie)
        {
            this.datesOperations.Add(dateAnomalie);
        }
        public Anomalie(Anomalie a)
        {
            this.Born = a.Born;
            this.datesOperations = a.datesOperations;
        }
    }
}
