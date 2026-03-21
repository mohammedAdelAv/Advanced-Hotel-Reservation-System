using Advanced_Hotel_Reservation_System.enums;
using Advanced_Hotel_Reservation_System.People;

namespace Advanced_Hotel_Reservation_System
{
    internal class Reservation
    {
        public Guests Guest { get; private set; }
        public ReservationStatus Status { get; private set; }

        public Room Room { get; private set; }

        private int nights;
        public int Nights
        {
            get => nights;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Number of Nightd can not be 0");
                }
                this.nights = value;
            }
        }


        //-------------------------------------------------------------------------------------
        // cal the total price of the reservation based on the room price and number of nights
        public double TotalPrice => Room.Price * Nights;

        //-------------------------------------------------------------------------------------
        // method to update the reservation status
        public void UpdateStatus(ReservationStatus newStatus)
        {
            this.Status = newStatus;
        }


        public Reservation(Guests guest, Room room, int nights)
        {
            Guest = guest ?? throw new ArgumentNullException(nameof(guest));
            Room = room ?? throw new ArgumentNullException("Room can not be null");
            Nights = nights;
            Status = ReservationStatus.Active;
        }



        public override string ToString()
        {
            return $"Guest: {Guest.Name} | Room: {Room.RoomId} | Nights: {Nights} | Total: {TotalPrice}$ | Status: {Status}";
        }


        //-------------------------------------------------------------------------------------
        // method to complete the reservation and update the room status to available
        public void Complete()
        {
            Status = ReservationStatus.Completed;
            Room.UpdateStatus(RoomStatus.Available);
        }

        public double discountedPrice(int nights)
        {
            if (nights <= 0)
            {
                throw new ArgumentException("Number of nights must be greater than zero.");
            }
            if (nights >= 5) { 
            
            }
            return;
        }
    }
}
