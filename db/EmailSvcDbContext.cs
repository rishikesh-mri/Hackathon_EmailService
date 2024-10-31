using System;
using EmailService.entities;
using Microsoft.EntityFrameworkCore;

namespace EmailService.db;

public class EmailSvcDbContext : DbContext
{
    public DbSet<PropertyManager> PropertyManagers { get; set; }
    public DbSet<RegionalManager> RegionalManagers { get; set; }

    public string DbPath { get; set; }

    public EmailSvcDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "EmailSvc.db");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlite($"Data Source={DbPath}");
}
