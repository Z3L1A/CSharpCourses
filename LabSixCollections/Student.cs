using System;

namespace LabSixCollections
{
    public class Student
    {
        private static int _recordBookNumberIncrementer;
        
        public Student()
        {
            RecordBookNumber = ++_recordBookNumberIncrementer;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        private int RecordBookNumber { get; }

        public string RecordBook => $"RB{RecordBookNumber:000000}";

        public decimal AverageMark { get; set; }

        public override string ToString()
        {
            return $"Student {FirstName} {LastName}. Record book number - {RecordBook}. Average mark - {AverageMark}";
        }
    }
}