namespace rapid.erp.Common
{
    public partial class EmailSentResult
    {
        public string Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public System.Exception Ex { get; set; }
        public object DataItem { get; set; }
    }
}
