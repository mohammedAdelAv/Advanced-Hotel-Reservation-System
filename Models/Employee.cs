using Advanced_Hotel_Reservation_System.People;
using System.ComponentModel.DataAnnotations;

namespace Advanced_Hotel_Reservation_System.Models
{
    public class Employee : Person
    {
        private string position = string.Empty;
        [Required]
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
        [Required]
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

        public Employee(int id, string name, string position, double salary) : base(id, name)
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
