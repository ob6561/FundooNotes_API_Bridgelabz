
using FundooNotes.API.ModelLayer.Entities;
using Microsoft.EntityFrameworkCore;
namespace FundooNotes.API.Data
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
