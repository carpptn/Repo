namespace PUConnector.Commons.ApiModel.Enums
{
    /// <summary>
    /// Status zwrotu
    /// </summary>
    public enum RefundStatuses
    {
        /// <summary>
        /// Zwrot jest w trakcie przetwarzania
        /// </summary>
        PENDING,

        /// <summary>
        /// Zwrot został anulowany
        /// </summary>
        CANCELED,

        /// <summary>
        /// Zwrot został wykonany
        /// </summary>
        FINALIZED,
    }
}