using System;
using System.ComponentModel.DataAnnotations;

namespace Advanced_Hotel_Reservation_System.People
{
    public abstract class Person
    {
       
        private int id;

        [Key]  
        public int Id
        {
            get => id;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(nameof(value));
                }
                this.id = value;
            }
        }

        
        private string name = string.Empty;

        [Required]
        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(value));
                }
                this.name = value;
            }
        }

        public Person(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public abstract void DisplayInfo();
    }
}
