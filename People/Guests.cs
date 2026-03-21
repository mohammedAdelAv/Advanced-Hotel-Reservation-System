using Advanced_Hotel_Reservation_System.enums;
using System.Net.NetworkInformation;

namespace Advanced_Hotel_Reservation_System.People
{
    internal class Guests : Person
    {
        public List<Reservation> Reservations { get; private set; }

        private string email = string.Empty;
        public string Email
        {
            get => email;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                {
                    throw new ArgumentException("Invalid Email");
                }
                this.email = value;
            }
        }
        private string phoneNumber = string.Empty;
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Invalid Phone Number");
                }
                this.phoneNumber = value;
            }
        }
        public Guests(int id, string name, string email, string phoneNumber) : base(id, name)
        {
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Reservations = new List<Reservation>();

        }

        override public void DisplayInfo()
        {
            Console.WriteLine($"Guest ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Phone Number: {PhoneNumber}");
        }

        //-------------------------------------------------------------------------------------
        // method to add a new reservation for the guest
        public Reservation AddReservation(Room room, int nights)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room), "Room cannot be null.");
            }
            if (nights <= 0)
            {
                throw new ArgumentException("Nights can not be 0");
            }
            if (room.Status == RoomStatus.Booked)
            {
                throw new Exception("Cannot reserve an occupied room.");
            }
            
            if (Reservations.Count(r => r.Status == ReservationStatus.Active) >= 2)
            {
                throw new Exception("Cannot add more than 2 reservations");
            }             
            room.UpdateStatus(RoomStatus.Booked);
            Reservation reservation = new Reservation(this, room, nights);
            Reservations.Add(reservation);
            room.IncrementReservationCount();
            return reservation;
        }

        //-------------------------------------------------------------------------------------
        // method to cancel an active reservation by room id
        public void CanselReservations(int roomId)
        {
            var res = Reservations.FirstOrDefault(r => r.Status == ReservationStatus.Active && r.Room.RoomId == roomId);
            if (res == null)
            {
                Console.WriteLine("Reservation not found");
                return;
            }
            res.UpdateStatus(ReservationStatus.Completed);
            res.Room.UpdateStatus(RoomStatus.Available);
            Console.WriteLine("Reservation cancelled successfully.");
        }

        //-------------------------------------------------------------------------------------
        // method to show all active reservations of the guest
        public void ShowReservations()
        {
            if (!Reservations.Any(r => r.Status == ReservationStatus.Active))
            {
                Console.WriteLine("No active reservations.");
                return;
            }
            foreach (var r in Reservations)
            {
                Console.WriteLine(r.ToString());
            }
        }

        //-------------------------------------------------------------------------------------
        // method to show reservation details by room id
        public void ShowReservationsByRoomId(int roomId)
        {
            var res = Reservations.FirstOrDefault(r => r.Status == ReservationStatus.Active && r.Room.RoomId == roomId);
            if (res == null)
            {
                Console.WriteLine("Reservation not found");
                return;
            }
            else
            {
                Console.WriteLine(res.ToString());

            }
        }

        //-------------------------------------------------------------------------------------
        // method to check if the guest has any active reservations
        public bool HasActiveReservations()
        {
            return Reservations.Where(r => r.Status == ReservationStatus.Active).Any();
        }

        //-------------------------------------------------------------------------------------
        // method to calculate total amount spent by the guest on completed reservations
        public double GetTotalSpent()
        {
            return Reservations
                .Where(r => r.Status == ReservationStatus.Completed)
                .Sum(r => r.TotalPrice);
        }
    }
}
