using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace rapid.erp.ViewModel.Database
{
    public static class AppDbEnums
    {
        public enum ApplicationRoleEnum
        {
            [Display(Name = "Admin")]
            [Description("Admin")]
            Admin = 1,
            [Display(Name = "User")]
            [Description("User")]
            User = 2
        }

        public enum ApplicationUserEnum
        {
            [Display(Name = "SuperAdmin")]
            [Description("f8d45ddf-cc95-4c6d-9de5-3849ccff5777")]
            SuperAdmin = 1,
            [Display(Name = "Admin")]
            [Description("fb3d9803-5bb7-4a48-bbd2-f63038739365")]
            Admin = 2,
            [Display(Name = "User")]
            [Description("3ce11982-3ff7-46a9-a450-038eb42a3849")]
            User = 3
        }

        public enum ExportTypeEnum
        {
            [Display(Name = "pdf")]
            Pdf = 1,
            [Display(Name = "excel")]
            Excel = 2
        }

        public enum MonthListEnum
        {
            [Display(Name = "January")]
            January = 1,
            [Display(Name = "February")]
            February = 2,
            [Display(Name = "March")]
            March = 3,
            [Display(Name = "April")]
            April = 4,
            [Display(Name = "May")]
            May = 5,
            [Display(Name = "June")]
            June = 6,
            [Display(Name = "July")] 
            July = 7,
            [Display(Name = "August")]
            August = 8,
            [Display(Name = "September")]
            September = 9,
            [Display(Name = "October")]
            October = 10,
            [Display(Name = "November")]
            November = 11,
            [Display(Name = "December")]
            December = 12

        }

        public enum NotificationTypeEnum
        {
            [Display(Name = "Sms")]
            Sms = 1,
            [Display(Name = "Email")]
            Email = 3,
            [Display(Name = "App")]
            App = 4
        }

        public enum NotificationSentStatusEnum
        {
            [Display(Name = "Pending")]
            Pending = 1,
            [Display(Name = "Sent")]
            Sent = 2,
            [Display(Name = "Canceled")]
            Canceled = 3,
            [Display(Name = "Failed")]
            Failed = 4,
            [Display(Name = "Waiting")]
            Waiting = 5
        }

        public enum FileExtensionEnum
        {
            [Display(Name = ".jpg")]
            JPG,
            [Display(Name = ".png")]
            PNG,
            [Display(Name = ".pdf")]
            PDF,
            [Display(Name = ".docx")]
            DOCX,
            [Display(Name = ".xlsx")]
            XLSX,
            [Display(Name = ".jpeg")]
            JPEG,
            [Display(Name = ".ppt")]
            PPT,
            [Display(Name = ".pptx")]
            PPTX,
            [Display(Name = ".jfif")]
            JFIF,
            
        }

    }
}
