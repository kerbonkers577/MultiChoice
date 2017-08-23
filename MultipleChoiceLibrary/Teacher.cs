using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceLibrary
{
    public class Teacher : Person
    {
        //Stores information regarding to teacher who creates the test
        private string subject;

        public Teacher(){ }

        public Teacher(string name, string subject) : base(name)
        {
            this.subject = subject;
        }

        public override string ReturnInfo()
        {
            string info;
            info = String.Format("Name : {0}\nSubject : {1}", name, subject);
            return info;
        }
    }
}
