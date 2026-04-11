using Advanced_Hotel_Reservation_System.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Advanced_Hotel_Reservation_System.IServices
{
    public interface IGuestServices
    {

        Task Add(Guest guest);
        Task<List<Guest>> GetAll();
        Task<Guest> GetById(int id);  
        Task Delete(int id);

    }
}
