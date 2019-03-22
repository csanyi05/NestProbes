using System.Collections.Generic;

namespace NestProbe
{
    public class Homework
    {
        public string Id
        {
            get;
            private set;         
        }
        public List<Line> Lines = new List<Line>();

        public Homework(string id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if (obj is Homework)
            {
                var hw2 = obj as Homework;
                return hw2.Id.Equals(Id);
            }
            return false;

        }
    }
}
