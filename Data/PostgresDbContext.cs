using Microsoft.EntityFrameworkCore;
using webapitest.Repository.Models;
using webapitest.Repository.Models.Distortions;

namespace webapitest.Data;

public class PostgresDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public PostgresDbContext(DbContextOptions<PostgresDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Thought> Thoughts { get; set; }
    public DbSet<Distortion> Distortion { get; set; }

    public DbSet<Event> Events { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Access the connection string from appsettings.json
        var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";

        // Override with environment variable if exists
        var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        if (!string.IsNullOrEmpty(databaseUrl)) connectionString = ConvertDatabaseUrlToConnectionString(databaseUrl);

        // Configure the database connection for PostgreSQL
        optionsBuilder.UseNpgsql(connectionString);
        // Replace "your_postgresql_connection_string_here" with the actual connection string for your PostgreSQL database.
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Thought>()
            .HasOne<User>(e => e.User)
            .WithMany(e => e.Thoughts)
            .HasForeignKey(e => e.UserId)
            .IsRequired();

        modelBuilder.Entity<Distortion>()
            .HasMany(e => e.Thoughts)
            .WithMany(e => e.Distortions);

        modelBuilder.Entity<Event>()
            .HasOne<User>(e => e.User)
            .WithMany(e => e.Events)
            .HasForeignKey(e => e.UserId)
            .IsRequired();

        modelBuilder.Entity<Event>()
            .HasMany(e => e.Distortions)
            .WithMany(e => e.Events);
    }

    private static string ConvertDatabaseUrlToConnectionString(string databaseUrl)
    {
        var uri = new Uri(databaseUrl);
        var userInfo = uri.UserInfo.Split(':');
        return
            $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=True;";
    }
}