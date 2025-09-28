using Microsoft.AspNetCore.Mvc;

namespace rapid.erp.Core.Utility
{
    public interface IInvokedControllerFeature
    {
        /// <summary>
        ///  A base class for an MVC controller with view support.
        /// </summary>
        Controller Controller { get; }
    }
}
