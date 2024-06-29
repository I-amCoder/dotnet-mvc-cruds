

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppMvc.Models;

namespace WebAppMvc.Services
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostMetaData> PostsMetaData { get; set; }
        public  DbSet<Comment> Comments { get; set; }

        private readonly string _adminId = Guid.NewGuid().ToString();
        private readonly string _adminRoleId = Guid.NewGuid().ToString();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>()
                .HasMany(c => c.Posts)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Post>()
                .HasMany(p=>p.Comments)
                .WithOne(c=>c.Post)
                .HasForeignKey(c=>c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Post>()
                    .HasOne(p => p.MetaData)
                    .WithOne(d => d.Post)
                    .HasForeignKey<PostMetaData>(d=>d.PostId)
                    .OnDelete(DeleteBehavior.Cascade);


            SeedRoles(builder);
            SeedUsers(builder);
        }

        protected void SeedRoles(ModelBuilder builder) 
        {
            IdentityRole admin = new()
            {
                Id = _adminRoleId,
                Name = "admin",
                NormalizedName = "ADMIN"
            };

            IdentityRole user = new()
            {
                Name = "user",
                NormalizedName = "USER",
            };

            builder.Entity<IdentityRole>().HasData(admin,user);
        }

        protected void SeedUsers(ModelBuilder builder)
        {
            string username = "admin@admin.com";
            ApplicationUser user = new()
            {
                Id = _adminId,
                FirstName = "Junaid",
                LastName = "Ali",
                UserName = username,
                Email = username,
                NormalizedEmail = username.ToUpper(),
                NormalizedUserName = username.ToUpper(),
            };

            user.PasswordHash = GetPasswordHash(user, "password");

            builder.Entity<ApplicationUser>().HasData(user);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = _adminRoleId,
                UserId = _adminId
            });

        }

        protected string GetPasswordHash(ApplicationUser user,string password)
        {
            PasswordHasher<ApplicationUser> hasher = new();
            return hasher.HashPassword(user,password);
        }

        
    }
}
