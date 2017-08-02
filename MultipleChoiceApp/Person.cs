using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceApp
{
    abstract class Person
    {
        protected string name;
        

        public Person() { }

        public Person(string name)
        {
            this.name = name;
        }

        public abstract string ReturnInfo();
        
    }
}
