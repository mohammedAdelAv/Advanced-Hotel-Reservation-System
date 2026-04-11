using Advanced_Hotel_Reservation_System.Data;
using Advanced_Hotel_Reservation_System.IServices;
using Advanced_Hotel_Reservation_System.Models;

namespace Advanced_Hotel_Reservation_System.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly AppDbContext _context;

        public EmployeeServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
                throw new Exception("Employee not found");

            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }

        public void Update(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
        }

        public Employee GetById(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee == null)
                throw new Exception("Employee not found");

            return employee;
        }

        public List<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }
    }
}