using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccessLayer
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseNpgsql("Host=localhost;Port=5432;Database=ELibrary;Username=postgres;Password=123Qwe123");
            return new DataContext(options.Options);
        }
    }
}