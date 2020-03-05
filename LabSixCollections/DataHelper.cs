using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabSixCollections
{
    public class DataHelper
    {
        private readonly Random _random;

        public DataHelper()
        {
            _random = new Random();
        }

        public IEnumerable<Student> GetListOfStudents(int number)
        {
            var students = new List<Student>();

            for (int i = 0; i < number; i++)
            {
                students.Add(GetRandomStudent());
            }

            return students;
        }

        public Student GetRandomStudent()
        {
            return new Student
            {
                FirstName = GetRandomName(),
                LastName = GetRandomName(),
                AverageMark = _random.Next(60, 100),
            };
        }

        private string GetRandomName()
        {
            var guid = Guid.NewGuid().ToString("N").ToCharArray();
            var letters = guid.Where(char.IsLetter).ToArray();
            letters[0] = char.ToUpper(letters[0]);
            var str = new string(letters);
            return str.Substring(0,str.Length > 7 ? 8 : str.Length);
        }

    }
}