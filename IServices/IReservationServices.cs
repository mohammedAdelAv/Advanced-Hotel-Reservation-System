using Advanced_Hotel_Reservation_System.Models;

namespace Advanced_Hotel_Reservation_System.IServices
{
    public interface IReservationServices
    {
     
        Task ShowAllActiveReservations(int guestId);
        Task<Reservation?> GetActiveReservationByRoomId(int guestId, int roomId);
        Task<Reservation> AddReservation(Guest guest, Room room, int nights);
        Task CancelReservations(Guest guest, int roomId);
        Task<bool> HasActiveReservations(Guest guest);
        Task<double> GetTotalSpent(int guestId);
    }
}
