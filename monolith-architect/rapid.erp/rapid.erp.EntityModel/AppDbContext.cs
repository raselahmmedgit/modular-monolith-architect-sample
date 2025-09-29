using Microsoft.EntityFrameworkCore;
using rapid.erp.Core.Utility;
using rapid.erp.EntityModel.Admin;

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

            modelBuilder.Entity<AppSetting>().Property(x => x.AppSettingId).ValueGeneratedOnAdd();
            modelBuilder.Entity<AppModule>().Property(x => x.ModuleId).ValueGeneratedOnAdd();
            modelBuilder.Entity<AppSubModule>().Property(x => x.SubModuleId).ValueGeneratedOnAdd();
            modelBuilder.Entity<AppMenu>().Property(x => x.MenuId).ValueGeneratedOnAdd();
            modelBuilder.Entity<AppSubMenu>().Property(x => x.SubMenuId).ValueGeneratedOnAdd();
            modelBuilder.Entity<AppMenuType>().Property(x => x.MenuTypeId).ValueGeneratedOnAdd();
            modelBuilder.Entity<AppUserRoleAndPermission>().Property(x => x.UserRoleAndPermissionId).ValueGeneratedOnAdd();
            modelBuilder.Entity<AppUserTokenRefresh>().Property(x => x.UserTokenRefreshId).ValueGeneratedOnAdd();
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
