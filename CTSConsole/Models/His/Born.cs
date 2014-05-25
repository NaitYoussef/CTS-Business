using System;
using System.Collections.Generic;

namespace CTSConsole.Models.His
{
    class Born
    {
        private List<Operation> operations=new List<Operation>();
        private string refBorn="";

        public string RefBorn
        {
            get
            {
                return refBorn;
            }
            set
            {
                this.refBorn = value;
            }
        }

        public override string ToString()
        {

            string s= "Born : " + refBorn + " nombre d'instription : " + operations[0].DateOperation.Count + "\n";
            foreach (var o in operations)
            {
                s += o + "\n";
            }
            return s;
        }

        public void AddOperation(DateTime dateOperation, string typeOperation)
        {

            foreach (var operation in operations)
            {
                if (operation.TypeOperation.Equals(typeOperation))
                {
                    operation.Inc(dateOperation);
                    return ;
                }
            }
            this.operations.Add(new Operation(dateOperation,typeOperation));
        }
        

        public Born(string born, Operation operation)
        {
            this.refBorn = born;
            this.operations.Add(operation);
        }
        public Born(Born operations)
        {
            this.refBorn = operations.refBorn;
            foreach (var operation in operations.operations)
            {
                this.operations.Add(operation);
            }
        }
    }
}
