using Advanced_Hotel_Reservation_System.enums;
using Advanced_Hotel_Reservation_System.Models;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Advanced_Hotel_Reservation_System.Data;

Console.WriteLine("Program Started...");

// Lists to store guests, rooms, and employees
List<Guest> guests = new List<Guest>();
List<Room> rooms = new List<Room>();
List<Employee> employees = new List<Employee>();

var serviceCollection = new ServiceCollection();
serviceCollection.AddDbContext<AppDbContext>(options => options.UseSqlServer("options.UseSqlServer(\"Server=192.168.1.19,1433;Database=HotelDB;Trusted_Connection=True;TrustServerCertificate=True\");"));
while (true)
{
    Console.WriteLine("\nWelcome to the Hotel Reservation Management System");
    Console.WriteLine("Please select an option:");
    Console.WriteLine("\n1. Add Guest");
    Console.WriteLine("2. Add Employee");
    Console.WriteLine("3. Add Room");
    Console.WriteLine("4. Show Available Rooms");
    Console.WriteLine("5. Make Room Reservation");
    Console.WriteLine("6. Show Guest Reservations");
    Console.WriteLine("7. Cancel Room Reservation");
    Console.WriteLine("8. Complete Reservation");
    Console.WriteLine("9. Show Total Revenue");
    Console.WriteLine("10. Delete Guest");
    Console.WriteLine("11. Show Top Customers");
    Console.WriteLine("12. Show Top Rooms");
    Console.WriteLine("13. Exit");

    Console.Write("Choose: ");
    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            {
                Console.WriteLine("Enter Guest Id:");
                int.TryParse(Console.ReadLine(), out int guestId);
                Console.WriteLine("Enter Guest Name:");
                string guestName = Console.ReadLine()!;
                Console.WriteLine("Enter Guest Email:");
                string gusetEmail = Console.ReadLine()!;
                Console.WriteLine("Enter Guest Phone Number:");
                string guestPhone = Console.ReadLine()!;
                Guest guest = new Guest(guestId, guestName, gusetEmail, guestPhone);
                guests.Add(guest);
                Console.WriteLine("Guest added successfully!");
                Console.WriteLine("Guest Details:");
                guest.DisplayInfo();
                Console.WriteLine("-----------------------------------------------------------------");
            }
            break;
        case "2":
            {
                Console.WriteLine("Enter Employee Id:");
                int.TryParse(Console.ReadLine(), out int employeeId);
                Console.WriteLine("Enter Employee Name:");
                string employeeName = Console.ReadLine()!;
                Console.WriteLine("Enter Employee Position:");
                string employeePosition = Console.ReadLine()!;
                Console.WriteLine("Enter Employee Salary:");
                double.TryParse(Console.ReadLine(), out double employeeSalary);
                Employee employee = new Employee(employeeId, employeeName, employeePosition, employeeSalary);
                employees.Add(employee);
                Console.WriteLine("Employee added successfully!");
                Console.WriteLine("Employee Details:");
                employee.DisplayInfo();
                Console.WriteLine("-----------------------------------------------------------------");
                break;
            }
        case "3":
            {
                Console.WriteLine("Enter Room Id:");
                int.TryParse(Console.ReadLine(), out int roomId);
                var roomExists = rooms.Any(r => r.RoomId == roomId);
                if (roomExists)
                {
                    Console.WriteLine("Room already exists!");
                    break;
                }
                Console.WriteLine("Enter Room Type (Single, Double, Suite):");
                RoomType roomType; Enum.TryParse(Console.ReadLine(), true, out roomType);
                Console.WriteLine("Enter Room Price:");
                double.TryParse(Console.ReadLine(), out double roomPrice);
                Room room = new Room(roomId, roomType, roomPrice);
                rooms.Add(room);
                Console.WriteLine("Room added successfully!");
                Console.WriteLine("Room Details:");
                room.DisplayInfo();
                Console.WriteLine("-----------------------------------------------------------------");
            }
            break;
        case "4":
            {
                if (!rooms.Any(r => r.Status == RoomStatus.Available))
                {
                    Console.WriteLine("No Available rooms");
                }
                else
                {
                    foreach (var r in rooms.Where(a => a.Status == RoomStatus.Available))
                    {
                        r.DisplayInfo();
                    }
                }
                Console.WriteLine("-----------------------------------------------------------------");
            }
            break;
        case "5":
            {
                Console.WriteLine("Enter Guest Id:");
                int.TryParse(Console.ReadLine(), out int guestId);
                Console.WriteLine("Enter Room Id:");
                int.TryParse(Console.ReadLine(), out int roomId);
                Guest? findGuest = guests.FirstOrDefault(g => g.Id == guestId);
                if (findGuest == null)
                {
                    Console.WriteLine("Guest not found!");
                    break;
                }
                var findRoom = rooms.FirstOrDefault(r => r.RoomId == roomId);
                if (findRoom == null)
                {
                    Console.WriteLine("Room not found!");
                    break;
                }
                if (findRoom.Status == RoomStatus.Booked)
                {
                    Console.WriteLine("Room is already booked!");
                    break;
                }
                Console.WriteLine("Enter Number of Nights:");
                int.TryParse(Console.ReadLine(), out int nights);
                if (nights <= 0)
                {
                    Console.WriteLine("Nights must be greater than zero.");
                    break;
                }
                var res = findGuest.AddReservation(findRoom, nights);
                Console.WriteLine("Reservation completed successfully!");
                Console.WriteLine("Reservation Details:");
                Console.WriteLine(res);
                Console.WriteLine("-----------------------------------------------------------------");
            }
            break;
        case "6":
            {
                Console.WriteLine("Enter Guest Id:");
                int.TryParse(Console.ReadLine(), out int guestId);
                Guest? findGuest = guests.FirstOrDefault(g => g.Id == guestId);
                if (findGuest == null)
                {
                    Console.WriteLine("Guest not found!");
                    break;
                }
                findGuest.ShowReservations();
                Console.WriteLine("-----------------------------------------------------------------");
            }
            break;
        case "7":
            {
                Console.WriteLine("Enter Guest Id:");
                int.TryParse(Console.ReadLine(), out int guestId);
                Guest? findGuest = guests.FirstOrDefault(g => g.Id == guestId);
                if (findGuest == null)
                {
                    Console.WriteLine("Guest not found!");
                    break;
                }
                Console.WriteLine("Enter Room Id:");
                int.TryParse(Console.ReadLine(), out int roomId);
                var findRoom = rooms.FirstOrDefault(r => r.RoomId == roomId);
                if (findRoom == null)
                {
                    Console.WriteLine("Room not found!");
                    break;
                }
                findGuest.CanselReservations(roomId);
                Console.WriteLine("-----------------------------------------------------------------");
            }
            break;
        case "8":
            {
                Console.WriteLine("Enter Guest Id:");
                int.TryParse(Console.ReadLine(), out int guestId);
                Console.WriteLine("Enter Room Id:");
                int.TryParse(Console.ReadLine(), out int roomId);
                Guest? findGuest = guests.FirstOrDefault(g => g.Id == guestId);
                if (findGuest == null)
                {
                    Console.WriteLine("Guest not found!");
                    break;
                }
                var reservation = findGuest.Reservations.FirstOrDefault(r => r.Room.RoomId == roomId && r.Status == ReservationStatus.Active);
                if (reservation == null)
                {
                    Console.WriteLine("Active reservation not found!");
                    break;
                }
                reservation.Complete();
                Console.WriteLine("Reservation completed successfully!");
                Console.WriteLine("-----------------------------------------------------------------");

            }
            break;
        case "9":
            {
                double totalRevenue = guests.Sum(g => g.GetTotalSpent());
                Console.WriteLine($"Total Revenue: {totalRevenue}$");
                Console.WriteLine("-----------------------------------------------------------------");
            }
            break;
        case "10":
            {
                Console.WriteLine("Enter Guest Id:");
                int.TryParse(Console.ReadLine(), out int guestId);
                Guest? findGuest = guests.FirstOrDefault(g => g.Id == guestId);
                if (findGuest == null)
                {
                    Console.WriteLine("Guest not found!");
                    break;
                }
                else if (findGuest.HasActiveReservations())
                {
                    Console.WriteLine("Cannot delete guest with active reservations!");
                }
                else
                {
                    guests.Remove(findGuest);
                    Console.WriteLine("Guest deleted successfully!");
                }
                Console.WriteLine("-----------------------------------------------------------------");
            }
            break;

        case "11":
            {
                int count = 1;
                var topSpend = guests.OrderByDescending(s => s.GetTotalSpent()).Take(3);
                Console.WriteLine("Top Customers:");
                foreach (var g in topSpend)
                {                    
                    Console.WriteLine($"{count}. Guest Name: {g.Name} | Total Spent: {g.GetTotalSpent():C}");
                    count++;
                }
            }
            break;
        case "12":
            {
                int count = 1;
                var topRooms = rooms.OrderByDescending(r => r.ReservationCount).Take(3);
                Console.WriteLine("Top Rooms:");
                foreach (var r in topRooms)
                {
                    Console.WriteLine($"{count}. Room Name: {r.RoomId} | Reservations: {r.ReservationCount}");
                    count++;
                }
            }
            break;

        case "13":
            return;
    }
}