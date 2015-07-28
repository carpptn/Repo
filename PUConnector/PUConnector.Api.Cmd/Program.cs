using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PUConnector.Commons.ApiModel;
using System;
using System.Linq;

namespace PUConnector.Api.Cmd
{
    class Program
    {
        // entry point

        static void Main(string[] args)
        {
            if (HelpMode(args))
                return;
            
            Type requestType;
            string jsonRequest;
            if (!ReadAndValidateArgs(args, out requestType, out jsonRequest))
                return;

            ConnectAndExecute(requestType, jsonRequest);
        }


        // private methods

        private static void ConnectAndExecute(Type requestType, string jsonRequest)
        {
            Connector connector = new Connector();

            object request = null;
            try
            {
                request = JsonConvert.DeserializeObject(jsonRequest, requestType);
            }
            catch (Exception e)
            {
                Console.WriteLine("Json request serialization error: {0}", e.Message);
                return;
            }

            object response = null;
            switch (requestType.Name)
            {
                case "OrderCreateRequest":
                    response = connector.OrderCreate((OrderCreateRequest)request);
                    break;
                case "OrderCancelRequest":
                    response = connector.OrderCancel((OrderCancelRequest)request);
                    break;
                case "OrderRetrieveRequest":
                    response = connector.OrderRetrieve((OrderRetrieveRequest)request);
                    break;
                case "OrderStatusUpdateRequest":
                    response = connector.OrderStatusUpdate((OrderStatusUpdateRequest)request);
                    break;
                case "RefundCreateRequest":
                    response = connector.RefundCreate((RefundCreateRequest)request);
                    break;
            }

            if (response != null)
                Console.WriteLine(JsonConvert.SerializeObject(
                    response, 
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
                    ));
        }


        private static bool ReadAndValidateArgs(
            string[] args, 
            out Type requestType, 
            out string jsonRequest
            )
        {
            requestType = null;
            jsonRequest = null;

            if (args.Length != 2)
            {
                Console.WriteLine("Error: invalid number of arguments");
                return false;
            }

            Type[] requestTypes = new Type[] {
                typeof(OrderCreateRequest),
                typeof(OrderCancelRequest),
                typeof(OrderRetrieveRequest),
                typeof(OrderStatusUpdateRequest),
                typeof(RefundCreateRequest)
            };

            requestType = requestTypes.FirstOrDefault(a => a.Name == args[0]);
            if (requestType == null)
            {
                Console.WriteLine("Error: invalid request type");
                return false;
            }

            jsonRequest = args[1].Replace("'", "\"");

            try
            {
                JContainer.Parse(jsonRequest);
            }
            catch (Exception e)
            {
                Console.WriteLine("Json request is not valid: {0}", e.Message);
                return false;
            }

            return true;
        }

        
        private static bool HelpMode(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("PuConnector.Api.Cmd 1.0\n");
                Console.WriteLine("using:");
                Console.WriteLine("PuConnector requestType jsonRequest\n");
                Console.WriteLine("requestType: OrderCreateRequest | OrderCancelRequest | OrderRetrieveRequest | OrderStatusUpdate | RefundCreateRequest\n");
                Console.WriteLine(@"example: PuConnector.Api.Cmd.exe OrderCreateRequest ""{'merchantPosId':'145227', 'notifyUrl':'http://MyDomain.com/OrderNotify/Receive','customerIp':'90.1.1.1', 'description':'test order','currencyCode':'PLN','totalAmount':1000,'products':[{'name':'test product','unitPrice':1000,'quantity':1}]}""" + "\n");
                return true;
            }

            return false;
        }
    }
}
