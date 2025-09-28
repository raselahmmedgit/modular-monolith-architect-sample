using System.Collections.Generic;

namespace rapid.erp.Core.FlashMessage
{
    public interface IFlashMessage
    {

        /// <summary>
        /// Queues a flash message for display with the specified message.
        /// </summary>
        /// <param name="flashMessage"></param>
        void Queue(FlashMessageModel flashMessage);

        /// <summary>
        /// Retrieves the queued flash messages for display and clears them.
        /// </summary>
        /// <returns></returns>
        List<FlashMessageModel> Retrieve();
        List<FlashMessageModel> Retrieve(string keyName);

        /// <summary>
        /// Retrieves the queued messages for the current response without clearing them.
        /// </summary>
        /// <returns></returns>
        List<FlashMessageModel> Peek();
    }
}
