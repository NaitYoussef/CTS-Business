using System;
using System.Collections.Generic;

namespace CTSConsole.Models.His
{
    class Operation
    {
        private List<DateTime> dateOperation = new List<DateTime>();
        public string TypeOperation { get; set; }
        public override string ToString()
        {
            string s = "";
            foreach (var date in dateOperation)
            {
                s+=date + "\n";
            }
            return s;
        }
         

        public List<DateTime> DateOperation
        {
            get { return dateOperation; }
            set { dateOperation = value; }
        }

        public void Inc(DateTime date)
        {
            this.dateOperation.Add(date);
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
            Operation operation = (Operation) obj;
            if (this.TypeOperation.Equals(operation.TypeOperation))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public Operation()
        {
            this.TypeOperation = "";
        }

        public Operation(DateTime dateOperation, string typeOperation)
        {
            this.dateOperation.Add(dateOperation);
            this.TypeOperation = typeOperation;
        }

        public Operation(Operation operation)
        {
            this.TypeOperation = operation.TypeOperation;
            foreach (var date in operation.dateOperation)
            {
                this.dateOperation.Add(date);
            }
        }
    }
}
