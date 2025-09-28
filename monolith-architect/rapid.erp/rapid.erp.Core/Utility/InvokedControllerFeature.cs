using Microsoft.AspNetCore.Mvc;

namespace rapid.erp.Core.Utility
{
    /// <summary>
    /// Get Invoked (Current) Controller from view.
    /// </summary>
    public class InvokedControllerFeature : IInvokedControllerFeature
    {
        /// <summary>
        /// Get Invoked (Current) Controller.
        /// </summary>
        /// <param name="controller"> A base class for an MVC controller with view support.</param>
        public InvokedControllerFeature(Controller controller)
        {
            Controller = controller;
        }
        /// <summary>
        ///  A base class for an MVC controller with view support.
        /// </summary>
        public Controller Controller { get; }
    }
}
