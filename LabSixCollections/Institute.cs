using System.Collections.Generic;

namespace LabSixCollections
{
    public class Institute
    {
        public Institute()
        {
            Faculties = new List<Faculty>();
        }

        public string Name { get; set; }

        public List<Faculty> Faculties { get; set; }
    }
}