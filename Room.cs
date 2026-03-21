using Advanced_Hotel_Reservation_System.enums;

namespace Advanced_Hotel_Reservation_System
{
    internal class Room
    {
        private int roomId;
        public int RoomId
        {
            get => roomId;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Invalid RoomId");
                }
                this.roomId = value;
            }
        }

        private double price;
        public double Price
        {
            get => price;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Invalid Price");
                }
                this.price = value;
            }
        }

        public RoomType Type { get; set; }
        public RoomStatus Status { get; private set; }

        public int ReservationCount { get; private set; }

        public Room(int id, RoomType type, double price)
        {
            this.RoomId = id;
            this.Type = type;
            this.Price = price;
            this.Status = RoomStatus.Available;

        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Room ID: {RoomId}");
            Console.WriteLine($"Price: {Price:C}");
            Console.WriteLine($"Type: {Type}");
            Console.WriteLine($"Status: {Status}");
        }

        // Method to update the room status
        public void UpdateStatus(RoomStatus newStatus)
        {
            this.Status = newStatus;
        }

        public void IncrementReservationCount()
        {
            ReservationCount++;
        }

    }
}
