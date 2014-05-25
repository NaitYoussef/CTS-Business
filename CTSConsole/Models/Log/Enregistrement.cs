using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTSConsole.Models.Log
{
    class Enregistrement
    {
        public string Nature { get; set; }
        private List<EnregistrementDetail> details= new List<EnregistrementDetail>();

        public Enregistrement(string nature)
        {
            this.Nature = nature;
        }
        public Enregistrement(string nature, EnregistrementDetail ed)
        {
            this.Nature = nature;
            this.details.Add(ed);
        }
        public List<EnregistrementDetail> Details
        {
            set
            {
                this.details = value;
            }
            get
            {
                return this.details;
            }
           
        }

        public override string ToString()
        {
            string result = "Enregistrement : " + Nature + '\n' + "Liste Enregistrement";
            foreach (var detail in details)
            {
                result += detail.ToString() + '\n';
            }
            return result;
        }

        public void AddEnregistrementDetail(EnregistrementDetail ed)
        {
            if(!this.Details.Contains(ed))
                this.details.Add(ed);

        }
        
        public Enregistrement(Enregistrement e)
        {
            this.Nature = e.Nature;
            foreach (var enregistrementDetail in e.details)
            {
                this.details.Add(enregistrementDetail);
            }
        }
    }
}
