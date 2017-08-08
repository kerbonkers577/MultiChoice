using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceApp
{
    class Teacher : Person
    {
        private string subject;

        public Teacher(){ }

        public Teacher(string name, string subject) : base(name)
        {
            this.subject = subject;
        }

        public override string ReturnInfo()
        {
            string info;
            info = String.Format("Name : {0}\nStudent Number : {1}", name, subject);
            return info;
        }
    }
}
