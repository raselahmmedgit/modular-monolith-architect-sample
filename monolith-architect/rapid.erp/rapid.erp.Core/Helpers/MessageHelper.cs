namespace rapid.erp.Core.Helpers
{
    public static class MessageHelper
    {
        public static string MessageTypeInfo = "info";
        public static string MessageTypeWarning = "warning";
        public static string MessageTypeSuccess = "success";
        public static string MessageTypeDanger = "danger";

        public static string Save = "Saved successfully.";
        public static string Update = "Updated successfully.";
        public static string Delete = "Deleted successfully.";
        public static string Add = "Added successfully.";
        public static string Edit = "Edited successfully.";
        public static string Remove = "Removed successfully.";
                
        public static string SaveFail = "Save failed.";
        public static string UpdateFail = "Update failed.";
        public static string DeleteFail = "Delete failed.";
        public static string AddFail = "Add failed.";
        public static string EditFail = "Edit failed.";
        public static string RemoveFail = "Remove failed.";

        public static string FilesUploaded = "'{0}' Files uploaded successfully.";
        public static string FilesUploadedFail = "Filed upload faild.";
        public static string FilesNoSelected = "No files selected.";
        public static string FilesAlreadyExists = "File already exists.";
        public static string NameAlreadyExists = "Name already taken. Please choose another name.";
        public static string UrlAlreadyExists = "Url already taken. Please choose another url.";
        public static string AlreadyExists = "Already exists.";
        public static string AlreadyAdded = "Already added.";
        public static string Fail = "Failed.";
        public static string Success = "Successfully completed.";
        public static string Info = "Please contact your system admin.";
        public static string Warning = "Please contact your system admin.";
        public static string Error = "We are facing some problem while processing the current request. Please try again later.";
        public static string UnhandledError = "We are facing some problem while processing the current request. Please try again later.";
        public static string UnAuthenticated = "You are not authenticated user.";
        public static string Authenticated = "You are authenticated user.";
        public static string NullError = "Requested object could not be found.";
        public static string NullReferenceExceptionError = "There are one or more required fields are missing.";

        public static string IsEmailExists = "Email '{0}' is already taken. Please choose another email.";
        public static string IsEmailNotExists = "Email '{0}' is available.";

        public static string SentMessage = "Message sent successfully.";
        public static string SentMessageFail = "Message couldn't be sent successfully.";
        public static string CaptchaSecurityCode = "Please enter the correct security code.";

        public static string DataFound = "Data found.";
        public static string DataNotFound = "Data not found.";
        public static string CreateDbAndInsertMasterData = "Created database and inserted master data.";
        public static string CreateDbAndInsertMasterDataFail = "Couldn't created database and inserted master data.";
        public static string AlreadyDbExists = "Database already exists.";

        public static string InvalidDateTime = "Invalid date {0} ";
        public static string InvalidBeginEndDateTime = "The {0} shouldn't be greater than {1}.";
        public static string InvalidEndDateTime = "The {0} should be provided.";
        public static string NotExistDateTime = "The {0} should be provided.";
        public static string InvalidQueryParams = "Invalid Query Paramters.";

        public static string FileImported = "File uploaded successfully.";
        public static string FileParseFail = "Problem parsing file.";
        public static string FilesImportedFail = "File upload failed.";
        public static string DuplicateCustomField = "Couldn't be added duplicate custom field.";

        public static string DuplicateInsertion = "Data row is not exists.";
        public static string DuplicateInsertionFail = "Insert failed. Data row already exists.";

        public static string InternalServerError = "Internal Server error.";
        public static string LoginUserDeleteFail = "Delete failed. You couldn't deleted login user.";
        public static string AdminRoleUserDeleteFail = "Delete failed. You couldn't deleted admin role user.";

        public static string UserExists = "Email is already taken. Please choose another email.";
        public static string UserNotExists = "Email is available.";

        public static string SendForSubmit = "Send For Submit successfully.";
        public static string SendForSubmitFail = "Send For Submit failed. Please Complete At First.";

        public static string SendForRevised = "Send For Revised successfully.";
        public static string SendForRevisedFail = "Send For Revised failed. Please Complete At First.";

        public static string SendForApproval = "Send For Approval successfully.";
        public static string SendForApprovalFail = "Send For Approval failed. Please Complete At First.";

        public static string Approval = "Approved successfully.";
        public static string ApprovalFail = "Approved failed. Please Complete At First.";

        public static string UserInvalidUserPassword = "Invalid user or password.";
        public static string UserInactive = "User is not active.";
        public static string UserInvalidLogin = "Invalid login attempt.";
        public static string UserInvalidPasswordChange = "Invalid password change attempt.";
        public static string UserInvalidEmail = "Could not find your account.";
        public static string DateFormatInvalid = "Date format is not valid.";
        public static string DataInvalid = "Data is not valid.";

        public static string ExcelFileFormatInvalid = "Excel file format is not valid.";
        public static string EffectiveDateInvalid = "Effective Date is not valid.";
        public static string PasswordChanged = "Your password has been changed successfully.";

    }
}
