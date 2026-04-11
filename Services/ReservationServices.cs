using Advanced_Hotel_Reservation_System.Data;
using Advanced_Hotel_Reservation_System.enums;
using Advanced_Hotel_Reservation_System.IServices;
using Advanced_Hotel_Reservation_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Advanced_Hotel_Reservation_System.Services
{
    public class reservationServices : IReservationServices
    {
        private readonly AppDbContext _context;
        public reservationServices(AppDbContext context)
        {
            _context = context;
        }



        public async Task<List<Reservation>> GetAll()
        {
            return await _context.Reservations.ToListAsync();

        }

        public async Task<Reservation> GetById(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
                throw new KeyNotFoundException("Reservation not found");
            return reservation;
        }

        //-------------------------------------------------------------------------------------
        // method to show all active reservations of the guest
        public async Task ShowAllActiveReservations(int guestId)
        {
            await _context.Reservations
                .Include(r => r.Room)
                .FirstOrDefaultAsync(r => r.GuestId == guestId && r.Status == ReservationStatus.Active);
        }

        //-------------------------------------------------------------------------------------
        // method to show reservation details by room id
        public async Task<Reservation?> GetActiveReservationByRoomId(int guestId, int roomId)
        {
            return await _context.Reservations
            .Include(r => r.Room)
            .FirstOrDefaultAsync(r => r.GuestId == guestId && r.Room.RoomId == roomId && r.Status == ReservationStatus.Active);
        }

        //-------------------------------------------------------------------------------------
        // method to add a new reservation for the guest
        public async Task<Reservation> AddReservation(Guest guest, Room room, int nights)
        {
            if (room == null)
                throw new ArgumentNullException(nameof(room), "Room cannot be null.");

            if (nights <= 0)
                throw new ArgumentException("Nights can not be 0");

            if (room.Status == RoomStatus.Booked)
                throw new Exception("Cannot reserve an occupied room.");


            if (guest.Reservations.Count(r => r.Status == ReservationStatus.Active) >= 2)
                throw new Exception("Cannot add more than 2 reservations");

            room.UpdateStatus(RoomStatus.Booked);
            Reservation reservation = new Reservation(guest, room, nights);
            room.IncrementReservationCount();

            reservation.DiscountStrategy = new NightsDiscount();

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return reservation;
        }

        //-------------------------------------------------------------------------------------
        // method to cancel an active reservation by room id
        public async Task CancelReservations(Guest guest, int roomId)
        {
            var res = guest.Reservations.FirstOrDefault(r => r.Status == ReservationStatus.Active && r.Room.RoomId == roomId);

            if (res == null)
                throw new Exception("Reservation not found");

            res.UpdateStatus(ReservationStatus.Completed);
            res.Room.UpdateStatus(RoomStatus.Available);
            await _context.SaveChangesAsync();
        }

        //-------------------------------------------------------------------------------------
        // method to check if the guest has any active reservations
        public bool HasActiveReservations(Guest guest)
        {
            return guest.Reservations.Where(r => r.Status == ReservationStatus.Active).Any();
        }

        //-------------------------------------------------------------------------------------
        // method to calculate total amount spent by the guest on completed reservations
        public async Task<double> GetTotalSpent(int guestId)
        {
            var exists = await _context.Guests.AnyAsync(g => g.Id == guestId);

            if (!exists)
                throw new KeyNotFoundException("Guest not found");

            return await _context.Reservations
                .Where(r => r.GuestId == guestId && r.Status == ReservationStatus.Completed)
                .SumAsync(r => r.TotalPrice);
        }


    }
}
