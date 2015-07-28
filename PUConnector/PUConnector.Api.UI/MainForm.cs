using Newtonsoft.Json;
using PUConnector.Commons.ApiModel;
using PUConnector.Commons.WCF;
using PUConnector.Db.Repository;
using PUConnector.Notifications.Service;
using PUConnector.OrdersManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;

namespace PUConnector.Api.UI
{
    public partial class MainForm : Form
    {
        // variables

        private Connector connector;
        private Manager manager;
        private ServiceHost host;

        
        // constructor

        public MainForm()
        {
            InitializeComponent();
        }


        // protected methods

        protected override void OnLoad(EventArgs e)
        {
            cbRequestType.SelectedIndex = 0;

            this.connector = new Connector();
            this.manager = connector.OrdersManager;

            OrderNotify serviceInstance = new OrderNotify();
            serviceInstance.OnOrderNotifyReceived += serviceInstance_OnOrderNotifyReceived;
            serviceInstance.OnRefundNotifyReceived += serviceInstance_OnRefundNotifyReceived;

            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            this.host =
                new ServiceHost(serviceInstance, new Uri[] { });
            this.host.Open();

            if (this.manager != null)
                this.LoadOrders();
            else
            {
                this.scMain.Panel2Collapsed = true;
                this.scMain.Panel2.Hide();
            }

            base.OnLoad(e);
        }


        protected override void OnClosed(EventArgs e)
        {
            if (this.host != null && this.host.State == CommunicationState.Opened)
            this.host.Close();

            base.OnClosed(e);
        }


        // private methods

        private void LoadOrders()
        {
            int ordersLimit = int.Parse(ConfigurationManager.AppSettings["OrdersLimit"]);
            dgvOrders.DataSource = this.manager.OrdersGet(ordersLimit > 0 ? (int?)ordersLimit : null);
            dgvOrders.Columns[3].Width = 230;
            dgvOrders.Columns[4].Width = 230;
        }


        private void AddNewOrders()
        {
            long id = 0;
            PUOrder order = this.SelectedOrder();
            if (order != null) id = order.Id;

            List<PUOrder> orders = new List<PUOrder>((PUOrder[])dgvOrders.DataSource);
            orders.InsertRange(0, this.manager.NewOrdersGet(id));
            dgvOrders.DataSource = orders.ToArray();

            dgvOrders.Columns[3].Width = 230;
            dgvOrders.Columns[4].Width = 230;
        }


        private PUOrder SelectedOrder()
        {
            if (dgvOrders.CurrentRow == null) 
                return null;

            int rowIdx = dgvOrders.CurrentRow.Index;
            if (rowIdx >= 0)
            {
                PUOrder[] orders = (PUOrder[])dgvOrders.DataSource;
                if (orders.Length > rowIdx)
                {
                    return orders[rowIdx];
                }
            }
            return null;
        }


        private void LoadRefunds()
        {
            PUOrder order = this.SelectedOrder();
            if (order == null) return;

            PURefund[] refunds = manager.GetOrderRefunds(order.ExtOrderId);
            dgvRefunds.DataSource = refunds;
            dgvRefunds.Columns[4].Width = 140;
            dgvRefunds.Columns[5].Width = 140;
        }


        private void LoadCommLogs()
        {
            PUOrder order = this.SelectedOrder();
            if (order == null) return;

            PUCommLog[] commLogs = manager.GetOrderCommLogs(order.ExtOrderId);
            dgvCommLogs.DataSource = commLogs;
            dgvCommLogs.Columns[2].Width = 140;
            dgvCommLogs.Columns[5].Width = 140;
        }


        private void ShowCommLogReqResp()
        {
            if (dgvCommLogs.CurrentRow == null)
                return;

            int rowIdx = dgvCommLogs.CurrentRow.Index;
            if (rowIdx >= 0)
            {
                PUCommLog[] commLogs = (PUCommLog[])dgvCommLogs.DataSource;
                if (commLogs.Length > rowIdx)
                {
                    PUCommLog log = commLogs[rowIdx];

                    cbRequestType.SelectedItem = log.RequestType;
                    rtbRequest.Text = this.IndentJson(log.RequestContent);
                    rtbResponse.Text = this.IndentJson(log.ResponseContent);
                    txtResponseCode.Text = log.ResponseDate.ToString() + " " + log.ResponseType;
                }
            }
        }


        private string IndentJson(object obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;

            if (obj == null) return string.Empty;
            if (obj is string)
            {
                string str = (string)obj;
                if (string.IsNullOrEmpty(str)) return string.Empty;
                obj = JsonConvert.DeserializeObject(str, settings);
            }

            string indentJson = 
                JsonConvert.SerializeObject(obj, Formatting.Indented, settings);

            return indentJson;
        }


        private void btnSendReq_Click(object sender, EventArgs e)
        {
            if (cbRequestType.SelectedItem == null)
                return;

            string reqtype = cbRequestType.SelectedItem.ToString();

            Type[] requestTypes = new Type[] {
                typeof(OrderCreateRequest),
                typeof(OrderCancelRequest),
                typeof(OrderRetrieveRequest),
                typeof(OrderStatusUpdateRequest),
                typeof(RefundCreateRequest)
            };

            Type requestType = requestTypes.FirstOrDefault(a => a.Name == reqtype);

            rtbResponse.Text = string.Empty;
            txtResponseCode.Text = string.Empty;

            object request = null;
            try
            {
                request = JsonConvert.DeserializeObject(rtbRequest.Text, requestType);
            }
            catch (Exception ex)
            {
                string message = string.Format("Json request serialization error: {0}", ex.Message);
                new TraceSource("PUConnector.UI").TraceEvent(TraceEventType.Error, 0, message);
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            object response = null;
            switch (requestType.Name)
            {
                case "OrderCreateRequest":
                    response = connector.OrderCreate((OrderCreateRequest)request);
                    if (this.manager != null)
                        this.AddNewOrders();
                    break;
                case "OrderCancelRequest":
                    response = connector.OrderCancel((OrderCancelRequest)request);
                    break;
                case "OrderRetrieveRequest":
                    response = connector.OrderRetrieve((OrderRetrieveRequest)request);
                    if (this.manager != null)
                    {
                        string orderId = ((OrderRetrieveRequest)request).OrderId;
                        this.RefreshOrder(this.manager.ExtOrderIdByOrderId(orderId));
                    }
                    break;
                case "OrderStatusUpdateRequest":
                    response = connector.OrderStatusUpdate((OrderStatusUpdateRequest)request);
                    break;
                case "RefundCreateRequest":
                    response = connector.RefundCreate((RefundCreateRequest)request);
                    break;
            }

            Cursor.Current = Cursors.Default;

            rtbResponse.Text = this.IndentJson(response);
            txtResponseCode.Text = 
                DateTime.Now.ToString() + " " + 
                (response != null ? response.GetType().Name : "null");
        }

        
        private void ShowRequestTemplate()
        {
            if (cbRequestType.SelectedItem == null)
                return;

            string reqtype = cbRequestType.SelectedItem.ToString();

            string template = Encoding.UTF8.GetString((byte[])Resources.ResourceManager.GetObject(reqtype));

            rtbRequest.Text = template;
            rtbResponse.Text = string.Empty;
            txtResponseCode.Text = string.Empty;
        }


        private void dgvCommLogs_SelectionChanged(object sender, EventArgs e)
        {
            this.ShowCommLogReqResp();
        }


        private void cbRequestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowRequestTemplate();
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            this.ShowRequestTemplate();
        }


        private void btnConsClear_Click(object sender, EventArgs e)
        {
            rtbConsole.Text = string.Empty;
        }


        private void btnOrdersRefresh_Click(object sender, EventArgs e)
        {
            this.LoadOrders();
        }


        private void buttonLogsRefresh_Click(object sender, EventArgs e)
        {
            this.LoadCommLogs();
        }


        private void buttonRefundsRefresh_Click(object sender, EventArgs e)
        {
            this.LoadRefunds();
        }


        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            this.LoadCommLogs();
            this.LoadRefunds();
        }


        private void serviceInstance_OnOrderNotifyReceived(OrderRecord orderRecord)
        {
            rtbNotify.Text = this.IndentJson(orderRecord);
            txtLastNotifyDt.Text = DateTime.Now.ToString();

            PUOrder order = this.SelectedOrder();
            if (order != null &&
                orderRecord != null &&
                order.OrderId == orderRecord.OrderId)
                this.LoadCommLogs();

            if (orderRecord != null)
                this.RefreshOrder(orderRecord.ExtOrderId);
        }


        private void RefreshOrder(string extOrderId)
        {
            PUOrder[] orders = (PUOrder[])dgvOrders.DataSource;
            if (orders == null || orders.Length <= 0) return;

            PUOrder order = orders.FirstOrDefault(a => a.ExtOrderId == extOrderId);
            if (order == null) return;

            PUOrder updatedOrder =
                this.manager.OrderByExtOrderId(order.ExtOrderId);

            order.Updated = updatedOrder.Updated;
            order.OrderObject = updatedOrder.OrderObject;
            order.Status = updatedOrder.Status;

            dgvOrders.Refresh();
        }


        private void serviceInstance_OnRefundNotifyReceived(RefundRecord_Type refundRecord)
        {
            rtbNotify.Text = this.IndentJson(refundRecord);
            txtLastNotifyDt.Text = DateTime.Now.ToString();
            this.LoadRefunds();
        }


        private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            if (e.Exception != null)
            {
                Exception ex = e.Exception;
                string message =
                    string.Format("Unhandled exception {0}: {1}",
                    ex.GetType().Name,
                    ex.Message
                    );

                new TraceSource("PUConnector.UI").TraceEvent(TraceEventType.Error, 0, message);
            }

            new ErrorHandler().HandleError(e.Exception);
        }


        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            new ErrorHandler().HandleError(e.ExceptionObject as Exception);
        }
    }
}
