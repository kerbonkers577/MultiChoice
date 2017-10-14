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
        private string id;
        public Teacher(){ }

        public Teacher(string id,string name, string subject) : base(name)
        {
            this.id = id;
            this.subject = subject;
        }

        public void SetID(string id)
        {
            this.id = id;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetSubject(string subject)
        {
            this.subject = subject;
        }

        public string GetName()
        {
            return name;
        }
        public string GetTeacherID()
        {
            return id;
        }
        public string GetSubject()
        {
            return subject;
        }

        public override string ReturnInfo()
        {
            string info;
            info = String.Format("Name : {0}\nSubject : {1}", name, subject);
            return info;
        }
    }
}
