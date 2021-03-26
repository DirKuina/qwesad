using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Class1
    {
        public Class1()
        {
            this.Full_name = "Nikita Solovey";
            this.Number_of_group = "KS-24";
            this.Number_of_lab = 1;
            this.Number_of_project = 4;
            
        }
        
        public string Full_name { get; set; }
        public string Number_of_group { get; set; }
        public int Number_of_project { get; set; }
        public int Number_of_lab { get; set; }

    }

}
