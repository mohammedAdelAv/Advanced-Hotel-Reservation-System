using Advanced_Hotel_Reservation_System.enums;
using Advanced_Hotel_Reservation_System.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Advanced_Hotel_Reservation_System.IServices
{
    public interface IRoomServices
    {
        Task Add(Room room);
        Task<List<Room>> GetAll();
        Task<Room?> GetById(int id);
        Task UpdateStatus(int roomId, RoomStatus status);
        Task Delete(int id);
       
    }
}
