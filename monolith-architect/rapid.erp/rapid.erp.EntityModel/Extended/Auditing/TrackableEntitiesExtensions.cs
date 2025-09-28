using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Newtonsoft.Json;

using System;
using System.Linq;

namespace rapid.erp.EntityModel.Extended
{
    public static partial class DbContextExtensions
    {
        /// <summary>
        /// Populate special properties for all Trackable Entities in context.
        /// </summary>
        public static void UpdateTrackableEntities<TUserId>(this DbContext context, TUserId editorUserId)
            where TUserId : struct
        {
            DateTime utcNow = DateTime.Now;

            var changedEntries = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added
                         || e.State == EntityState.Modified
                         || e.State == EntityState.Deleted);

            foreach (var dbEntry in changedEntries)
            {
                UpdateTrackableEntity(dbEntry, utcNow);
            }
        }

        private static void UpdateTrackableEntity(EntityEntry dbEntry, DateTime utcNow)
        {
            object entity = dbEntry.Entity;

            switch (dbEntry.State)
            {
                case EntityState.Added:
                    if (entity is ICreationTrackable creationTrackable)
                    {
                        creationTrackable.CreatedDate = utcNow;
                    }
                    break;

                case EntityState.Modified:
                    if (entity is IModificationTrackable modificatonTrackable)
                    {
                        modificatonTrackable.UpdatedDate = utcNow;
                        dbEntry.CurrentValues[nameof(IModificationTrackable.UpdatedDate)] = utcNow;

                        if (entity is ICreationTrackable)
                        {
                            PreventPropertyOverwrite<DateTime>(dbEntry, nameof(ICreationTrackable.CreatedDate));
                        }
                    }
                    break;

                case EntityState.Deleted:
                    if (entity is ISoftDeletable softDeletable)
                    {
                        dbEntry.State = EntityState.Unchanged;
                        softDeletable.IsDeleted = true;
                        dbEntry.CurrentValues[nameof(ISoftDeletable.IsDeleted)] = true;

                        if (entity is IDeletionTrackable deletionTrackable)
                        {
                            deletionTrackable.DeletedDate = utcNow;
                            dbEntry.CurrentValues[nameof(IDeletionTrackable.DeletedDate)] = utcNow;
                        }
                    }
                    break;

                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// If we set <see cref="EntityEntry.State"/> to <see cref="EntityState.Modified"/> on entity with
        /// empty <see cref="ICreationTrackable.CreatedUtc"/> or <see cref="ICreationAuditable.CreatorUserId"/>
        /// we should not overwrite database values.
        /// https://github.com/gnaeus/EntityFramework.CommonTools/issues/4
        /// </summary>
        private static void PreventPropertyOverwrite<TProperty>(EntityEntry dbEntry, string propertyName)
        {
            var propertyEntry = dbEntry.Property(propertyName);

            //if (propertyEntry.IsModified && Equals(dbEntry.CurrentValues[propertyName], default(TProperty)))
            //{
            //    propertyEntry.IsModified = false;
            //}
            if (propertyEntry.IsModified)
            {
                propertyEntry.IsModified = false;
            }
        }

        public static void Log()
        {

        }
        public static void LogTrackableEntities<TUserId>(this DbContext context, TUserId editorUserId)
            where TUserId : struct
        {
            DateTime utcNow = DateTime.UtcNow;

            var changedEntries = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added
                         || e.State == EntityState.Modified
                         || e.State == EntityState.Deleted);

            foreach (var dbEntry in changedEntries)
            {
                var jObj = JsonConvert.SerializeObject(dbEntry, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
        }
    }
}
