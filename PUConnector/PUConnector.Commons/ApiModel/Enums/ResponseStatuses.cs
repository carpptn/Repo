namespace PUConnector.Commons.ApiModel.Enums
{
    /// <summary>
    /// Status odpowiedzi
    /// </summary>
    public enum ResponseStatuses
    {
        /// <summary>
        /// Numer extOrderId nie jest unikalny
        /// </summary>
        ERROR_ORDER_NOT_UNIQUE,

        /// <summary>
        /// Żądanie zostało wykonane poprawnie
        /// </summary>
        SUCCESS,

        /// <summary>
        /// Wymagana autoryzacja 3DS. System sprzedawcy musi wykonać przekierowanie w celu kontynuacji procesu płatności (można skorzystać z metody OpenPayU.authorize3DS())
        /// </summary>
        WARNING_CONTINUE_3DS,

        /// <summary>
        /// Wymagana autoryzacja CVV/CVC. Wywołaj metodę OpenPayU.authorizeCVV()
        /// </summary>
        WARNING_CONTINUE_CVV,

        /// <summary>
        /// Błędna składnia żądania
        /// </summary>
        ERROR_SYNTAX,

        /// <summary>
        /// Jedna lub więcej wartości jest nieprawidłowa
        /// </summary>
        ERROR_VALUE_INVALID,

        /// <summary>
        /// Brakuje jednej lub więcej wartości
        /// </summary>
        ERROR_VALUE_MISSING,

        /// <summary>
        /// Błędna autentykacja. Należy sprawdzić parametry podpisu i prawidłowość wdrożenia algorytmu podpisu
        /// </summary>
        UNAUTHORIZED,

        /// <summary>
        /// Brak uprawnień do wykonania żądania
        /// </summary>
        UNAUTHORIZED_REQUEST,

        /// <summary>
        /// W systemie PayU brak danych, które wskazano w żądaniu
        /// </summary>
        DATA_NOT_FOUND,

        /// <summary>
        /// Upłynął okres ważności dla realizacji żądania
        /// </summary>
        TIMEOUT,

        /// <summary>
        /// System PayU jest niedostępny. Spróbuj ponownie później
        /// </summary>
        BUSINESS_ERROR,

        /// <summary>
        /// System PayU jest niedostępny. Spróbuj ponownie później
        /// </summary>
        ERROR_INTERNAL,

        /// <summary>
        /// Wystąpił niespodziewany błąd. Spróbuj ponownie później
        /// </summary>
        GENERAL_ERROR,

        /// <summary>
        /// Wystąpił drobny niespodziewany błąd. Spróbuj ponownie później
        /// </summary>
        WARNING,

        /// <summary>
        /// System PayU jest niedostępny. Spróbuj ponownie później
        /// </summary>
        SERVICE_NOT_AVAILABLE,
    }
}