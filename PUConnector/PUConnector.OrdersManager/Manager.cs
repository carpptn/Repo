using Newtonsoft.Json;
using PetaPoco;
using PUConnector.Commons.ApiModel;
using PUConnector.Commons.ApiModel.Enums;
using PUConnector.Db.Repository;
using PUConnector.OrdersManager.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.ServiceModel;

namespace PUConnector.OrdersManager
{
    /// <summary>
    /// Warstwa dostępu do bazy danych
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class Manager : IManager
    {
        // variables

        private TraceSource ts = new TraceSource("PUConnector.Manager");


        // properties

        /// <summary>
        /// Łańcuch definiujący połączenie z bazą danych
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Nazwa providera dostępu do bazy danych
        /// </summary>
        public string ProviderName { get; set; }


        // constructors

        /// <summary>
        /// Domyślny konstruktor odczytujący parametry konfiguracyjne z pliku .config
        /// </summary>
        public Manager()
        {
            this.ts.TraceInformation("Created");
            this.ts.TraceInformation("Read configuration");

            if (ConfigurationManager.ConnectionStrings["OrdersManager"] != null &&
                !string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings["OrdersManager"].ConnectionString))
            {
                this.ConnectionString =
                    ConfigurationManager.ConnectionStrings["OrdersManager"].ConnectionString;

                this.ProviderName =
                    ConfigurationManager.ConnectionStrings["OrdersManager"].ProviderName;
            }
        }


        /// <summary>
        /// Sparametryzowany konstruktor nie wymagający pliku .config
        /// </summary>
        /// <param name="connectionString">Łańcuch definiujący połączenie z bazą danych</param>
        /// <param name="providerName">Nazwa providera dostępu do bazy danych</param>
        public Manager(string connectionString, string providerName)
        {
            this.ts.TraceInformation("Created");

            this.ConnectionString = connectionString;
            this.ProviderName = providerName;
        }


        // public methods

        /// <summary>
        /// Zwraca zamówienia/transakcje zapisane w tabli [PUOrder]
        /// </summary>
        /// <param name="limit">Limit zwracanych rekordów</param>
        /// <returns>Kolekcja zamówień/transakcji</returns>
        public PUOrder[] OrdersGet(int? limit = null)
        {
            using (Database db = new Database(this.ConnectionString, this.ProviderName))
            {
                if (limit != null)
                {
                    db.Fetch<PUOrder>(
                        string.Format("SELECT TOP {0} * FROM [PUOrder] ORDER BY [Id] DESC", (int)limit)
                    ).ToArray();
                }

                return
                    db.Fetch<PUOrder>(
                        "SELECT * FROM [PUOrder] ORDER BY [Id] DESC"
                    ).ToArray();
            }
        }

        /// <summary>
        /// Zwraca zamówienia/transakcje identyfikatorze PK w kolumnie [PUOrder].[Id] większym niż <paramref name="fromId"/>
        /// </summary>
        /// <param name="fromId">Wartość idetyfikatora PK w kolumnie [PUOrder].[Id]</param>
        /// <returns>Kolekcja zamówień/transakcji</returns>
        public PUOrder[] NewOrdersGet(long fromId)
        {
            using (Database db = new Database(this.ConnectionString, this.ProviderName))
            {
                return
                    db.Fetch<PUOrder>(
                        "SELECT * FROM [PUOrder] WHERE [Id] > @0 ORDER BY [Id] DESC", fromId
                    ).ToArray();
            }
        }


        /// <summary>
        /// Zwraca rekord zamówienia na podstawie wewnętrznego i zewnętrznego identyfikatora zamówienia
        /// </summary>
        /// <param name="orderId">Wewnętrzny identyfikator zamówienia OrderId</param>
        /// <param name="extOrderId">Zewnętrzny identyfikator zamówienia ExtOrderId</param>
        /// <returns>Rekord zamówienia</returns>
        public PUOrder OrderByOrderIdAndExtOrderId(string orderId, string extOrderId)
        {
            using (Database db = new Database(this.ConnectionString, this.ProviderName))
            {
                PUOrder order =
                    db.FirstOrDefault<PUOrder>(
                        "SELECT * FROM [PUOrder] WHERE [ExtOrderId]=@0 AND [OrderId]=@1",
                        extOrderId,
                        orderId
                    );

                if (order == null)
                    throw new OrderNotFoundException(
                        string.Format(
                            "Order not found! extOrderId={0}, orderId={1}",
                            extOrderId,
                            orderId)
                        );

                return order;
            }
        }


        /// <summary>
        /// Zwraca rekord zamówienia na podstawie wewnętrznego identyfikatora zamówienia (OrderId)
        /// </summary>
        /// <param name="orderId">Wewnętrzny identyfikator zamówienia OrderId</param>
        /// <returns>Rekord zamówienia</returns>
        public PUOrder OrderByOrderId(string orderId)
        {
            using (Database db = new Database(this.ConnectionString, this.ProviderName))
            {
                PUOrder order =
                    db.FirstOrDefault<PUOrder>("SELECT * FROM [PUOrder] WHERE [OrderId]=@0", orderId);
                if (order == null)
                    throw new OrderNotFoundException("Order not found! orderId=" + orderId);

                return order;
            }
        }


        /// <summary>
        /// Zwraca rekord zamówienia na podstawie zewnętrznego identyfikatora zamówienia (ExtOrderId)
        /// </summary>
        /// <param name="extOrderId">Zewnętrzny identyfikator zamówienia ExtOrderId</param>
        /// <returns>Rekord zamówienia</returns>
        public PUOrder OrderByExtOrderId(string extOrderId)
        {
            using (Database db = new Database(this.ConnectionString, this.ProviderName))
            {
                PUOrder order =
                    db.FirstOrDefault<PUOrder>("SELECT * FROM [PUOrder] WHERE [ExtOrderId]=@0", extOrderId);
                if (order == null)
                    throw new OrderNotFoundException("Order not found! extOrderId=" + extOrderId);

                return order;
            }
        }


        /// <summary>
        /// Rekord zwrotu z tabeli [PURefund] o danym wewnęntrznym i zewnętrznym identyfikatorze
        /// </summary>
        /// <param name="refundId">Wewnętrzny identyfikator zwrotu RefundId</param>
        /// <param name="extRefundId">Zewnętrzny identyfikator zwrotu ExtRefundId</param>
        /// <returns>Rekord zwrotu z tabeli [PURefund]</returns>
        public PURefund RefundByRefundIdAndExtRefundId(string refundId, string extRefundId)
        {
            using (Database db = new Database(this.ConnectionString, this.ProviderName))
            {
                PURefund refund =
                    db.FirstOrDefault<PURefund>(
                        "SELECT * FROM [PURefund] WHERE [ExtRefundId]=@0 AND [RefundId]=@1",
                        extRefundId,
                        refundId
                    );

                if (refund == null)
                    throw new RefundNotFoundException(
                        string.Format(
                            "Refund not found! extRefundId={0}, refundId={1}",
                            extRefundId,
                            refundId)
                        );

                return refund;
            }
        }


        /// <summary>
        /// Rekord zwrotu z tabeli [PURefund] o danym wewnęntrznym identyfikatorze RefundId
        /// </summary>
        /// <param name="refundId">Wewnętrzny identyfikator zwrotu RefundId</param>
        /// <returns>Rekord zwrotu z tabeli [PURefund]</returns>
        public PURefund RefundByRefundId(string refundId)
        {
            using (Database db = new Database(this.ConnectionString, this.ProviderName))
            {
                PURefund refund =
                    db.FirstOrDefault<PURefund>("SELECT * FROM [PURefund] WHERE [RefundId]=@0", refundId);
                if (refund == null)
                    throw new RefundNotFoundException("Refund not found! refundId=" + refundId);

                return refund;
            }
        }


        /// <summary>
        /// Rekord zwrotu z tabeli [PURefund] o danym zewnęntrznym identyfikatorze ExtRefundId
        /// </summary>
        /// <param name="extRefundId">Zewnętrzny identyfikator zwrotu ExtRefundId</param>
        /// <returns>Rekord zwrotu z tabeli [PURefund]</returns>
        public PURefund RefundByExtRefundId(string extRefundId)
        {
            using (Database db = new Database(this.ConnectionString, this.ProviderName))
            {
                PURefund refund =
                    db.FirstOrDefault<PURefund>("SELECT * FROM [PURefund] WHERE [ExtRefundId]=@0", extRefundId);
                if (refund == null)
                    throw new RefundNotFoundException("Refund not found! extRefundId=" + extRefundId);

                return refund;
            }
        }


        /// <summary>
        /// Zwraca rekordy zwrotów PURefund dla zamówienia o danym identyfikatorze zewnętrznym zamówienia (ExtOrderId)
        /// </summary>
        /// <param name="extOrderId">Zewnętrzny identyfikator zamówienia ExtOrderId</param>
        /// <returns>Kolekcja zwrotów zamówienia</returns>
        public PURefund[] GetOrderRefunds(string extOrderId)
        {
            PUOrder order = this.OrderByExtOrderId(extOrderId);

            using (Database db = new Database(this.ConnectionString, this.ProviderName))
            {
                return 
                    db.Fetch<PURefund>("SELECT * FROM [PURefund] WHERE [PUOrderId]=@0", order.Id).ToArray();
            }
        }


        /// <summary>
        /// Zwraca rekordy logów PUCommLog dla zamówienia o danym identyfikatorze zewnętrznym zamówienia (ExtOrderId)
        /// </summary>
        /// <param name="extOrderId">Zewnętrzny identyfikator zamówienia ExtOrderId</param>
        /// <returns>Kolekcja logów zamówienia</returns>
        public PUCommLog[] GetOrderCommLogs(string extOrderId)
        {
            PUOrder order = this.OrderByExtOrderId(extOrderId);

            using (Database db = new Database(this.ConnectionString, this.ProviderName))
            {
                return
                    db.Fetch<PUCommLog>(
                        "SELECT * FROM [PUCommLog] WHERE [PUOrderId]=@0 ORDER BY [ID]",
                        order.Id
                        ).ToArray();
            }
        }


        /// <summary>
        /// Deserializuje obiekt zwrotu zapisany w rekordzie zwrotu w tabeli [PURefund]
        /// </summary>
        /// <param name="refund">Rekord zwrotu w tabeli [PURefund]</param>
        /// <returns>Obiekt zwrotu</returns>
        public RefundRecord_Type ParseRefundObject(PURefund refund)
        {
            return JsonConvert.DeserializeObject<RefundRecord_Type>(refund.RefundObject);
        }


        /// <summary>
        /// Deserializuje obiekt zamówienia zapisany w rekordzie zamówienia w tabeli [PUOrder]
        /// </summary>
        /// <param name="order">Rekord zamówienia w tabeli [PUOrder]</param>
        /// <returns>Obiekt zamówienia</returns>
        public OrderRecord ParseOrderObject(PUOrder order)
        {
            return JsonConvert.DeserializeObject<OrderRecord>(order.OrderObject);
        }


        /// <summary>
        /// Parsuje obiekt wysłanego żądania i otrzymanej na nie odpowiedzi od PayU
        /// </summary>
        /// <param name="commLog">Rekord z tabeli [PUCommLog]</param>
        /// <returns>Obiekt wysłanego żądania i otrzymanej na nie odpowiedzi od PayU</returns>
        public Tuple<object, object> ParseRequestAndResponse(PUCommLog commLog)
        {
            object request = null;
            if (!string.IsNullOrEmpty(commLog.RequestType))
                request =
                    JsonConvert.DeserializeObject(
                    commLog.RequestContent, Type.GetType(
                        "PUConnector.Commons.ApiModel." +
                        commLog.RequestType + 
                        ", PUConnector.Commons"
                        )
                    );

            object response = null;
            if (!string.IsNullOrEmpty(commLog.ResponseType))
                response =
                    JsonConvert.DeserializeObject(
                    commLog.ResponseContent, Type.GetType(
                        "PUConnector.Commons.ApiModel." +
                        commLog.ResponseType + 
                        ", PUConnector.Commons"
                        )

                    );

            return new Tuple<object, object>(request, response);
        }


        /// <summary>
        /// Zwraca zewnętrzny identyfikator zamówienia (ExtOrderId) na podstawie wewnętrznego identyfikatora zamówienia (OrderId)
        /// </summary>
        /// <param name="orderId">Wewnętrzny identyfikator zamówienia OrderId</param>
        /// <returns>Zewnętrzny identyfikator zamówienia ExtOrderId</returns>
        public string ExtOrderIdByOrderId(string orderId)
        {
            return this.OrderByOrderId(orderId).ExtOrderId;
        }


        /// <summary>
        /// Zwraca wewnętrzny identyfikator zamówienia (OrderId) na podstawie zewnętrznego identyfikatora zamówienia (ExtOrderId)
        /// </summary>
        /// <param name="extOrderId">Zewnętrzny identyfikator zamówienia ExtOrderId</param>
        /// <returns>Wewnętrzny identyfikator zamówienia OrderId</returns>
        public string OrderIdByExtOrderId(string extOrderId)
        {
            return this.OrderByExtOrderId(extOrderId).OrderId;
        }


        /// <summary>
        /// Zwraca zewnętrzny identyfikator zwrotu (ExtRefundId) na podstawie wewnętrznego identyfikatora zwrotu (RefundId)
        /// </summary>
        /// <param name="refundId">Wewnętrzny identyfikator zwrotu RefundId</param>
        /// <returns>Zewnętrzny identyfikator zwrotu ExtRefundId</returns>
        public string ExtRefundIdByRefundId(string refundId)
        {
            return this.RefundByRefundId(refundId).ExtRefundId;
        }


        /// <summary>
        /// Zwraca wewnętrzny identyfikator zwrotu (RefundId) na podstawie zewnętrznego identyfikatora zwrotu (ExtRefundId)
        /// </summary>
        /// <param name="extRefundId">Zewnętrzny identyfikator zwrotu ExtRefundId</param>
        /// <returns>Wewnętrzny identyfikator zwrotu RefundId</returns>
        public string RefundIdByExtRefundId(string extRefundId)
        {
            return this.RefundByExtRefundId(extRefundId).RefundId;
        }


        /// <summary>
        /// Tworzy nowe zamówienie/transakcję
        /// </summary>
        /// <param name="extOrderId">Zewnętrzny identyfikator zamówienia ExtOrderId lub null</param>
        /// <returns>Zewnętrzny identyfikator zamówienia ExtOrderId</returns>
        public string OrderCreate(string extOrderId)
        {
            using (Database db = new Database(this.ConnectionString, this.ProviderName))
            {
                PUOrder order = new PUOrder();
                order.Created = DateTime.Now;
                order.ExtOrderId = (extOrderId == null) ? Guid.NewGuid().ToString() : extOrderId;
                db.Insert(order);
                return order.ExtOrderId;
            }
        }


        /// <summary>
        /// Aktualizuje wewnętrzny identyfikator zamówienia (OrderId) dla zamówienia o danym zewnętrznym identyfikatorze (ExtOrderId)
        /// </summary>
        /// <param name="extOrderId">Zewnętrzny identyfikator zamówienia ExtOrderId</param>
        /// <param name="orderId">Nowy wewnętrzny identyfikator zamówienia OrderId</param>
        public void OrderUpdateOrderId(string extOrderId, string orderId)
        {
            using (Database db = new Database(this.ConnectionString, this.ProviderName))
            {
                PUOrder order = this.OrderByExtOrderId(extOrderId);
                order.OrderId = orderId;
                order.Updated = DateTime.Now;
                db.Update(order);
            }
        }


        /// <summary>
        /// Aktualizuje obiekt zamówienia/transakcji
        /// </summary>
        /// <param name="orderObject">Obiekt zamówienia/transakcji</param>
        /// <returns>Znacznik dokoniania aktualizacji</returns>
        public bool OrderUpdate(OrderRecord orderObject)
        {
            using (Database db = new Database(this.ConnectionString, this.ProviderName))
            {
                PUOrder order = this.OrderByOrderIdAndExtOrderId(
                    orderObject.OrderId,
                    orderObject.ExtOrderId
                    );

                if (order.Status == OrderStatuses.COMPLETED.ToString() ||
                    order.Status == OrderStatuses.CANCELED.ToString() ||
                    order.Status == OrderStatuses.REJECTED.ToString())
                    return false;

                order.Status = orderObject.Status.ToString();
                order.OrderObject =
                    (orderObject != null) ? JsonConvert.SerializeObject(
                        orderObject, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
                    ) : null;
                order.Updated = DateTime.Now;
                db.Update(order);
                return true;
            }
        }


        /// <summary>
        /// Tworzy rekord w tabeli [PUCommLog] dla zamówienia o danym orderId
        /// </summary>
        /// <param name="orderId">Identyfikator zewnętrzny zamówienia (OrderId)</param>
        /// <param name="requestType">Typ żądania</param>
        /// <param name="request">Treść żądania</param>
        /// <param name="responseType">Typ odpowiedzi</param>
        /// <param name="response">Treść odpowiedzi</param>
        /// <returns>Id utworzonego rekordu</returns>
        public long? CommLogCreateForOrderId(
            string orderId, 
            string requestType, 
            string request, 
            string responseType,
            string response
            )
        {
            PUOrder order = this.OrderByOrderId(orderId);
            if (order == null) return null;
            return this.CommLogCreate(order, requestType, request, responseType, response);
        }


        /// <summary>
        /// Tworzy rekord w tabeli [PUCommLog] dla zamówienia o danym extOrderId
        /// </summary>
        /// <param name="extOrderId">Identyfikator zewnętrzny zamówienia (ExtOrderId)</param>
        /// <param name="requestType">Typ żądania</param>
        /// <param name="request">Treść żądania</param>
        /// <param name="responseType">Typ odpowiedzi</param>
        /// <param name="response">Treść odpowiedzi</param>
        /// <returns>Id utworzonego rekordu</returns>
        public long? CommLogCreateForExtOrderId(
            string extOrderId,
            string requestType,
            string request,
            string responseType,
            string response
            )
        {
            PUOrder order = this.OrderByExtOrderId(extOrderId);
            if (order == null) return null;
            return this.CommLogCreate(order, requestType, request, responseType, response);
        }


        /// <summary>
        /// Tworzy rekord w tabeli [PUCommLog]
        /// </summary>
        /// <param name="order">Obiekt zamówienia</param>
        /// <param name="requestType">Typ żądania</param>
        /// <param name="request">Treść żądania</param>
        /// <param name="responseType">Typ odpowiedzi</param>
        /// <param name="response">Treść odpowiedzi</param>
        /// <returns>Id utworzonego rekordu</returns>
        public long CommLogCreate(
            PUOrder order,
            string requestType,
            string request,
            string responseType,
            string response
            )
        {
            using (Database db = new Database(this.ConnectionString, this.ProviderName))
            {
                PUCommLog commLog = new PUCommLog();
                commLog.PUOrderId = order.Id;

                if (request != null)
                {
                    commLog.RequestType = requestType;
                    commLog.RequestContent = request;
                    commLog.RequestDate = DateTime.Now;
                }

                if (response != null)
                {
                    commLog.ResponseType = responseType;
                    commLog.ResponseContent = response;
                    commLog.ResponseDate = DateTime.Now;
                }

                db.Insert(commLog);
                return commLog.Id;
            }
        }


        /// <summary>
        /// Aktualizuje rekord w tabeli [PUCommLog]
        /// </summary>
        /// <param name="commLogId">Id rekordu w tabeli [PUCommLog]</param>
        /// <param name="responseType">Typ odpowiedzi</param>
        /// <param name="response">Treść odpowiedzi</param>
        public void CommLogUpdate(
            long commLogId, 
            string responseType,
            string response
            )
        {
            using (Database db = new Database(this.ConnectionString, this.ProviderName))
            {
                PUCommLog commLog =
                    db.FirstOrDefault<PUCommLog>("SELECT * FROM [PUCommLog] WHERE [Id]=@0", commLogId);
                if (commLog == null)
                    throw new CommLogNotFoundException("CommLog not found! Id=" + commLogId);

                commLog.ResponseType = responseType;
                commLog.ResponseContent =  response;
                commLog.ResponseDate = DateTime.Now;

                db.Update(commLog);
            }
        }


        /// <summary>
        /// Tworzy nowy zwrot dla zamówienia
        /// </summary>
        /// <param name="orderId">Wewnętrzny identyfikator zamówienia/transakcji OrderId</param>
        /// <returns>Zewnętrzny identyfikator zwrotu ExtRefundId</returns>
        public string RefundCreate(string orderId)
        {
            PUOrder order = this.OrderByOrderId(orderId);

            using (Database db = new Database(this.ConnectionString, this.ProviderName))
            {
                PURefund refund = new PURefund();
                refund.PUOrderId = order.Id;
                refund.Created = DateTime.Now;
                refund.ExtRefundId = Guid.NewGuid().ToString();
                db.Insert(refund);
                return refund.ExtRefundId;
            }
        }


        /// <summary>
        /// Aktualizuje wewnętrzny identyfikator zwrotu (RefundId) dla zwrotu o danym zewnętrznym identyfikatorze (ExtRefundId)
        /// </summary>
        /// <param name="extRefundId">Zewnętrzny identyfikator zwrotu ExtRefundId</param>
        /// <param name="refundId">Wewnętrzny identyfikator zwrotu RefundId</param>
        public void RefundUpdateRefundId(string extRefundId, string refundId)
        {
            using (Database db = new Database(this.ConnectionString, this.ProviderName))
            {
                PURefund refund = this.RefundByExtRefundId(extRefundId);
                refund.RefundId = refundId;
                refund.Updated = DateTime.Now;
                db.Update(refund);
            }
        }


        /// <summary>
        /// Aktualizuje obiekt zwrotu
        /// </summary>
        /// <param name="refundObject">Obiekt zwrotu</param>
        /// <returns>Znacznik dokoniania aktualizacji</returns>
        public bool RefundUpdate(RefundRecord_Type refundObject)
        {
            using (Database db = new Database(this.ConnectionString, this.ProviderName))
            {
                PURefund refund = 
                    this.RefundByRefundIdAndExtRefundId(
                        refundObject.RefundId, 
                        refundObject.ExtRefundId
                        );

                if (refund.Status == RefundStatuses.CANCELED.ToString() ||
                    refund.Status == RefundStatuses.FINALIZED.ToString())
                    return false;

                refund.Status = refundObject.Status.ToString();
                refund.RefundObject =
                    (refundObject != null) ? JsonConvert.SerializeObject(
                        refundObject, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
                    ) : null;
                refund.Updated = DateTime.Now;
                db.Update(refund);
                return true;
            }
        }


        /// <summary>
        /// Zwraca aktualny obiekt opisujący zwrot o danym identyfikatorze
        /// </summary>
        /// <param name="refundId">Wewnętrzny identyfikator zwrotu RefundId</param>
        /// <returns>Obiekt zwrotu</returns>
        public RefundRecord_Type GetRefundObject(string refundId)
        {
            PURefund refund = this.RefundByRefundId(refundId);
            if (refund == null) return null;
            return JsonConvert.DeserializeObject<RefundRecord_Type>(refund.RefundObject);
        }


        /// <summary>
        /// Zwraca aktualny obiekt opisujący zamówienie/transakcję o danym identyfikatorze
        /// </summary>
        /// <param name="orderId">Wewnętrzny identyfikator zamówienia OrderId</param>
        /// <returns>Obiekt zamówienia/transakcji</returns>
        public OrderRecord GetOrderObject(string orderId)
        {
            PUOrder order = this.OrderByOrderId(orderId);
            if (order == null) return null;
            return JsonConvert.DeserializeObject<OrderRecord>(order.OrderObject);
        }


        /// <summary>
        /// Zwraca dla danego zamówienia/transakcji obiekty wysłanych żądań i otrzymanych na nie odpowiedzi od PayU
        /// </summary>
        /// <param name="extOrderId">Zewnętrzny identyfikator zamówienia ExtOrderId</param>
        /// <returns>Obiekty wysłanych żądań i otrzymanych na nie odpowiedzi od PayU</returns>
        public Tuple<object, object>[] GetRequestsAndResponses(string extOrderId)
        {
            List<Tuple<object, object>> list = new List<Tuple<object, object>>();

            PUCommLog[] logs = this.GetOrderCommLogs(extOrderId);

            foreach (PUCommLog log in logs)
                list.Add(this.ParseRequestAndResponse(log));

            return list.ToArray();
        }
    }
}
