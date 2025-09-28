namespace rapid.erp.Common
{
    public class StoredProcedureResult
    {
        public bool Success { get; }

        public bool HasRecord { get; }

        public string Message { get; }

        public int? ResultCount { get; }
        
        public string ResultId { get; }

        public string RedirectUrl { get; }
    }
}
