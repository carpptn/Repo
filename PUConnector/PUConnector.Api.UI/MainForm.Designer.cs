namespace PUConnector.Api.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panelRequest = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSendReq = new System.Windows.Forms.Button();
            this.rtbRequest = new FastColoredTextBoxNS.FastColoredTextBox();
            this.pnlRequestType = new System.Windows.Forms.Panel();
            this.cbRequestType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelResponse = new System.Windows.Forms.Panel();
            this.rtbResponse = new FastColoredTextBoxNS.FastColoredTextBox();
            this.pnlResponseCode = new System.Windows.Forms.Panel();
            this.txtResponseCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelNotify = new System.Windows.Forms.Panel();
            this.rtbNotify = new FastColoredTextBoxNS.FastColoredTextBox();
            this.pnlNotifyInfo = new System.Windows.Forms.Panel();
            this.txtLastNotifyDt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.scRRNC = new System.Windows.Forms.SplitContainer();
            this.btnConsClear = new System.Windows.Forms.Button();
            this.rtbConsole = new System.Windows.Forms.RichTextBox();
            this.scOrders = new System.Windows.Forms.SplitContainer();
            this.btnOrdersRefresh = new System.Windows.Forms.Button();
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonLogsRefresh = new System.Windows.Forms.Button();
            this.dgvCommLogs = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvRefunds = new System.Windows.Forms.DataGridView();
            this.buttonRefundsRefresh = new System.Windows.Forms.Button();
            this.panelRequest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtbRequest)).BeginInit();
            this.pnlRequestType.SuspendLayout();
            this.panelResponse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtbResponse)).BeginInit();
            this.pnlResponseCode.SuspendLayout();
            this.panelNotify.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtbNotify)).BeginInit();
            this.pnlNotifyInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scRRNC)).BeginInit();
            this.scRRNC.Panel1.SuspendLayout();
            this.scRRNC.Panel2.SuspendLayout();
            this.scRRNC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scOrders)).BeginInit();
            this.scOrders.Panel1.SuspendLayout();
            this.scOrders.Panel2.SuspendLayout();
            this.scOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCommLogs)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRefunds)).BeginInit();
            this.SuspendLayout();
            // 
            // panelRequest
            // 
            this.panelRequest.Controls.Add(this.btnClear);
            this.panelRequest.Controls.Add(this.btnSendReq);
            this.panelRequest.Controls.Add(this.rtbRequest);
            this.panelRequest.Controls.Add(this.pnlRequestType);
            this.panelRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRequest.Location = new System.Drawing.Point(0, 0);
            this.panelRequest.Name = "panelRequest";
            this.panelRequest.Size = new System.Drawing.Size(329, 263);
            this.panelRequest.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Location = new System.Drawing.Point(13, 237);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Template";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSendReq
            // 
            this.btnSendReq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendReq.Location = new System.Drawing.Point(241, 237);
            this.btnSendReq.Name = "btnSendReq";
            this.btnSendReq.Size = new System.Drawing.Size(75, 23);
            this.btnSendReq.TabIndex = 2;
            this.btnSendReq.Text = "Send";
            this.btnSendReq.UseVisualStyleBackColor = true;
            this.btnSendReq.Click += new System.EventHandler(this.btnSendReq_Click);
            // 
            // rtbRequest
            // 
            this.rtbRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbRequest.AutoCompleteBrackets = true;
            this.rtbRequest.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.rtbRequest.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n";
            this.rtbRequest.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.rtbRequest.BackBrush = null;
            this.rtbRequest.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.rtbRequest.CharHeight = 14;
            this.rtbRequest.CharWidth = 8;
            this.rtbRequest.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.rtbRequest.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.rtbRequest.IsReplaceMode = false;
            this.rtbRequest.Language = FastColoredTextBoxNS.Language.JS;
            this.rtbRequest.LeftBracket = '(';
            this.rtbRequest.LeftBracket2 = '{';
            this.rtbRequest.Location = new System.Drawing.Point(0, 59);
            this.rtbRequest.Name = "rtbRequest";
            this.rtbRequest.Paddings = new System.Windows.Forms.Padding(0);
            this.rtbRequest.RightBracket = ')';
            this.rtbRequest.RightBracket2 = '}';
            this.rtbRequest.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.rtbRequest.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("rtbRequest.ServiceColors")));
            this.rtbRequest.ShowFoldingLines = true;
            this.rtbRequest.Size = new System.Drawing.Size(326, 174);
            this.rtbRequest.TabIndex = 1;
            this.rtbRequest.Zoom = 100;
            // 
            // pnlRequestType
            // 
            this.pnlRequestType.Controls.Add(this.cbRequestType);
            this.pnlRequestType.Controls.Add(this.label1);
            this.pnlRequestType.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRequestType.Location = new System.Drawing.Point(0, 0);
            this.pnlRequestType.Name = "pnlRequestType";
            this.pnlRequestType.Size = new System.Drawing.Size(329, 53);
            this.pnlRequestType.TabIndex = 0;
            // 
            // cbRequestType
            // 
            this.cbRequestType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRequestType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRequestType.FormattingEnabled = true;
            this.cbRequestType.Items.AddRange(new object[] {
            "OrderCreateRequest",
            "OrderCancelRequest",
            "OrderRetrieveRequest",
            "OrderStatusUpdateRequest",
            "RefundCreateRequest"});
            this.cbRequestType.Location = new System.Drawing.Point(7, 21);
            this.cbRequestType.Name = "cbRequestType";
            this.cbRequestType.Size = new System.Drawing.Size(309, 21);
            this.cbRequestType.TabIndex = 1;
            this.cbRequestType.SelectedIndexChanged += new System.EventHandler(this.cbRequestType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Request";
            // 
            // panelResponse
            // 
            this.panelResponse.Controls.Add(this.rtbResponse);
            this.panelResponse.Controls.Add(this.pnlResponseCode);
            this.panelResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelResponse.Location = new System.Drawing.Point(0, 0);
            this.panelResponse.Name = "panelResponse";
            this.panelResponse.Size = new System.Drawing.Size(286, 263);
            this.panelResponse.TabIndex = 2;
            // 
            // rtbResponse
            // 
            this.rtbResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbResponse.AutoCompleteBrackets = true;
            this.rtbResponse.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.rtbResponse.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n";
            this.rtbResponse.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.rtbResponse.BackBrush = null;
            this.rtbResponse.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.rtbResponse.CharHeight = 14;
            this.rtbResponse.CharWidth = 8;
            this.rtbResponse.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.rtbResponse.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.rtbResponse.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.rtbResponse.IsReplaceMode = false;
            this.rtbResponse.Language = FastColoredTextBoxNS.Language.JS;
            this.rtbResponse.LeftBracket = '(';
            this.rtbResponse.LeftBracket2 = '{';
            this.rtbResponse.Location = new System.Drawing.Point(7, 59);
            this.rtbResponse.Name = "rtbResponse";
            this.rtbResponse.Paddings = new System.Windows.Forms.Padding(0);
            this.rtbResponse.ReadOnly = true;
            this.rtbResponse.RightBracket = ')';
            this.rtbResponse.RightBracket2 = '}';
            this.rtbResponse.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.rtbResponse.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("rtbResponse.ServiceColors")));
            this.rtbResponse.ShowFoldingLines = true;
            this.rtbResponse.Size = new System.Drawing.Size(276, 174);
            this.rtbResponse.TabIndex = 2;
            this.rtbResponse.Zoom = 100;
            // 
            // pnlResponseCode
            // 
            this.pnlResponseCode.Controls.Add(this.txtResponseCode);
            this.pnlResponseCode.Controls.Add(this.label2);
            this.pnlResponseCode.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlResponseCode.Location = new System.Drawing.Point(0, 0);
            this.pnlResponseCode.Name = "pnlResponseCode";
            this.pnlResponseCode.Size = new System.Drawing.Size(286, 53);
            this.pnlResponseCode.TabIndex = 1;
            // 
            // txtResponseCode
            // 
            this.txtResponseCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResponseCode.Location = new System.Drawing.Point(7, 21);
            this.txtResponseCode.Name = "txtResponseCode";
            this.txtResponseCode.ReadOnly = true;
            this.txtResponseCode.Size = new System.Drawing.Size(265, 20);
            this.txtResponseCode.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Response";
            // 
            // panelNotify
            // 
            this.panelNotify.Controls.Add(this.rtbNotify);
            this.panelNotify.Controls.Add(this.pnlNotifyInfo);
            this.panelNotify.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNotify.Location = new System.Drawing.Point(0, 0);
            this.panelNotify.Name = "panelNotify";
            this.panelNotify.Size = new System.Drawing.Size(284, 263);
            this.panelNotify.TabIndex = 4;
            // 
            // rtbNotify
            // 
            this.rtbNotify.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbNotify.AutoCompleteBrackets = true;
            this.rtbNotify.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.rtbNotify.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n";
            this.rtbNotify.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.rtbNotify.BackBrush = null;
            this.rtbNotify.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.rtbNotify.CharHeight = 14;
            this.rtbNotify.CharWidth = 8;
            this.rtbNotify.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.rtbNotify.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.rtbNotify.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.rtbNotify.IsReplaceMode = false;
            this.rtbNotify.Language = FastColoredTextBoxNS.Language.JS;
            this.rtbNotify.LeftBracket = '(';
            this.rtbNotify.LeftBracket2 = '{';
            this.rtbNotify.Location = new System.Drawing.Point(7, 59);
            this.rtbNotify.Name = "rtbNotify";
            this.rtbNotify.Paddings = new System.Windows.Forms.Padding(0);
            this.rtbNotify.ReadOnly = true;
            this.rtbNotify.RightBracket = ')';
            this.rtbNotify.RightBracket2 = '}';
            this.rtbNotify.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.rtbNotify.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("rtbNotify.ServiceColors")));
            this.rtbNotify.ShowFoldingLines = true;
            this.rtbNotify.Size = new System.Drawing.Size(270, 174);
            this.rtbNotify.TabIndex = 3;
            this.rtbNotify.Zoom = 100;
            // 
            // pnlNotifyInfo
            // 
            this.pnlNotifyInfo.Controls.Add(this.txtLastNotifyDt);
            this.pnlNotifyInfo.Controls.Add(this.label3);
            this.pnlNotifyInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNotifyInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlNotifyInfo.Name = "pnlNotifyInfo";
            this.pnlNotifyInfo.Size = new System.Drawing.Size(284, 53);
            this.pnlNotifyInfo.TabIndex = 1;
            // 
            // txtLastNotifyDt
            // 
            this.txtLastNotifyDt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLastNotifyDt.Location = new System.Drawing.Point(7, 21);
            this.txtLastNotifyDt.Name = "txtLastNotifyDt";
            this.txtLastNotifyDt.ReadOnly = true;
            this.txtLastNotifyDt.Size = new System.Drawing.Size(263, 20);
            this.txtLastNotifyDt.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Notification";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelRequest);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(907, 263);
            this.splitContainer1.SplitterDistance = 329;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panelResponse);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panelNotify);
            this.splitContainer2.Size = new System.Drawing.Size(574, 263);
            this.splitContainer2.SplitterDistance = 286;
            this.splitContainer2.TabIndex = 0;
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.scRRNC);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.scOrders);
            this.scMain.Size = new System.Drawing.Size(907, 501);
            this.scMain.SplitterDistance = 292;
            this.scMain.TabIndex = 3;
            // 
            // scRRNC
            // 
            this.scRRNC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scRRNC.Location = new System.Drawing.Point(0, 0);
            this.scRRNC.Name = "scRRNC";
            this.scRRNC.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scRRNC.Panel1
            // 
            this.scRRNC.Panel1.Controls.Add(this.splitContainer1);
            // 
            // scRRNC.Panel2
            // 
            this.scRRNC.Panel2.BackColor = System.Drawing.Color.White;
            this.scRRNC.Panel2.Controls.Add(this.btnConsClear);
            this.scRRNC.Panel2.Controls.Add(this.rtbConsole);
            this.scRRNC.Size = new System.Drawing.Size(907, 292);
            this.scRRNC.SplitterDistance = 263;
            this.scRRNC.TabIndex = 0;
            // 
            // btnConsClear
            // 
            this.btnConsClear.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnConsClear.Location = new System.Drawing.Point(844, 0);
            this.btnConsClear.Name = "btnConsClear";
            this.btnConsClear.Size = new System.Drawing.Size(63, 25);
            this.btnConsClear.TabIndex = 1;
            this.btnConsClear.Text = "Clear Console";
            this.btnConsClear.UseVisualStyleBackColor = true;
            this.btnConsClear.Click += new System.EventHandler(this.btnConsClear_Click);
            // 
            // rtbConsole
            // 
            this.rtbConsole.BackColor = System.Drawing.Color.DimGray;
            this.rtbConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbConsole.ForeColor = System.Drawing.Color.White;
            this.rtbConsole.Location = new System.Drawing.Point(0, 0);
            this.rtbConsole.Name = "rtbConsole";
            this.rtbConsole.ReadOnly = true;
            this.rtbConsole.Size = new System.Drawing.Size(907, 25);
            this.rtbConsole.TabIndex = 0;
            this.rtbConsole.Text = "READY.";
            // 
            // scOrders
            // 
            this.scOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scOrders.Location = new System.Drawing.Point(0, 0);
            this.scOrders.Name = "scOrders";
            this.scOrders.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scOrders.Panel1
            // 
            this.scOrders.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.scOrders.Panel1.Controls.Add(this.btnOrdersRefresh);
            this.scOrders.Panel1.Controls.Add(this.dgvOrders);
            // 
            // scOrders.Panel2
            // 
            this.scOrders.Panel2.Controls.Add(this.tabControl1);
            this.scOrders.Size = new System.Drawing.Size(907, 205);
            this.scOrders.SplitterDistance = 97;
            this.scOrders.TabIndex = 0;
            // 
            // btnOrdersRefresh
            // 
            this.btnOrdersRefresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOrdersRefresh.Location = new System.Drawing.Point(844, 0);
            this.btnOrdersRefresh.Name = "btnOrdersRefresh";
            this.btnOrdersRefresh.Size = new System.Drawing.Size(63, 97);
            this.btnOrdersRefresh.TabIndex = 2;
            this.btnOrdersRefresh.Text = "Refresh Orders";
            this.btnOrdersRefresh.UseVisualStyleBackColor = true;
            this.btnOrdersRefresh.Click += new System.EventHandler(this.btnOrdersRefresh_Click);
            // 
            // dgvOrders
            // 
            this.dgvOrders.AllowUserToAddRows = false;
            this.dgvOrders.AllowUserToDeleteRows = false;
            this.dgvOrders.AllowUserToOrderColumns = true;
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrders.Location = new System.Drawing.Point(0, 0);
            this.dgvOrders.MultiSelect = false;
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.Size = new System.Drawing.Size(907, 97);
            this.dgvOrders.TabIndex = 0;
            this.dgvOrders.SelectionChanged += new System.EventHandler(this.dgvOrders_SelectionChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(907, 104);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonLogsRefresh);
            this.tabPage1.Controls.Add(this.dgvCommLogs);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(899, 78);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Communiation Logs";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonLogsRefresh
            // 
            this.buttonLogsRefresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonLogsRefresh.Location = new System.Drawing.Point(840, 3);
            this.buttonLogsRefresh.Name = "buttonLogsRefresh";
            this.buttonLogsRefresh.Size = new System.Drawing.Size(56, 72);
            this.buttonLogsRefresh.TabIndex = 3;
            this.buttonLogsRefresh.Text = "Refresh Logs";
            this.buttonLogsRefresh.UseVisualStyleBackColor = true;
            this.buttonLogsRefresh.Click += new System.EventHandler(this.buttonLogsRefresh_Click);
            // 
            // dgvCommLogs
            // 
            this.dgvCommLogs.AllowUserToAddRows = false;
            this.dgvCommLogs.AllowUserToDeleteRows = false;
            this.dgvCommLogs.AllowUserToOrderColumns = true;
            this.dgvCommLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCommLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCommLogs.Location = new System.Drawing.Point(3, 3);
            this.dgvCommLogs.MultiSelect = false;
            this.dgvCommLogs.Name = "dgvCommLogs";
            this.dgvCommLogs.ReadOnly = true;
            this.dgvCommLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCommLogs.Size = new System.Drawing.Size(893, 72);
            this.dgvCommLogs.TabIndex = 1;
            this.dgvCommLogs.SelectionChanged += new System.EventHandler(this.dgvCommLogs_SelectionChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonRefundsRefresh);
            this.tabPage2.Controls.Add(this.dgvRefunds);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(899, 78);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Refunds";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvRefunds
            // 
            this.dgvRefunds.AllowUserToAddRows = false;
            this.dgvRefunds.AllowUserToDeleteRows = false;
            this.dgvRefunds.AllowUserToOrderColumns = true;
            this.dgvRefunds.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvRefunds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRefunds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRefunds.Location = new System.Drawing.Point(3, 3);
            this.dgvRefunds.MultiSelect = false;
            this.dgvRefunds.Name = "dgvRefunds";
            this.dgvRefunds.ReadOnly = true;
            this.dgvRefunds.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRefunds.Size = new System.Drawing.Size(893, 72);
            this.dgvRefunds.TabIndex = 1;
            // 
            // buttonRefundsRefresh
            // 
            this.buttonRefundsRefresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonRefundsRefresh.Location = new System.Drawing.Point(840, 3);
            this.buttonRefundsRefresh.Name = "buttonRefundsRefresh";
            this.buttonRefundsRefresh.Size = new System.Drawing.Size(56, 72);
            this.buttonRefundsRefresh.TabIndex = 4;
            this.buttonRefundsRefresh.Text = "Refresh Refunds";
            this.buttonRefundsRefresh.UseVisualStyleBackColor = true;
            this.buttonRefundsRefresh.Click += new System.EventHandler(this.buttonRefundsRefresh_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(907, 501);
            this.Controls.Add(this.scMain);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUConnector Studio v1.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelRequest.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rtbRequest)).EndInit();
            this.pnlRequestType.ResumeLayout(false);
            this.pnlRequestType.PerformLayout();
            this.panelResponse.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rtbResponse)).EndInit();
            this.pnlResponseCode.ResumeLayout(false);
            this.pnlResponseCode.PerformLayout();
            this.panelNotify.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rtbNotify)).EndInit();
            this.pnlNotifyInfo.ResumeLayout(false);
            this.pnlNotifyInfo.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.scRRNC.Panel1.ResumeLayout(false);
            this.scRRNC.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scRRNC)).EndInit();
            this.scRRNC.ResumeLayout(false);
            this.scOrders.Panel1.ResumeLayout(false);
            this.scOrders.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scOrders)).EndInit();
            this.scOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCommLogs)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRefunds)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRequest;
        private System.Windows.Forms.Panel pnlRequestType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbRequestType;
        private System.Windows.Forms.Panel panelResponse;
        private System.Windows.Forms.Panel pnlResponseCode;
        private System.Windows.Forms.TextBox txtResponseCode;
        private System.Windows.Forms.Label label2;
        private FastColoredTextBoxNS.FastColoredTextBox rtbRequest;
        private System.Windows.Forms.Panel panelNotify;
        private System.Windows.Forms.Panel pnlNotifyInfo;
        private System.Windows.Forms.TextBox txtLastNotifyDt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.SplitContainer scOrders;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvCommLogs;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvRefunds;
        private System.Windows.Forms.SplitContainer scRRNC;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSendReq;
        private FastColoredTextBoxNS.FastColoredTextBox rtbResponse;
        private FastColoredTextBoxNS.FastColoredTextBox rtbNotify;
        private System.Windows.Forms.Button btnConsClear;
        private System.Windows.Forms.Button btnOrdersRefresh;
        public System.Windows.Forms.RichTextBox rtbConsole;
        private System.Windows.Forms.Button buttonLogsRefresh;
        private System.Windows.Forms.Button buttonRefundsRefresh;
    }
}

