using Microsoft.EntityFrameworkCore;
using StudentMN.Models;
using StudentMN.Models.Account;
using StudentMN.Models.Permission;

namespace StudentMN.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình BaseEntity cho tất cả các entity
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property("CreatedAt")
                        .HasDefaultValueSql("GETDATE()");
                }
            }

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
                entity.Property(e => e.FullName).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();

                entity.HasOne(e => e.Role)
                    .WithMany(r => r.Users)
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Role configuration
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.RoleName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(200);
            });

            // Permission configuration
            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PermissionName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(200);
            });

            // RolePermission configuration
            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Role)
                    .WithMany(r => r.RolePermissions)
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Permission)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(e => e.PermissionId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Tạo composite unique index
                entity.HasIndex(e => new { e.RoleId, e.PermissionId }).IsUnique();
            });

            // Student configuration
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StudentCode).IsRequired().HasMaxLength(20);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Gender).HasMaxLength(10);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).HasMaxLength(15);
                entity.HasIndex(e => e.StudentCode).IsUnique();

                entity.HasOne(e => e.User)
                    .WithOne()
                    .HasForeignKey<Student>(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, RoleName = "Admin", Description = "Quản trị viên", CreatedAt = DateTime.Now },
                new Role { Id = 2, RoleName = "Student", Description = "Sinh viên", CreatedAt = DateTime.Now },
                new Role { Id = 3, RoleName = "Teacher", Description = "Giảng viên", CreatedAt = DateTime.Now }
            );

            // Seed Admin User (password: Admin@123)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    FullName = "Administrator",
                    Email = "admin@studentmn.com",
                    RoleId = 1,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                }
            );

            // Seed Permissions
            modelBuilder.Entity<Permission>().HasData(
                new Permission { Id = 1, PermissionName = "ViewUsers", Description = "Xem danh sách người dùng", CreatedAt = DateTime.Now },
                new Permission { Id = 2, PermissionName = "CreateUser", Description = "Tạo người dùng mới", CreatedAt = DateTime.Now },
                new Permission { Id = 3, PermissionName = "UpdateUser", Description = "Cập nhật người dùng", CreatedAt = DateTime.Now },
                new Permission { Id = 4, PermissionName = "DeleteUser", Description = "Xóa người dùng", CreatedAt = DateTime.Now },
                new Permission { Id = 5, PermissionName = "ViewStudents", Description = "Xem danh sách sinh viên", CreatedAt = DateTime.Now },
                new Permission { Id = 6, PermissionName = "ManageStudents", Description = "Quản lý sinh viên", CreatedAt = DateTime.Now }
            );

            // Seed RolePermissions (Admin có tất cả quyền)
            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission { Id = 1, RoleId = 1, PermissionId = 1, CreatedAt = DateTime.Now },
                new RolePermission { Id = 2, RoleId = 1, PermissionId = 2, CreatedAt = DateTime.Now },
                new RolePermission { Id = 3, RoleId = 1, PermissionId = 3, CreatedAt = DateTime.Now },
                new RolePermission { Id = 4, RoleId = 1, PermissionId = 4, CreatedAt = DateTime.Now },
                new RolePermission { Id = 5, RoleId = 1, PermissionId = 5, CreatedAt = DateTime.Now },
                new RolePermission { Id = 6, RoleId = 1, PermissionId = 6, CreatedAt = DateTime.Now },
                // Student chỉ có quyền xem
                new RolePermission { Id = 7, RoleId = 2, PermissionId = 5, CreatedAt = DateTime.Now }
            );
        }

        // Override SaveChanges để tự động cập nhật UpdatedAt
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.Now;
            }

            return base.SaveChanges();
        }
    }
}