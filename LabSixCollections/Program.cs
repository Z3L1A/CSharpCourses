using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabSixCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            var dataHelper = new DataHelper();

            var kpi = new Institute()
            {
                Name = "КПІ",
                Faculties = 
                {
                    new Faculty
                    {
                        Name = "ФІОТ",
                        Students = dataHelper.GetListOfStudents(152).ToList(),
                    },
                    new Faculty
                    {
                        Name = "ФТІ",
                        Students = dataHelper.GetListOfStudents(100).ToList(),
                    },
                    new Faculty
                    {
                        Name = "ФЕЛ",
                        Students = dataHelper.GetListOfStudents(127).ToList(),
                    },
                    new Faculty
                    {
                        Name = "ІПСА",
                        Students = dataHelper.GetListOfStudents(73).ToList(),
                    }
                }
            };
            var knu = new Institute()
            {
                Name = "КНУ",
                Faculties = 
                {
                    new Faculty
                    {
                        Name = "МехМат",
                        Students = dataHelper.GetListOfStudents(135).ToList(),
                    },
                    new Faculty
                    {
                        Name = "ФізТех",
                        Students = dataHelper.GetListOfStudents(86).ToList(),
                    },
                    new Faculty
                    {
                        Name = "Хім",
                        Students = dataHelper.GetListOfStudents(93).ToList(),
                    }
                }
            };
            var knuba = new Institute()
            {
                Name = "КНУБА",
                Faculties = 
                {
                    new Faculty
                    {
                        Name = "Арх",
                        Students = dataHelper.GetListOfStudents(174).ToList(),
                    },
                    new Faculty
                    {
                        Name = "ФАІТ",
                        Students = dataHelper.GetListOfStudents(98).ToList(),
                    },
                    new Faculty
                    {
                        Name = "Диз",
                        Students = dataHelper.GetListOfStudents(71).ToList(),
                    }
                }
            };

            var institutes = new List<Institute>() {kpi, knu, knuba};
            foreach (var institute in institutes)
            {
                Console.WriteLine($"Кількість студентів у {institute.Name}: {GetNumberOfStudentsInInstitute(institute)}.");
            }

            Console.WriteLine();

            foreach (var institute in institutes)
            {
                var facultyWithMaxNumberOfStudents = GetFacultyWithMaxNumberOfStudents(institute);
                Console.WriteLine($"У інституті {institute.Name} факультет з найбільшою кількістю студентів - {facultyWithMaxNumberOfStudents.Name}. " +
                                  $"Кількість студентів - {facultyWithMaxNumberOfStudents.Students.Count}");
            }

            Console.WriteLine();

            foreach (var institute in institutes)
            {
                var studentsWithAMark = GetStudentsWithAMark(institute);
                Console.WriteLine($"Кількість студентів з середньою оцінкою 95..100 в інституті {institute.Name} - {studentsWithAMark.Count()}");
            }

            Console.WriteLine();

            foreach (var faculty in kpi.Faculties)
            {
                var studentsWithAMark = GetStudentsWithAMark(faculty);
                Console.WriteLine($"Кількість студентів з середньою оцінкою 95..100 в інституті {kpi.Name} на факультеті {faculty.Name} - {studentsWithAMark.Count()}");
            }
        }

        public static int GetNumberOfStudentsInInstitute(Institute institute)
        {
            var result = 0;

            foreach (var faculty in institute.Faculties)
            {
                result += faculty.Students.Count;
            }

            return result;
        }

        public static Faculty GetFacultyWithMaxNumberOfStudents(Institute institute)
        {
            var result = institute.Faculties[0];

            foreach (var faculty in institute.Faculties)
            {
                if (faculty.Students.Count > result.Students.Count)
                {
                    result = faculty;
                }
            }

            return result;
        }

        public static IEnumerable<Student> GetStudentsWithAMark(Institute institute)
        {
            var studentsWithAMark = new List<List<Student>>();

            foreach (var faculty in institute.Faculties)
            {
                studentsWithAMark.Add(GetStudentsWithAMark(faculty).ToList());
            }

            return studentsWithAMark.SelectMany(s => s);
        }

        public static IEnumerable<Student> GetStudentsWithAMark(Faculty faculty)
        {
            var studentsWithAMark = new List<Student>();

            foreach (var student in faculty.Students)
            {
                if (student.AverageMark > 94)
                {
                    studentsWithAMark.Add(student);
                }
            }

            return studentsWithAMark;
        }
    }
}
