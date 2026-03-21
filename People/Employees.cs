namespace Advanced_Hotel_Reservation_System.People
{
    internal class Employees : Person
    {
        private string position = string.Empty;
        public string Position
        {
            get => position;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Invalid Position");
                }
                this.position = value;
            }
        }

        private double salary;
        public double Salary
        {
            get => salary;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Invalid Salary");
                }
                this.salary = value;
            }
        }

        public Employees(int id, string name, string position, double salary) : base(id, name)
        {
            this.Position = position;
            this.Salary = salary;
        }

        override public void DisplayInfo()
        {
            Console.WriteLine($"Employee ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Position: {Position}");
            Console.WriteLine($"Salary: {Salary:C}");
        }
    }
}
