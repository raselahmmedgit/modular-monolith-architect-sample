using Microsoft.EntityFrameworkCore;
using rapid.erp.Core.Utility;
using rapid.erp.EntityModel.Admin;
using rapid.erp.EntityModel.Security;

namespace rapid.erp.EntityModel
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        //private const string connectionString = @"Data Source=10.42.65.188; Database=AuditManagement; User ID=sa; Password=Password1@; Integrated Security=False; Trusted_Connection=false; Encrypt=false; TrustServerCertificate=true; MultipleActiveResultSets=True";

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

        public virtual DbSet<AppSetting> AppSetting { get; set; }
        public virtual DbSet<AppModule> AppModule { get; set; }
        public virtual DbSet<AppSubModule> AppSubModule { get; set; }
        public virtual DbSet<AppMenu> AppMenu { get; set; }
        public virtual DbSet<AppSubMenu> AppSubMenu { get; set; }
        public virtual DbSet<AppMenuType> AppMenuType { get; set; }
        public virtual DbSet<AppUserRoleAndPermission> AppUserRoleAndPermission { get; set; }
        public virtual DbSet<AppUserTokenRefresh> AppUserTokenRefresh { get; set; }

        public DbSet<AppSqlResult> AppSqlResult { get; set; }

        public DbSet<Company> Company { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.HasDefaultSchema("dbo");

            //modelBuilder.Entity<AppSetting>(entity =>
            //{
            //    entity.HasKey(e => e.AppSettingId);

            //    entity.Property(e => e.AppSettingId).ValueGeneratedNever();

            //    entity.Property(e => e.AppSettingName).HasMaxLength(256);

            //    entity.Property(e => e.Key)
            //        .HasMaxLength(256)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Value)
            //        .HasMaxLength(256)
            //        .IsUnicode(false);
            //});

            //modelBuilder.Entity<AppModule>(entity =>
            //{
            //    entity.HasKey(e => e.ModuleId);

            //    entity.Property(e => e.ModuleId).ValueGeneratedNever();

            //    entity.Property(e => e.ActionName)
            //        .HasMaxLength(250)
            //        .IsUnicode(false);

            //    entity.Property(e => e.AreaName)
            //        .HasMaxLength(250)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ModuleName)
            //        .IsRequired()
            //        .HasMaxLength(250)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ControllerName)
            //        .HasMaxLength(250)
            //        .IsUnicode(false);

            //    entity.Property(e => e.IconPath)
            //        .HasMaxLength(250)
            //        .IsUnicode(false);

            //});

            //modelBuilder.Entity<AppSubModule>(entity =>
            //{
            //    entity.HasKey(e => e.SubModuleId);

            //    entity.Property(e => e.ModuleId).ValueGeneratedNever();

            //    entity.Property(e => e.ActionName)
            //        .HasMaxLength(250)
            //        .IsUnicode(false);

            //    entity.Property(e => e.AreaName)
            //        .HasMaxLength(250)
            //        .IsUnicode(false);

            //    entity.Property(e => e.SubModuleName)
            //        .IsRequired()
            //        .HasMaxLength(250)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ControllerName)
            //        .HasMaxLength(250)
            //        .IsUnicode(false);

            //    entity.Property(e => e.IconPath)
            //        .HasMaxLength(250)
            //        .IsUnicode(false);

            //});

            //modelBuilder.Entity<AppMenu>(entity =>
            //{
            //    entity.HasKey(e => e.MenuId);

            //    entity.Property(e => e.MenuId).ValueGeneratedNever();

            //    entity.Property(e => e.ActionName).HasMaxLength(256);

            //    entity.Property(e => e.AreaName)
            //        .HasMaxLength(250)
            //        .IsUnicode(false);

            //    entity.Property(e => e.MenuName)
            //        .HasMaxLength(250)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ControllerName).HasMaxLength(256);

            //    entity.Property(e => e.IconPath)
            //        .HasMaxLength(250)
            //        .IsUnicode(false);
            //});

            //modelBuilder.Entity<AppSubMenu>(entity =>
            //{
            //    entity.HasKey(e => e.SubMenuId);

            //    entity.Property(e => e.MenuId).ValueGeneratedNever();

            //    entity.Property(e => e.ActionName).HasMaxLength(256);

            //    entity.Property(e => e.AreaName)
            //        .HasMaxLength(250)
            //        .IsUnicode(false);

            //    entity.Property(e => e.SubMenuName)
            //        .HasMaxLength(250)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ControllerName).HasMaxLength(256);

            //    entity.Property(e => e.IconPath)
            //        .HasMaxLength(250)
            //        .IsUnicode(false);
            //});

            //modelBuilder.Entity<AppMenuType>(entity =>
            //{
            //    entity.HasKey(e => e.MenuTypeId);

            //    entity.Property(e => e.MenuTypeId).ValueGeneratedNever();

            //    entity.Property(e => e.MenuTypeName)
            //        .HasMaxLength(250)
            //        .IsUnicode(false);
            //});

            //modelBuilder.Entity<AppUserRoleAndPermission>(entity =>
            //{
            //    entity.HasKey(e => e.UserRoleAndPermissionId);

            //    entity.Property(e => e.UserRoleAndPermissionId).ValueGeneratedNever();

            //    entity.Property(e => e.RoleId);

            //});

            //modelBuilder.Entity<AppUserTokenRefresh>(entity =>
            //{
            //    entity.HasKey(e => e.UserTokenRefreshId);

            //    entity.Property(e => e.DeviceId)
            //        .HasMaxLength(256)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

            //    entity.Property(e => e.RefreshToken)
            //        .HasMaxLength(256)
            //        .IsUnicode(false);

            //    entity.Property(e => e.TimeStamp).HasColumnType("datetime");

            //    entity.Property(e => e.TokenHash)
            //        .HasMaxLength(256)
            //        .IsUnicode(false);

            //    entity.Property(e => e.TokenSalt)
            //        .HasMaxLength(256)
            //        .IsUnicode(false);

            //    entity.Property(e => e.UserId)
            //        .HasMaxLength(256)
            //        .IsUnicode(false);
            //});

            //modelBuilder.Entity<AppSqlResult>(entity =>
            //{
            //    entity.HasKey(e => e.ResultId);
            //});

            //modelBuilder.Entity<Company>(entity =>
            //{
            //    entity.Property(e => e.CreatedDate)
            //        .HasColumnType("datetime")
            //        .HasDefaultValueSql("(getdate())");
            //});

            //OnModelCreatingPartial(modelBuilder);

            //if (AppConstants.IsMasterDataInserted == false)
            //{
            //    AppDbContextSeedData.SeedData(modelBuilder);
            //}
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
