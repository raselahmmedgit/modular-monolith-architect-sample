namespace rapid.erp.ViewModel.Config
{
    public class AppConfig
    {
        public static string Name = "AppConfig";
        public string? AppLogoWidth { get; set; }
        public string? AppPrefix { get; set; }
        public string? AppCopyrightText { get; set; }
        public string? AppWebsite { get; set; }

        public bool IsMSSqlDatabase { get; set; }
        public bool IsMySqlDatabase { get; set; }
        public bool IsDatabaseCreated { get; set; }
        public bool IsMasterDataInserted { get; set; }
    }
}
