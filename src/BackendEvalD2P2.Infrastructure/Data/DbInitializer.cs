using Microsoft.EntityFrameworkCore;

namespace BackendEvalD2P2.Infrastructure.Data;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.Migrate();
        
        context.SaveChanges();
    }
}