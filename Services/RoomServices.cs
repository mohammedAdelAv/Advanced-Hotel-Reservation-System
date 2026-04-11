using Advanced_Hotel_Reservation_System.Data;
using Advanced_Hotel_Reservation_System.enums;
using Advanced_Hotel_Reservation_System.IServices;
using Advanced_Hotel_Reservation_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Advanced_Hotel_Reservation_System.Services
{
    public class RoomServices : IRoomServices
    {
        private readonly AppDbContext _context;

        public RoomServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Room>> GetAll()
        {
            return await _context.Rooms.Include(r => r.Reservations).ToListAsync();
        }

        public async Task<Room?> GetById(int id)
        {
            return await _context.Rooms.Include(r => r.Reservations).FirstOrDefaultAsync(r => r.RoomId == id);                   
        }

        public async Task UpdateStatus(int roomId, RoomStatus status)
        {
            var existingRoom = await _context.Rooms.FindAsync(roomId);
            if (existingRoom == null)
                throw new KeyNotFoundException("Room not found");

            existingRoom.UpdateStatus(status);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                throw new KeyNotFoundException("Room not found");

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();


        }
    }
}
