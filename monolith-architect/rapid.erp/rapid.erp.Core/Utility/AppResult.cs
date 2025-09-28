using Microsoft.AspNetCore.Mvc;
using rapid.erp.Core.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace rapid.erp.Core.Utility
{
    public class AppResult
    {
        public bool Success { get; }

        public string Message { get; }

        public string MessageType { get; }

        public int ParentId { get; }

        public string ParentName { get; }

        public bool HasRecord { get; }

        public object Data { get; }

        public int ResultCount { get; }
        
        public string ResultId { get; }

        public string RedirectUrl { get; set;}

        public string UploadPath { get; }

        public AppResult()
        {
        }

        private AppResult(bool success, string message, string messageType)
        {
            Success = success;
            Message = message;
            MessageType = messageType;
        }

        private AppResult(bool success, string message, string messageType, string uploadPath)
        {
            Success = success;
            Message = message;
            MessageType = messageType;
            UploadPath = uploadPath;
        }

        private AppResult(bool success, string message, string messageType, int parentId, string parentName)
        {
            Success = success;
            Message = message;
            MessageType = messageType;
            ParentId = parentId;
            ParentName = parentName;
        }

        private AppResult(bool success, string message, string messageType, int parentId, string parentName, object data)
        {
            Success = success;
            Message = message;
            MessageType = messageType;
            ParentId = parentId;
            ParentName = parentName;
            Data = data;
        }

        public static AppResult Info()
        {
            return new AppResult(false, MessageHelper.Info, MessageHelper.MessageTypeInfo);
        }

        public static AppResult Info(string message)
        {
            return new AppResult(false, message, MessageHelper.MessageTypeInfo);
        }

        public static AppResult Warning()
        {
            return new AppResult(false, MessageHelper.Warning, MessageHelper.MessageTypeWarning);
        }

        public static AppResult Warning(string message)
        {
            return new AppResult(false, message, MessageHelper.MessageTypeWarning);
        }

        public static AppResult Fail()
        {
            return new AppResult(false, MessageHelper.Error, MessageHelper.MessageTypeDanger);
        }

        public static AppResult Fail(string message)
        {
            return new AppResult(false, message, MessageHelper.MessageTypeDanger);
        }

        public static AppResult Ok()
        {
            return new AppResult(true, MessageHelper.Success, MessageHelper.MessageTypeSuccess);
        }

        public static AppResult Ok(string message)
        {
            return new AppResult(true, message, MessageHelper.MessageTypeSuccess);
        }

        public static AppResult Ok(string message, int parentId, string parentName)
        {
            return new AppResult(true, message, MessageHelper.MessageTypeSuccess, parentId, parentName);
        }

        public static AppResult Ok(string message, string uploadPath)
        {
            return new AppResult(true, message, MessageHelper.MessageTypeSuccess, uploadPath);
        }

        public static AppResult Ok(string message, int parentId, string parentName, object data)
        {
            return new AppResult(true, message, MessageHelper.MessageTypeSuccess, parentId, parentName, data);
        }
    }
}
