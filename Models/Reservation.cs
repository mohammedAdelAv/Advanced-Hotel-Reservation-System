using Advanced_Hotel_Reservation_System.enums;
using Advanced_Hotel_Reservation_System.interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Advanced_Hotel_Reservation_System.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; } 
    
        public Guest Guest { get; set; } // Navigation

        [ForeignKey("Guest")]
        public int GuestId { get; set; }  

     
        public Room Room { get; set; } // Navigation

        [ForeignKey("Room")]
        public int RoomId { get; set; }     



        public ReservationStatus Status { get; private set; }

        
        private int nights;

        [Required]
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

        // discount strategy for the reservation
        [NotMapped]
        public IDiscountStrategy DiscountStrategy { get; set; }

        //-------------------------------------------------------------------------------------
        // cal the total price of the reservation based on the room price and number of nights
        public double TotalPrice => Room.Price * Nights;

        // calculate the discount amount based on the total price and the discount strategy
        public double DiscountAmount => TotalPrice * DiscountStrategy.ApplyDiscount(Nights);

        // calculate the final price after applying the discount
        public double FinalPrice => TotalPrice - DiscountAmount;

        //-------------------------------------------------------------------------------------
        // method to update the reservation status
        public void UpdateStatus(ReservationStatus newStatus)
        {
            this.Status = newStatus;
        }

        //-------------------------------------------------------------------------------------
        // constructor to initialize the reservation with guest, room, and number of nights
        public Reservation() { }
        public Reservation(Guest guest, Room room, int nights)
        {
            Guest = guest ?? throw new ArgumentNullException(nameof(guest));
            Room = room ?? throw new ArgumentNullException("Room can not be null");
            Nights = nights;
            Status = ReservationStatus.Active;

            DiscountStrategy = new NightsDiscount();
        }

       


        //-------------------------------------------------------------------------------------
        // method to complete the reservation and update the room status to available
        public void Complete()
        {
            Status = ReservationStatus.Completed;
            Room.UpdateStatus(RoomStatus.Available);
        }

        //-------------------------------------------------------------------------------------

        public override string ToString()
        {
            return $"Guest: {Guest.Name} | Room: {Room.RoomId} | Status: {Status} | Nights: {Nights} | Total: {TotalPrice}$ | Discount: {DiscountAmount:C} | Final: {FinalPrice:C}";
        }

    }
}
