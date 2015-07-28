namespace PUConnector.Commons.ApiModel.Enums
{
    /// <summary>
    /// Status zamówienia
    /// </summary>
    public enum OrderStatuses
    {
        /// <summary>
        /// Nowa płatność została utworzona
        /// </summary>
        NEW,

        /// <summary>
        /// Płatność jest w trakcie rozliczenia
        /// </summary>
        PENDING,

        /// <summary>
        /// System PayU oczekuje na akcje ze strony sprzedawcy w celu wykonania płatności. Ten status występuje w przypadku gdy auto-odbiór na posie sprzedawcy jest wyłączony
        /// </summary>
        WAITING_FOR_CONFIRMATION,

        /// <summary>
        /// Płatność została zaakceptowana w całości. Środki są dostępne do wypłaty
        /// </summary>
        COMPLETED,

        /// <summary>
        /// Płatność została anulowana
        /// </summary>
        CANCELED,

        /// <summary>
        /// Płatność została odrzucona z uwagi na życzenie sprzedawcy
        /// </summary>
        REJECTED
    }
}