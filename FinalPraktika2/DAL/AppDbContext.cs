using FinalPraktika2.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalPraktika2.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions opt):base(opt)
        {
        }
        public DbSet<Comment> Comments { get; set; }
    }
}
