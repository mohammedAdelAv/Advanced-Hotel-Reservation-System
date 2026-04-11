using Advanced_Hotel_Reservation_System.Data;
using Advanced_Hotel_Reservation_System.enums;
using Advanced_Hotel_Reservation_System.IServices;
using Advanced_Hotel_Reservation_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Advanced_Hotel_Reservation_System.Services
{
    internal class GuestServices : IGuestServices
    {
        private readonly AppDbContext _context;

        public GuestServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Guest guest)
        {
            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Guest>> GetAll()
        {
            return await _context.Guests.Include(g => g.Reservations).ToListAsync();
        }

        public async Task<Guest> GetById(int id)
        {
            var guest = await _context.Guests.Include(g => g.Reservations).FirstOrDefaultAsync(g => g.Id == id);
            if (guest == null)
                throw new KeyNotFoundException("Guest not found");
            return guest;
        }

        public async Task Delete(int id)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null)
                throw new KeyNotFoundException("Guest not found");
            _context.Guests.Remove(guest);
            await _context.SaveChangesAsync();
        }
    
    }
}
