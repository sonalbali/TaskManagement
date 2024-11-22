using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Task_Managment_System.Model.Entities;

namespace Task_Managment_System.Data
{
    public class TaskDBContext(DbContextOptions<TaskDBContext> options) : DbContext(options)
    {
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Registration> Users { get; set; }

        }
}
    