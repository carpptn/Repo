using PUConnector.Commons.ApiModel;
using System.ServiceModel;

namespace PUConnector.Api
{
    /// <summary>
    /// Definiuje kontrakt WCF dla funkcjonalności klasy <see cref="Connector"/>.
    /// Umożliwia uruchomienie instancji klasy <see cref="Connector"/> w postaci usługi WebService (soap) w PUConnector.SoapProxy.Service
    /// </summary>
    [ServiceContract]
    public interface IConnector
    {
        /// <summary>
        /// Wysyła żądanie utworzenia nowego zamówienia/transakcji
        /// </summary>
        /// <param name="request">Żądanie utworzenia zamówienia/transakcji</param>
        /// <returns>Odpowiedź na żądanie utworzenia zamówienia/transakcji</returns>
        [OperationContract]
        OrderCreateResponse OrderCreate(OrderCreateRequest request);

        /// <summary>
        /// Wysyła żądanie anulowania zamówienia/transakcji
        /// </summary>
        /// <param name="request">Żadanie anulowania zamówienia/transakcji</param>
        /// <returns>Odpowiedź na żądanie anulowania zamówienia/transakcji</returns>
        [OperationContract]
        OrderCancelResponse OrderCancel(OrderCancelRequest request);

        /// <summary>
        /// Wysyła żądanie pobrania szczegółów zamówienia/transakcji
        /// </summary>
        /// <param name="request">Żądanie pobrania szczegółów zamówienia/transakcji</param>
        /// <returns>Odpowiedź na żądanie pobrania szczegółów zamówienia/transakcji</returns>
        [OperationContract]
        OrderRetrieveResponse OrderRetrieve(OrderRetrieveRequest request);

        /// <summary>
        /// Wysyła żądanie aktualizacji statusu zamówienia/transakcji
        /// </summary>
        /// <param name="request">Żądanie aktualizacji statusu zamówienia/transakcji</param>
        /// <returns>Odpowiedź na żądanie aktualizacji stutusu zamówienia/transakcji</returns>
        [OperationContract]
        OrderStatusUpdateResponse OrderStatusUpdate(OrderStatusUpdateRequest request);

        /// <summary>
        /// Wysyła żądanie utworzenia nowego zwrotu dla zamównia/transakcji
        /// </summary>
        /// <param name="request">Żądanie utworzenia zwrotu dla zamówienia/transakcji</param>
        /// <returns>Odpowiedź na żądanie utworzenia zwrotu dla zamówienia/transakcji</returns>
        [OperationContract]
        RefundCreateResponse RefundCreate(RefundCreateRequest request);
    }
}
