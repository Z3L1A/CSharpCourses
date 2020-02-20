using System;
using System.Text;

namespace LabEight
{
    public class Vehicle
    {
        public Vehicle(int numberOfSeats)
        {
            NumberOfSeats = numberOfSeats;
        }

        public Vehicle(int numberOfSeats, string color) : this(numberOfSeats)
        {
            Color = color;
        }

        public int YearOfIssue { get; set; }

        public string Color { get; set; }

        public int NumberOfSeats { get; private set; }

        public override string ToString()
        {
            var str = new StringBuilder();
            str.AppendFormat("Vehicle have {0} number of seats." , NumberOfSeats);

            if (YearOfIssue != 0)
            {
                str.AppendFormat(" Year of issue - {0}.", YearOfIssue);
            }

            if (Color != string.Empty)
            {
                str.AppendFormat(" Color - {0}", Color);
            }

            return str.ToString();
        }
    }
}