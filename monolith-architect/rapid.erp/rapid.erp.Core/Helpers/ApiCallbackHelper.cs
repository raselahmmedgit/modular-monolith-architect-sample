namespace rapid.erp.Core.Helpers
{
    public class ApiCallbackHelper
    {
        public static async Task<string> FetchJsonDataAsync(string url)
        {
            string outputJson;
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                outputJson = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return outputJson;

        }
    }
}
