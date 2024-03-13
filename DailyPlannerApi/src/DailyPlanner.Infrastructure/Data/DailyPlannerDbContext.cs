using System.Reflection;
using DailyPlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanner.Infrastructure.Data;

public class DailyPlannerDbContext : DbContext
{
    public DailyPlannerDbContext(DbContextOptions<DailyPlannerDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Card> Cards { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<Column> Columns { get; set; }
    public DbSet<ToDoList> DoToLists { get; set; }
    public DbSet<ToDoItem> DoToItems { get; set; }
}