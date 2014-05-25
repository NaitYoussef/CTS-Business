using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTSConsole.Models.Log
{
    class Traget
    {
        public char Sense { get; set; }
        public int NumeroLine { get; set; }
        public int NumeroBus { get; set; }
        private List<EnregistrementDetail> enregistrements=new List<EnregistrementDetail>();

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
