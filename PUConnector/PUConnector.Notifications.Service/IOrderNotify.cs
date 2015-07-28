using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace PUConnector.Notifications.Service
{
    /// <summary>
    /// Kontrakt WCF opisujący usługę Web typu Rest odbierającą powiadomienia (notyfikacje) PayU
    /// </summary>
    [ServiceContract]
    public interface IOrderNotify
    {
        /// <summary>
        /// Odbiera powiadomienia (notyfikacje) o zmianie stanu zamówienia/transakcji bądź zwrotu
        /// </summary>
        /// <param name="stream">Strumień z treścią powiadomienia</param>
        /// <returns>Pusty łańcuch tekstowy w przypadku poprawnego przetworzenia powiadomienia lub opis błędu</returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Receive", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string Receive(Stream stream);

        /// <summary>
        /// Zwraca napis "OK" - pozwala na szybki test dostępności usługi z poziomu przeglądarki internetowej
        /// </summary>
        /// <returns>"OK"</returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Test", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string Test();
    }
}
