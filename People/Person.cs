namespace Advanced_Hotel_Reservation_System.People
{
    internal abstract class Person
    {
        private int id;
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
