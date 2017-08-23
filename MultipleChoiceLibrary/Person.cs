using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceLibrary
{
    public abstract class Person
    {
        //Abstract class that Student and teacher are derived from
        protected string name;
        

        public Person() { }

        public Person(string name)
        {
            this.name = name;
        }

        public abstract string ReturnInfo();
        
    }
}
