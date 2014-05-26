using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CTSConsole.Models.Log
{
    class ResultBus
    {
        public ResultBus(int numeroBus, Traget aller, Traget retour)
        {
            NumeroBus = numeroBus;
            Aller = aller;
            Retour = retour;
        }

        public int NumeroBus { get; set; }
        public Traget Aller { get; set; }
        public Traget Retour { get; set; }

        public override string ToString()
        {
            return "Ce bus " + this.NumeroBus + " a effectue un l'aller suivant " + "\n" + Aller +
                   "\n#########\net le retour suivant \n" + Retour;
        }
    }
}
