using System;
using System.Collections.Generic;
using System.Text;

namespace rapid.erp.Core.Helpers
{
    public static class SmsTemplateHelper
    {
        public static string GetSmsTemplate(string url)
        {
            string template = $"You have been massege. {url}";
            return template;
        }
    }
}
