using Microsoft.EntityFrameworkCore;
using StudentMN.Models.Account;
using StudentMN.Models.Base;
using StudentMN.Models.Class;
using StudentMN.Models.PermissionModels;
using StudentMN.Models.ScoreStudent;

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
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<Major> Majors { get; set; }


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
            modelBuilder.Entity<Student>()
                    .HasOne(s => s.Class)
                    .WithMany(c => c.Students)
                    .HasForeignKey(s => s.ClassId)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Classes>()
                    .HasOne(c => c.Major)
                    .WithMany(m => m.Classes)
                    .HasForeignKey(c => c.MajorId)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Classes>()
                    .HasOne(c => c.Teacher)
                    .WithMany()
                    .HasForeignKey(c => c.TeacherId)
                    .OnDelete(DeleteBehavior.Restrict);

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
            modelBuilder.Entity<EnrollmentCourseSection>()
                .HasKey(x => new { x.StudentId, x.CourseSectionId });

            modelBuilder.Entity<EnrollmentCourseSection>()
                .HasOne(x => x.Student)
                .WithMany(s => s.EnrollmentCourseSections)
                .HasForeignKey(x => x.StudentId);

            modelBuilder.Entity<EnrollmentCourseSection>()
                .HasOne(x => x.CourseSection)
                .WithMany(c => c.EnrollmentCourseSections)
                .HasForeignKey(x => x.CourseSectionId);
            modelBuilder.Entity<CourseSection>()
                .HasOne(cs => cs.Teacher)
                .WithMany(t => t.CourseSections)
                .HasForeignKey(cs => cs.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CourseSection>()
                .HasOne(cs => cs.Subject)
                .WithMany(s => s.CourseSections)
                .HasForeignKey(cs => cs.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Score>()
                .HasOne(s => s.CourseSection)
                .WithMany(cs => cs.Scores)
                .HasForeignKey(s => s.CourseSectionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Score>()
                .HasOne(s => s.Student)
                .WithMany()
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

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