using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace sampleapp.Models
{
    public class TodoContext : DbContext
    {
        public DbSet<Todo>? Todos { get; set; }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=tcp:ingodbsrv.database.windows.net,1433;Initial Catalog=ingodb;Persist Security Info=False;User ID=ingodbadmin;Password=W9qxsriQ5;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }
    public class Todo
    {
        public int TodoId { get; set; }
        public string Title { get; set; } = "";
        public string Details { get; set; } = "";
        public DateTime CreationDate { get; set; } = DateTime.Today;
        public DateTime CompletionDate { get; set; } = DateTime.MaxValue;
        public bool isCompleted { get; set; } = false;
        public bool isStarted { get; set; } = false;
    }

    public class TodoForm
    {
        [BindProperty]
        public string Title { get; set; } = "";
        [BindProperty]
        public string Details { get; set; } = "";
    }
}