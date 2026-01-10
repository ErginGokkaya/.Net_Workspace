namespace Note_Taking_App.Datas
{
    using Microsoft.EntityFrameworkCore;
    using Note_Taking_App.Models;

    public class NoteDbContext : DbContext
    {
        public NoteDbContext(DbContextOptions<NoteDbContext> options) : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
    }
}