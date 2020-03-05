using System.Collections.Generic;

namespace LabSixCollections
{
    public class Faculty
    {
        public Faculty()
        {
            Students = new List<Student>();
        }

        public string Name { get; set; }

        public List<Student> Students { get; set; }
    }
}