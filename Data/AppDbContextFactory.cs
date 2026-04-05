using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Advanced_Hotel_Reservation_System.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer("Server=192.168.1.19,1433;Database=HotelDB;User Id=sa;Password=12345;TrustServerCertificate=True");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}