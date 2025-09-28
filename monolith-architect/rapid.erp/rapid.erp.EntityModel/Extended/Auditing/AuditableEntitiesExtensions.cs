using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System;
using System.Linq;


namespace rapid.erp.EntityModel.Extended
{
    public static partial class DbContextExtensions
    {
        /// <summary>
        /// Populate special properties for all Auditable Entities in context.
        /// </summary>
        public static void UpdateAuditableEntities<TUserId>(this DbContext context, TUserId editorUserId)
            where TUserId : struct
        {
            DateTime utcNow = DateTime.Now;

            var changedEntries = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added
                         || e.State == EntityState.Modified
                         || e.State == EntityState.Deleted);

            foreach (var dbEntry in changedEntries)
            {
                UpdateAuditableEntity(dbEntry, utcNow, editorUserId);
            }
        }

        /// <summary>
        /// Populate special properties for all Auditable Entities in context.
        /// </summary>
        public static void UpdateAuditableEntities(this DbContext context, int editorUserId)
        {
            DateTime utcNow = DateTime.Now;

            var changedEntries = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added
                         || e.State == EntityState.Modified
                         || e.State == EntityState.Deleted);

            foreach (var dbEntry in changedEntries)
            {
                UpdateAuditableEntity(dbEntry, utcNow, editorUserId);
            }
        }

        private static void UpdateAuditableEntity<TUserId>(
            EntityEntry dbEntry, DateTime utcNow, TUserId editorUserId)
            where TUserId : struct
        {
            object entity = dbEntry.Entity;

            switch (dbEntry.State)
            {
                case EntityState.Added:
                    if (entity is ICreationAuditable<TUserId> creationAuditable)
                    {
                        UpdateTrackableEntity(dbEntry, utcNow);
                        creationAuditable.CreatedBy = editorUserId;
                    }
                    dbEntry.CurrentValues[nameof(IDeletionAuditable<TUserId>.IsDeleted)] = false;
                    break;

                case EntityState.Modified:
                    if (entity is IModificationAuditable<TUserId> modificationAuditable)
                    {
                        UpdateTrackableEntity(dbEntry, utcNow);
                        modificationAuditable.UpdatedBy = editorUserId;
                        dbEntry.CurrentValues[nameof(IModificationAuditable<TUserId>.UpdatedBy)] = editorUserId;

                        if (entity is ICreationAuditable<TUserId>)
                        {
                            PreventPropertyOverwrite<TUserId>(dbEntry, nameof(ICreationAuditable<TUserId>.CreatedBy));
                            PreventPropertyOverwrite<TUserId>(dbEntry, nameof(ICreationAuditable<TUserId>.CreatedBy));
                        }
                    }
                    break;

                case EntityState.Deleted:
                    if (entity is IDeletionAuditable<TUserId> deletionAuditable)
                    {
                        UpdateTrackableEntity(dbEntry, utcNow);
                        // change CurrentValues after dbEntry.State becomes EntityState.Unchanged
                        deletionAuditable.DeletedBy = editorUserId;
                        dbEntry.CurrentValues[nameof(IDeletionAuditable<TUserId>.DeletedBy)] = editorUserId;
                    }
                    if (entity is ICreationAuditable<TUserId>)
                    {
                        PreventPropertyOverwrite<TUserId>(dbEntry, nameof(ICreationAuditable<TUserId>.CreatedBy));
                        PreventPropertyOverwrite<TUserId>(dbEntry, nameof(ICreationAuditable<TUserId>.CreatedBy));
                    }
                    if (entity is IModificationAuditable<TUserId>)
                    {
                        PreventPropertyOverwrite<TUserId>(dbEntry, nameof(IModificationAuditable<TUserId>.UpdatedDate));
                        PreventPropertyOverwrite<TUserId>(dbEntry, nameof(IModificationAuditable<TUserId>.UpdatedBy));
                    }
                    break;

                default:
                    throw new NotSupportedException();
            }
        }

        private static void UpdateAuditableEntity(
            EntityEntry dbEntry, DateTime utcNow, int editorUserId)
        {
            object entity = dbEntry.Entity;

            switch (dbEntry.State)
            {
                case EntityState.Added:
                    if (entity is ICreationAuditable creationAuditable)
                    {
                        UpdateTrackableEntity(dbEntry, utcNow);
                        creationAuditable.CreatedBy = editorUserId;
                        creationAuditable.IsDeleted = false;
                    }
                    break;

                case EntityState.Modified:
                    if (entity is IModificationAuditable modificationAuditable)
                    {
                        UpdateTrackableEntity(dbEntry, utcNow);
                        modificationAuditable.UpdatedBy = editorUserId;
                        dbEntry.CurrentValues[nameof(IModificationAuditable.UpdatedBy)] = editorUserId;
                    }

                    break;

                case EntityState.Deleted:
                    if (entity is IDeletionAuditable deletionAuditable)
                    {
                        UpdateTrackableEntity(dbEntry, utcNow);
                        // change CurrentValues after dbEntry.State becomes EntityState.Unchanged
                        deletionAuditable.DeletedBy = editorUserId;
                        dbEntry.CurrentValues[nameof(IDeletionAuditable.DeletedBy)] = editorUserId;
                    }
                    break;

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
