using Advanced_Hotel_Reservation_System.interfaces;

namespace Advanced_Hotel_Reservation_System
{
    internal class NightsDiscount : IDiscountStrategy
    {
        public double ApplyDiscount(int nights)
        {
            if (nights <= 5)
                return 0.05; 

            else if (nights <= 10)
                return 0.10; 

            else
                return 0.15; 
        }
    }
}
