using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace sampleapp.Models
{
    public class TodoContext : DbContext
    {
        public DbSet<Todo>? Todos { get; set; }
        private string _connectionString;

        public TodoContext(string connectionString) : base()
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(_connectionString);
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