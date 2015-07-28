using PUConnector.Commons.ApiModel;
using PUConnector.Db.Repository;
using System;
using System.ServiceModel;

namespace PUConnector.OrdersManager
{
    /// <summary>
    /// Definiuje kontrakt WCF dla funkcjonalności klasy <see cref="Manager"/>.
    /// Umożliwia uruchomienie instancji klasy <see cref="Manager"/> w postaci usługi WebService (soap) w PUConnector.SoapProxy.Service
    /// </summary>
    [ServiceContract]
    public interface IManager
    {
        /// <summary>
        /// Tworzy rekord w tabeli [PUCommLog]
        /// </summary>
        /// <param name="order">Obiekt zamówienia</param>
        /// <param name="requestType">Typ żądania</param>
        /// <param name="request">Treść żądania</param>
        /// <param name="responseType">Typ odpowiedzi</param>
        /// <param name="response">Treść odpowiedzi</param>
        /// <returns>Id utworzonego rekordu</returns>
        [OperationContract]
        long CommLogCreate(PUOrder order, string requestType, string request, string responseType, string response);

        /// <summary>
        /// Tworzy rekord w tabeli [PUCommLog] dla zamówienia o danym extOrderId
        /// </summary>
        /// <param name="extOrderId">Identyfikator zewnętrzny zamówienia (ExtOrderId)</param>
        /// <param name="requestType">Typ żądania</param>
        /// <param name="request">Treść żądania</param>
        /// <param name="responseType">Typ odpowiedzi</param>
        /// <param name="response">Treść odpowiedzi</param>
        /// <returns>Id utworzonego rekordu</returns>
        [OperationContract]
        long? CommLogCreateForExtOrderId(string extOrderId, string requestType, string request, string responseType, string response);

        /// <summary>
        /// Tworzy rekord w tabeli [PUCommLog] dla zamówienia o danym orderId
        /// </summary>
        /// <param name="orderId">Identyfikator wewnętrzny zamówienia (OrderId)</param>
        /// <param name="requestType">Typ żądania</param>
        /// <param name="request">Treść żądania</param>
        /// <param name="responseType">Typ odpowiedzi</param>
        /// <param name="response">Treść odpowiedzi</param>
        /// <returns>Id utworzonego rekordu</returns>
        [OperationContract]
        long? CommLogCreateForOrderId(string orderId, string requestType, string request, string responseType, string response);

        /// <summary>
        /// Aktualizuje rekord w tabeli [PUCommLog]
        /// </summary>
        /// <param name="commLogId">Id rekordu w tabeli [PUCommLog]</param>
        /// <param name="responseType">Typ odpowiedzi</param>
        /// <param name="response">Treść odpowiedzi</param>
        [OperationContract]
        void CommLogUpdate(long commLogId, string responseType, string response);

        /// <summary>
        /// Zwraca rekordy logów PUCommLog dla zamówienia o danym identyfikatorze zewnętrznym zamówienia (ExtOrderId)
        /// </summary>
        /// <param name="extOrderId">Zewnętrzny identyfikator zamówienia ExtOrderId</param>
        /// <returns>Kolekcja logów zamówienia</returns>
        [OperationContract]
        PUCommLog[] GetOrderCommLogs(string extOrderId);

        /// <summary>
        /// Zwraca rekordy zwrotów PURefund dla zamówienia o danym identyfikatorze zewnętrznym zamówienia (ExtOrderId)
        /// </summary>
        /// <param name="extOrderId">Zewnętrzny identyfikator zamówienia ExtOrderId</param>
        /// <returns>Kolekcja zwrotów zamówienia</returns>
        [OperationContract]
        PURefund[] GetOrderRefunds(string extOrderId);

        /// <summary>
        /// Zwraca rekord zamówienia na podstawie zewnętrznego identyfikatora zamówienia (ExtOrderId)
        /// </summary>
        /// <param name="extOrderId">Zewnętrzny identyfikator zamówienia ExtOrderId</param>
        /// <returns>Rekord zamówienia</returns>
        [OperationContract]
        PUOrder OrderByExtOrderId(string extOrderId);

        /// <summary>
        /// Zwraca rekord zamówienia na podstawie wewnętrznego identyfikatora zamówienia (OrderId)
        /// </summary>
        /// <param name="orderId">Wewnętrzny identyfikator zamówienia OrderId</param>
        /// <returns>Rekord zamówienia</returns>
        [OperationContract]
        PUOrder OrderByOrderId(string orderId);

        /// <summary>
        /// Zwraca rekord zamówienia na podstawie wewnętrznego i zewnętrznego identyfikatora zamówienia
        /// </summary>
        /// <param name="orderId">Wewnętrzny identyfikator zamówienia OrderId</param>
        /// <param name="extOrderId">Zewnętrzny identyfikator zamówienia ExtOrderId</param>
        /// <returns>Rekord zamówienia</returns>
        [OperationContract]
        PUOrder OrderByOrderIdAndExtOrderId(string orderId, string extOrderId);

        /// <summary>
        /// Zwraca aktualny obiekt opisujący zamówienie/transakcję o danym identyfikatorze
        /// </summary>
        /// <param name="orderId">Wewnętrzny identyfikator zamówienia OrderId</param>
        /// <returns>Obiekt zamówienia/transakcji</returns>
        [OperationContract]
        OrderRecord GetOrderObject(string orderId);

        /// <summary>
        /// Zwraca aktualny obiekt opisujący zwrot o danym identyfikatorze
        /// </summary>
        /// <param name="refundId">Wewnętrzny identyfikator zwrotu RefundId</param>
        /// <returns>Obiekt zwrotu</returns>
        [OperationContract]
        RefundRecord_Type GetRefundObject(string refundId);

        /// <summary>
        /// Zwraca dla danego zamówienia/transakcji obiekty wysłanych żądań i otrzymanych na nie odpowiedzi od PayU
        /// </summary>
        /// <param name="extOrderId">Zewnętrzny identyfikator zamówienia ExtOrderId</param>
        /// <returns>Obiekty wysłanych żądań i otrzymanych na nie odpowiedzi od PayU</returns>
        [OperationContract]
        Tuple<object, object>[] GetRequestsAndResponses(string extOrderId);

        /// <summary>
        /// Tworzy nowe zamówienie/transakcję
        /// </summary>
        /// <param name="extOrderId">Zewnętrzny identyfikator zamówienia ExtOrderId lub null</param>
        /// <returns>Zewnętrzny identyfikator zamówienia ExtOrderId</returns>
        [OperationContract]
        string OrderCreate(string extOrderId);

        /// <summary>
        /// Zwraca zewnętrzny identyfikator zamówienia (ExtOrderId) na podstawie wewnętrznego identyfikatora zamówienia (OrderId)
        /// </summary>
        /// <param name="orderId">Wewnętrzny identyfikator zamówienia OrderId</param>
        /// <returns>Zewnętrzny identyfikator zamówienia ExtOrderId</returns>
        [OperationContract]
        string ExtOrderIdByOrderId(string orderId);

        /// <summary>
        /// Zwraca zewnętrzny identyfikator zwrotu (ExtRefundId) na podstawie wewnętrznego identyfikatora zwrotu (RefundId)
        /// </summary>
        /// <param name="refundId">Wewnętrzny identyfikator zwrotu RefundId</param>
        /// <returns>Zewnętrzny identyfikator zwrotu ExtRefundId</returns>
        [OperationContract]
        string ExtRefundIdByRefundId(string refundId);

        /// <summary>
        /// Zwraca wewnętrzny identyfikator zamówienia (OrderId) na podstawie zewnętrznego identyfikatora zamówienia (ExtOrderId)
        /// </summary>
        /// <param name="extOrderId">Zewnętrzny identyfikator zamówienia ExtOrderId</param>
        /// <returns>Wewnętrzny identyfikator zamówienia OrderId</returns>
        [OperationContract]
        string OrderIdByExtOrderId(string extOrderId);

        /// <summary>
        /// Aktualizuje obiekt zamówienia/transakcji
        /// </summary>
        /// <param name="orderObject">Obiekt zamówienia/transakcji</param>
        /// <returns>Znacznik dokoniania aktualizacji</returns>
        [OperationContract]
        bool OrderUpdate(OrderRecord orderObject);

        /// <summary>
        /// Aktualizuje wewnętrzny identyfikator zamówienia (OrderId) dla zamówienia o danym zewnętrznym identyfikatorze (ExtOrderId)
        /// </summary>
        /// <param name="extOrderId">Zewnętrzny identyfikator zamówienia ExtOrderId</param>
        /// <param name="orderId">Nowy wewnętrzny identyfikator zamówienia OrderId</param>
        [OperationContract]
        void OrderUpdateOrderId(string extOrderId, string orderId);

        /// <summary>
        /// Deserializuje obiekt zamówienia zapisany w rekordzie zamówienia w tabeli [PUOrder]
        /// </summary>
        /// <param name="order">Rekord zamówienia w tabeli [PUOrder]</param>
        /// <returns>Obiekt zamówienia</returns>
        [OperationContract]
        OrderRecord ParseOrderObject(PUOrder order);

        /// <summary>
        /// Deserializuje obiekt zwrotu zapisany w rekordzie zwrotu w tabeli [PURefund]
        /// </summary>
        /// <param name="refund">Rekord zwrotu w tabeli [PURefund]</param>
        /// <returns>Obiekt zwrotu</returns>
        [OperationContract]
        RefundRecord_Type ParseRefundObject(PURefund refund);

        /// <summary>
        /// Parsuje obiekt wysłanego żądania i otrzymanej na nie odpowiedzi od PayU
        /// </summary>
        /// <param name="commLog">Rekord z tabeli [PUCommLog]</param>
        /// <returns>Obiekt wysłanego żądania i otrzymanej na nie odpowiedzi od PayU</returns>
        [OperationContract]
        Tuple<object, object> ParseRequestAndResponse(PUCommLog commLog);

        /// <summary>
        /// Rekord zwrotu z tabeli [PURefund] o danym zewnęntrznym identyfikatorze ExtRefundId
        /// </summary>
        /// <param name="extRefundId">Zewnętrzny identyfikator zwrotu ExtRefundId</param>
        /// <returns>Rekord zwrotu z tabeli [PURefund]</returns>
        [OperationContract]
        PURefund RefundByExtRefundId(string extRefundId);

        /// <summary>
        /// Rekord zwrotu z tabeli [PURefund] o danym wewnęntrznym identyfikatorze RefundId
        /// </summary>
        /// <param name="refundId">Wewnętrzny identyfikator zwrotu RefundId</param>
        /// <returns>Rekord zwrotu z tabeli [PURefund]</returns>
        [OperationContract]
        PURefund RefundByRefundId(string refundId);

        /// <summary>
        /// Rekord zwrotu z tabeli [PURefund] o danym wewnęntrznym i zewnętrznym identyfikatorze
        /// </summary>
        /// <param name="refundId">Wewnętrzny identyfikator zwrotu RefundId</param>
        /// <param name="extRefundId">Zewnętrzny identyfikator zwrotu ExtRefundId</param>
        /// <returns>Rekord zwrotu z tabeli [PURefund]</returns>
        [OperationContract]
        PURefund RefundByRefundIdAndExtRefundId(string refundId, string extRefundId);

        /// <summary>
        /// Tworzy nowy zwrot dla zamówienia
        /// </summary>
        /// <param name="orderId">Wewnętrzny identyfikator zamówienia/transakcji OrderId</param>
        /// <returns>Zewnętrzny identyfikator zwrotu ExtRefundId</returns>
        [OperationContract]
        string RefundCreate(string orderId);

        /// <summary>
        /// Zwraca wewnętrzny identyfikator zwrotu (RefundId) na podstawie zewnętrznego identyfikatora zwrotu (ExtRefundId)
        /// </summary>
        /// <param name="extRefundId">Zewnętrzny identyfikator zwrotu ExtRefundId</param>
        /// <returns>Wewnętrzny identyfikator zwrotu RefundId</returns>
        [OperationContract]
        string RefundIdByExtRefundId(string extRefundId);

        /// <summary>
        /// Aktualizuje obiekt zwrotu
        /// </summary>
        /// <param name="refundObject">Obiekt zwrotu</param>
        /// <returns>Znacznik dokoniania aktualizacji</returns>
        [OperationContract]
        bool RefundUpdate(RefundRecord_Type refundObject);

        /// <summary>
        /// Aktualizuje wewnętrzny identyfikator zwrotu (RefundId) dla zwrotu o danym zewnętrznym identyfikatorze (ExtRefundId)
        /// </summary>
        /// <param name="extRefundId">Zewnętrzny identyfikator zwrotu ExtRefundId</param>
        /// <param name="refundId">Wewnętrzny identyfikator zwrotu RefundId</param>
        [OperationContract]
        void RefundUpdateRefundId(string extRefundId, string refundId);

        /// <summary>
        /// Zwraca zamówienia/transakcje zapisane w tabli [PUOrder]
        /// </summary>
        /// <param name="limit">Limit zwracanych rekordów</param>
        /// <returns>Kolekcja zamówień/transakcji</returns>
        PUOrder[] OrdersGet(int? limit = null);

        /// <summary>
        /// Zwraca zamówienia/transakcje identyfikatorze PK w kolumnie [PUOrder].[Id] większym niż <paramref name="fromId"/>
        /// </summary>
        /// <param name="fromId">Wartość idetyfikatora PK w kolumnie [PUOrder].[Id]</param>
        /// <returns>Kolekcja zamówień/transakcji</returns>
        PUOrder[] NewOrdersGet(long fromId);
    }
}
