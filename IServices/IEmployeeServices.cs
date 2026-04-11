using Advanced_Hotel_Reservation_System.Models;
using Advanced_Hotel_Reservation_System.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Advanced_Hotel_Reservation_System.IServices
{
    public interface IEmployeeServices
    {
        Task Add(Employee employee);
        void Delete(int id);
        void Update(Employee employee);
        Employee GetById(int id);
        List<Employee> GetAll();
    }
}
