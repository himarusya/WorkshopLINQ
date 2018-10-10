using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace lab3
{
    public partial class WorkshopContext : DbContext
    {
        public WorkshopContext()
        {
        }

        public WorkshopContext(DbContextOptions<WorkshopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Mechanic> Mechanics { get; set; }
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<Workroom> Workroom { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                // установка пути к текущему каталогу
                builder.SetBasePath(Directory.GetCurrentDirectory());
                // получаем конфигурацию из файла appsettings.json
                builder.AddJsonFile("appsettings.json");
                // создаем конфигурацию
                var config = builder.Build();
                // получаем строку подключения
                string connectionString = config.GetConnectionString("SQLConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.CarId);

                entity.Property(e => e.CarId).HasColumnName("carID");

                entity.Property(e => e.BodyNumber).HasColumnName("bodyNumber");

                entity.Property(e => e.Colour)
                    .HasColumnName("colour")
                    .HasMaxLength(20);

                entity.Property(e => e.EngineNumber).HasColumnName("engineNumber");

                entity.Property(e => e.Model)
                    .HasColumnName("model")
                    .HasMaxLength(20);

                entity.Property(e => e.OwnerId).HasColumnName("ownerID");

                entity.Property(e => e.Vis).HasColumnName("vis");

                entity.Property(e => e.YearOfIssue)
                    .HasColumnName("yearOfIssue")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Mechanic>(entity =>
            {
                entity.HasKey(e => e.MechanicId);

                entity.Property(e => e.MechanicId).HasColumnName("mechanicID");

                entity.Property(e => e.Experience).HasColumnName("experience");

                entity.Property(e => e.FioMechanic)
                    .HasColumnName("fioMechanic")
                    .HasMaxLength(50);

                entity.Property(e => e.Qualification)
                    .HasColumnName("qualification")
                    .HasMaxLength(20);

                entity.Property(e => e.Salary).HasColumnName("salary");
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(e => e.OwnerId);

                entity.Property(e => e.OwnerId).HasColumnName("ownerID");

                entity.Property(e => e.Adress)
                    .HasColumnName("adress")
                    .HasMaxLength(50);

                entity.Property(e => e.DriverLicense).HasColumnName("driverLicense");

                entity.Property(e => e.FioOwner)
                    .HasColumnName("fioOwner")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone).HasColumnName("phone");
            });

            modelBuilder.Entity<Workroom>(entity =>
            {
                entity.Property(e => e.WorkroomId).HasColumnName("workroomID");

                entity.Property(e => e.CarId).HasColumnName("carID");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.MechanicId).HasColumnName("mechanicID");

                entity.Property(e => e.ReceiptDate)
                    .HasColumnName("receiptDate")
                    .HasColumnType("date");
            });
        }
    }
}
