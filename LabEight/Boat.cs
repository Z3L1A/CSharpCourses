namespace LabEight
{
    public class Boat : Vehicle
    {
        public Boat(int numberOfSeats) : base(numberOfSeats)
        {
        }

        public Boat(int numberOfSeats, string color) : base(numberOfSeats, color)
        {
        }
    }
}