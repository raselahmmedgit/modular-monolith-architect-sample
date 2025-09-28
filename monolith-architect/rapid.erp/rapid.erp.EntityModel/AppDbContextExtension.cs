using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using rapid.erp.EntityModel.Extended;

namespace rapid.erp.EntityModel
{
    public partial class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public int UserId = 0;
        public string LoginUserId;
        public string UserName = string.Empty;
        public int ActionState = 0;

        public object Question { get; set; }

        public AppDbContext(IHttpContextAccessor httpContextAccessor, DbContextOptions<AppDbContext> options)
           : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            var dataUserId = httpContextAccessor.HttpContext?.User?.Identity;

            //if (dataUserId != null)
            //{
            //    UserId = Convert.ToInt32(dataUserId.GetAppUserId());
            //    LoginUserId = dataUserId.UserId();
            //}

            //var dataUserName = httpContextAccessor.HttpContext?.User?.Identity.GetFullName();
            //if (dataUserName != null)
            //{
            //    UserName = dataUserName;
            //}

            Database.SetCommandTimeout(150000);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
           
            using var tran = await base.Database.BeginTransactionAsync();
            try
            {
                this.UpdateAuditableEntities(UserId);
                var result = await base.SaveChangesAsync(cancellationToken);
                await tran.CommitAsync();
                var d = tran.TransactionId;
                return result;
            }
            catch (Exception)
            {
                await tran.RollbackAsync();
                throw;
            }
           
        }
        public void PreventPropertyOverwrite<TProperty>(EntityEntry dbEntry, string propertyName)
        {
            var propertyEntry = dbEntry.Property(propertyName);
            if (propertyEntry.IsModified)
            {
                propertyEntry.IsModified = false;
            }
        }
        public EntityEntry ChangedEntries()
        {
            var changedEntries = this.ChangeTracker.Entries()
                   .Where(e => e.State == EntityState.Added
                            || e.State == EntityState.Modified
                            || e.State == EntityState.Deleted);
            return changedEntries.FirstOrDefault();
        }
    }
}