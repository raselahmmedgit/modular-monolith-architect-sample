using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using rapid.erp.Core.Security.EntityModel;
using rapid.erp.Core.Utility;

namespace rapid.erp.Core.Security
{
    public class AppIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string> //IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public AppIdentityDbContext()
        {
        }

        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = AppConstants.DefaultConnectionString;

                if (AppConstants.IsMSSqlDatabase == true)
                {
                    //MS SqlServer
                    optionsBuilder.UseSqlServer(connectionString);
                }
                else if (AppConstants.IsMySqlDatabase == true)
                {
                    //MySQL
                    optionsBuilder.UseMySQL(connectionString);
                }
                else if (AppConstants.IsPostgreSQLDatabase == true)
                {
                    //PostgreSQL
                    optionsBuilder.UseNpgsql(connectionString);
                }
                else if (AppConstants.IsOracleDatabase == true)
                {
                    //Oracle
                    optionsBuilder.UseOracle(connectionString);
                }
            }
        }

        //Identity
        public virtual DbSet<AspNetRole> AspNetRole { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaim { get; set; }
        public virtual DbSet<AspNetUser> AspNetUser { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaim { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogin { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRole { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserToken { get; set; }
        //Identity

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<ApplicationUser>(entity => entity.ToTable("AspNetUser"));
            //modelBuilder.Entity<ApplicationRole>(entity => entity.ToTable("AspNetRole"));
            //modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.ToTable("AspNetUserRole"));
            //modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable("AspNetUserClaim"));
            //modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable("AspNetRoleClaim"));
            //modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable("AspNetUserLogin"));
            //modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.ToTable("AspNetUserToken"));

            //Identity
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.ToTable("AspNetRole", "dbo");

                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.ToTable("AspNetRoleClaim", "dbo");

                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.ToTable("AspNetUser", "dbo");

                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.ToTable("AspNetUserClaim", "dbo");

                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.ToTable("AspNetUserLogin", "dbo");

                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.ToTable("AspNetUserRole", "dbo");

                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.Property(e => e.UserId);

                entity.Property(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.ToTable("AspNetUserToken", "dbo");

                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(256);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });
            //Identity

        }
    }
}