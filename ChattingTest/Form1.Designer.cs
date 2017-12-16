namespace ChattingTest
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lstPeers = new System.Windows.Forms.ListBox();
            this.btnListen = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lstMessages = new System.Windows.Forms.ListBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtServerAddress = new System.Windows.Forms.ComboBox();
            this.cmbAddresses = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRefreshAddresses = new System.Windows.Forms.Button();
            this.btnRefreshServers = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server Address:";
            // 
            // btnConnect
            // 
            this.btnConnect.Enabled = false;
            this.btnConnect.Location = new System.Drawing.Point(443, 87);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(82, 27);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(412, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "Peers";
            // 
            // lstPeers
            // 
            this.lstPeers.FormattingEnabled = true;
            this.lstPeers.ItemHeight = 19;
            this.lstPeers.Items.AddRange(new object[] {
            "User Name",
            "Root",
            "Client",
            "Golden",
            "Knight"});
            this.lstPeers.Location = new System.Drawing.Point(393, 153);
            this.lstPeers.Name = "lstPeers";
            this.lstPeers.Size = new System.Drawing.Size(120, 194);
            this.lstPeers.TabIndex = 5;
            this.lstPeers.SelectedIndexChanged += new System.EventHandler(this.lstPeers_SelectedIndexChanged);
            // 
            // btnListen
            // 
            this.btnListen.Enabled = false;
            this.btnListen.Location = new System.Drawing.Point(443, 11);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(82, 28);
            this.btnListen.TabIndex = 1;
            this.btnListen.Text = "Listen";
            this.btnListen.UseVisualStyleBackColor = true;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 362);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(532, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(39, 17);
            this.statusLabel.Text = "Ready";
            // 
            // lstMessages
            // 
            this.lstMessages.FormattingEnabled = true;
            this.lstMessages.ItemHeight = 19;
            this.lstMessages.Items.AddRange(new object[] {
            "Golden: Hiiiii",
            "Me: Hiiiiiiiii",
            "Golden: How are you ??",
            "Me: I\'m Fine What about you ??"});
            this.lstMessages.Location = new System.Drawing.Point(17, 153);
            this.lstMessages.Name = "lstMessages";
            this.lstMessages.Size = new System.Drawing.Size(357, 156);
            this.lstMessages.TabIndex = 4;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(311, 319);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(63, 28);
            this.btnSend.TabIndex = 7;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(17, 319);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(288, 27);
            this.txtMessage.TabIndex = 6;
            this.txtMessage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMessage_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 19);
            this.label3.TabIndex = 11;
            this.label3.Text = "Messages";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 19);
            this.label4.TabIndex = 12;
            this.label4.Text = "Your Name:";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(142, 51);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(207, 27);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.Text = "Golden";
            // 
            // txtServerAddress
            // 
            this.txtServerAddress.FormattingEnabled = true;
            this.txtServerAddress.Location = new System.Drawing.Point(142, 88);
            this.txtServerAddress.Name = "txtServerAddress";
            this.txtServerAddress.Size = new System.Drawing.Size(207, 27);
            this.txtServerAddress.TabIndex = 13;
            this.txtServerAddress.SelectedIndexChanged += new System.EventHandler(this.txtServerAddress_SelectedIndexChanged);
            this.txtServerAddress.TextChanged += new System.EventHandler(this.txtServerAddress_TextChanged);
            // 
            // cmbAddresses
            // 
            this.cmbAddresses.FormattingEnabled = true;
            this.cmbAddresses.Location = new System.Drawing.Point(142, 12);
            this.cmbAddresses.Name = "cmbAddresses";
            this.cmbAddresses.Size = new System.Drawing.Size(207, 27);
            this.cmbAddresses.TabIndex = 14;
            this.cmbAddresses.TextChanged += new System.EventHandler(this.cmbAddresses_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 19);
            this.label5.TabIndex = 15;
            this.label5.Text = "Your Address:";
            // 
            // btnRefreshAddresses
            // 
            this.btnRefreshAddresses.Location = new System.Drawing.Point(355, 12);
            this.btnRefreshAddresses.Name = "btnRefreshAddresses";
            this.btnRefreshAddresses.Size = new System.Drawing.Size(82, 27);
            this.btnRefreshAddresses.TabIndex = 16;
            this.btnRefreshAddresses.Text = "Refresh";
            this.btnRefreshAddresses.UseVisualStyleBackColor = true;
            // 
            // btnRefreshServers
            // 
            this.btnRefreshServers.Location = new System.Drawing.Point(355, 88);
            this.btnRefreshServers.Name = "btnRefreshServers";
            this.btnRefreshServers.Size = new System.Drawing.Size(82, 27);
            this.btnRefreshServers.TabIndex = 17;
            this.btnRefreshServers.Text = "Refresh";
            this.btnRefreshServers.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 384);
            this.Controls.Add(this.btnRefreshServers);
            this.Controls.Add(this.btnRefreshAddresses);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbAddresses);
            this.Controls.Add(this.txtServerAddress);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lstMessages);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnListen);
            this.Controls.Add(this.lstPeers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Chatting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstPeers;
        private System.Windows.Forms.Button btnListen;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ListBox lstMessages;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.ComboBox txtServerAddress;
        private System.Windows.Forms.ComboBox cmbAddresses;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnRefreshAddresses;
        private System.Windows.Forms.Button btnRefreshServers;
    }
}

