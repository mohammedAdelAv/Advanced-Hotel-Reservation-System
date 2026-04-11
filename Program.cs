using Advanced_Hotel_Reservation_System.enums;
using Advanced_Hotel_Reservation_System.IServices;
using Advanced_Hotel_Reservation_System.Models;
using Advanced_Hotel_Reservation_System.Services;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Program Started...");

var services = new ServiceCollection();

services.AddScoped<IGuestServices, GuestServices>();
services.AddScoped<IEmployeeServices, EmployeeServices>();
services.AddScoped<IRoomServices, RoomServices>();
services.AddScoped<IReservationServices, reservationServices>();

var provider = services.BuildServiceProvider();

using (var scope = provider.CreateScope())
{
    var guestService = scope.ServiceProvider.GetRequiredService<IGuestServices>();
    var roomService = scope.ServiceProvider.GetRequiredService<IRoomServices>();
    var reservationService = scope.ServiceProvider.GetRequiredService<IReservationServices>();
    var employeeService = scope.ServiceProvider.GetRequiredService<IEmployeeServices>();

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
                    string guestEmail = Console.ReadLine()!;
                    Console.WriteLine("Enter Guest Phone Number:");
                    string guestPhone = Console.ReadLine()!;
                    var guest = new Guest(0, guestName, guestEmail, guestPhone);
                    await guestService.Add(guest);
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
                    var employee = new Employee(0, employeeName, employeePosition, employeeSalary);
                    await employeeService.Add(employee);
                    Console.WriteLine("Employee added successfully!");
                    Console.WriteLine("Employee Details:");
                    employee.DisplayInfo();
                    Console.WriteLine("-----------------------------------------------------------------");
                }
                break;
            case "3":
                {
                    Console.WriteLine("Enter Room Id:");
                    int.TryParse(Console.ReadLine(), out int roomId);
                    var roomExists = await roomService.GetById(roomId);
                    if (roomExists != null)
                    {
                        Console.WriteLine("Room already exists!");
                        break;
                    }
                    Console.WriteLine("Enter Room Type (Single, Double, Suite):");
                    RoomType roomType; Enum.TryParse(Console.ReadLine(), true, out roomType);
                    Console.WriteLine("Enter Room Price:");
                    double.TryParse(Console.ReadLine(), out double roomPrice);
                    var room = new Room(roomId, roomType, roomPrice);
                    await roomService.Add(room);
                    Console.WriteLine("Room added successfully!");
                    Console.WriteLine("Room Details:");
                    room.DisplayInfo();
                    Console.WriteLine("-----------------------------------------------------------------");
                }
                break;
            case "4":
                {
                    var rooms = await roomService.GetAll();

                    var availableRooms = rooms
                       .Where(r => r.Status == RoomStatus.Available)
                       .ToList();

                    if (!availableRooms.Any())
                    {
                        Console.WriteLine("No Available rooms");
                    }
                    else
                    {
                        foreach (var r in availableRooms)
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
                    var findGuest =  await guestService.GetById(guestId);
                    if (findGuest == null)
                    {
                        Console.WriteLine("Guest not found!");
                        break;
                    }
                    var findRoom = await roomService.GetById(roomId);
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
                    var res = await reservationService.AddReservation(findGuest, findRoom, nights);
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
                    var findGuest = await guestService.GetById(guestId);
                    if (findGuest == null)
                    {
                        Console.WriteLine("Guest not found!");
                        break;
                    }
                    await reservationService.ShowAllActiveReservations(findGuest.Id);
                    Console.WriteLine("-----------------------------------------------------------------");
                }
                break;
            case "7":
                {
                    Console.WriteLine("Enter Guest Id:");
                    int.TryParse(Console.ReadLine(), out int guestId);
                    var findGuest = await guestService.GetById(guestId);
                    if (findGuest == null)
                    {
                        Console.WriteLine("Guest not found!");
                        break;
                    }
                    Console.WriteLine("Enter Room Id:");
                    int.TryParse(Console.ReadLine(), out int roomId);
                    var findRoom = await roomService.GetById(roomId);
                    if (findRoom == null)
                    {
                        Console.WriteLine("Room not found!");
                        break;
                    }
                    await reservationService.CancelReservations(findGuest, roomId);
                    Console.WriteLine("-----------------------------------------------------------------");
                }
                break;
            case "8":
                {
                    Console.WriteLine("Enter Guest Id:");
                    int.TryParse(Console.ReadLine(), out int guestId);
                    Console.WriteLine("Enter Room Id:");
                    int.TryParse(Console.ReadLine(), out int roomId);
                    var findGuest = await guestService.GetById(guestId);
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
                    Console.WriteLine("Enter Guest Id:");
                    int.TryParse(Console.ReadLine(), out int guestId);
                    double totalRevenue = await reservationService.GetTotalSpent(guestId);
                    Console.WriteLine($"Total Revenue: {totalRevenue:C}");
                    Console.WriteLine("-----------------------------------------------------------------");
                }
                break;
            case "10":
                {
                    Console.WriteLine("Enter Guest Id:");
                    int.TryParse(Console.ReadLine(), out int guestId);
                    var findGuest = await guestService.GetById(guestId);
                    if (findGuest == null)
                    {
                        Console.WriteLine("Guest not found!");
                        break;
                    }
                    else if (await reservationService.HasActiveReservations(findGuest))
                    {
                        Console.WriteLine("Cannot delete guest with active reservations!");
                    }
                    else
                    {
                        await guestService.Delete(findGuest.Id);
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
}


