namespace rapid.erp.Core.Dictionary
{
    /// <summary>
    /// Database dictionary
    /// </summary>
    public static class DbDictionary
    {

        public static readonly string DatabaseName = "RapidErp";

        public static class StoredProcedure
        {
            public static class ParameterName
            {
                public static readonly string ApplicationId = "@ApplicationId";

                public static readonly string TableName = "@TableName";
                public static readonly string ColumnName = "@ColumnName";
                public static readonly string PrimaryKey = "@PrimaryKey";
                public static readonly string ForeignKey = "@ForeignKey";
                public static readonly string PrimaryKeyValue = "@PrimaryKeyValue";
                public static readonly string CreatedBy = "@CreatedBy";
                public static readonly string DoDelete = "@DoDelete";

                public static readonly string UserId = "@UserId";
                public static readonly string RoleId = "@RoleId";
                public static readonly string ComponentGroupId = "@ComponentGroupId";
                public static readonly string SearchText = "@SearchText";
                public static readonly string Page = "@Page";
                public static readonly string Rows = "@Rows";
                public static readonly string SortDirection = "@SortDirection";
                public static readonly string SortOrderColumn = "@SortOrderColumn";

            }
            public static class ProcedureName
            {                                                                                      
                public static class Common
                {
                    /// <summary>
                    /// SpDataVarificationBeforeDelete @TableName varchar(400), @PrimaryKey varchar(400), @ForeignKey varchar(400), @PrimaryKeyValue varchar(400), @CreatedBy Date, @DoDelete bit
                    /// </summary>
                    public static readonly string SpDataVarificationAndDelete = "exec SpDataVarificationAndDelete @TableName, @PrimaryKey, @ForeignKey, @PrimaryKeyValue, @CreatedBy, @DoDelete";
                }

                public static class Security
                {
                    /// <summary>
                    /// SpRoleAndPermissionView @RoleId, @ComponentGroupId, @SearchText, @Page, @Rows
                    /// </summary>
                    public static readonly string SpRoleAndPermissionView = "exec sec.SpRoleAndPermissionView @RoleId, @ComponentGroupId, @SearchText, @SortOrderColumn, @SortDirection";

                    /// <summary>
                    /// SpRoleAndPermissionView @RoleId, @ComponentGroupId, @SearchText, @Page, @Rows
                    /// </summary>
                    public static readonly string SpAspNetUserAndRoleView = "exec sec.SpAspNetUserAndRoleView @UserId, @RoleId, @SearchText, @SortOrderColumn, @SortDirection";

                    /// <summary>
                    /// SpRoleAndPermissionView @RoleId, @ComponentGroupId, @SearchText, @Page, @Rows
                    /// </summary>
                    public static readonly string SpUserPermissionView = "exec sec.SpUserPermissionView @UserId, @RoleId, @ComponentGroupId, @SearchText, @SortOrderColumn, @SortDirection";
                }

            }
        }
    }
}
