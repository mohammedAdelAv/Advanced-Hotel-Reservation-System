using Advanced_Hotel_Reservation_System.enums;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace Advanced_Hotel_Reservation_System.Models
{
    public class Guest : Person
    {  

        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

        [EmailAddress]
        [Required]
        public string? Email {  get; set; }

        [Required]
        public string? PhoneNumber {  get; set; }

        public Guest() : base(0, "") { } 

        public Guest(int id, string name, string email, string phoneNumber) : base(0, name)
        {
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }

        override public void DisplayInfo()
        {
            Console.WriteLine($"Id: {Id}, Name: {Name}, Email: {Email}");
        }

    }   
}
