using ChattingTest.Broadcast;
using ChattingTest.Connections;
using ChattingTest.HelpingClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ChattingTest
{
    public partial class Form1 : Form
    {
        private Dictionary<User, ClientConnection> connections;
        private ServerConnection server;
        private UdpBroadcast broadcast;
        private List<UdpReceiver> udpReceivers;

        private MessageManager manager;
        User currentUser;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.lstMessages.Items.Clear();
            this.lstPeers.Items.Clear();
            manager = new MessageManager();
            connections = new Dictionary<User, ClientConnection>();

            IPAddress[] addresses = Global.getLocalIPAddresses();
            this.cmbAddresses.Items.AddRange(addresses);
            if (addresses.Length > 0)
                this.cmbAddresses.SelectedIndex = 0;

            udpReceivers = new List<UdpReceiver>();
            foreach (var address in addresses)
            {
                var udpReceiver = new UdpReceiver(this, address);
                udpReceiver.ServerFound += Broadcast_ServerFound;
                udpReceiver.Start();
                udpReceivers.Add(udpReceiver);
            }
        }

        private void Server_ConnectionAccepted(ClientConnection connection)
        {
            prepareConnection(connection);
        }

        private void Connection_MessageRecieved(ClientConnection connection, Message msg)
        {
            manager.AddMessage(msg);

            if (msg.Reciever.Equals(currentUser) || msg.Sender.Equals(currentUser))
                lstMessages.Items.Add(msg);
        }

        private void Connection_Connected(ClientConnection connection, HelpingClasses.User user)
        {
            this.lstPeers.Items.Add(user);
            this.lstPeers.SelectedItem = user;
            this.connections.Add(user, connection);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Message msg = this.connections[currentUser].Send(this.txtMessage.Text);
            this.lstMessages.Items.Add(msg);
            manager.AddMessage(msg);

            this.txtMessage.Clear();
            this.txtMessage.Focus();
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            if (this.btnListen.Text == "Stop")
            {
                this.server.Stop();
                this.server = null;
                this.broadcast.Cancel();
                this.broadcast = null;
                this.btnListen.Text = "Listen";
            }
            else
            {
                IPAddress address;
                try
                {
                    address = IPAddress.Parse(this.cmbAddresses.Text);
                }
                catch
                {
                    MessageBox.Show("IP Address you entered is invalid, Please try again.", "Invalid IP Address", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.server = new ServerConnection(this, txtUserName.Text, new IPEndPoint(address, Global.PORT));
                this.server.LocalUserName = this.txtUserName.Text;
                this.server.ConnectionAccepted += Server_ConnectionAccepted;
                server.Start();

                this.broadcast = new UdpBroadcast();
                this.broadcast.Start(address);
                this.btnListen.Text = "Stop";
            }
        }

        private void Broadcast_ServerFound(IPAddress address)
        {
            if (!this.txtServerAddress.Items.Contains(address))
            {
                this.txtServerAddress.Items.Add(address);
                if (this.txtServerAddress.SelectedIndex < 0)
                    this.txtServerAddress.SelectedIndex = 0;
            }
        }

        private void Connection_Disconnected(ClientConnection connection, User user)
        {
            this.lstPeers.Items.Remove(user);
            this.connections.Remove(user);
            this.manager.RemoveSentUserMessages(user);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            IPAddress address = IPAddress.Parse(this.txtServerAddress.Text);

            ClientConnection connection = new ClientConnection(this, this.txtUserName.Text, new IPEndPoint(address, Global.PORT));
            prepareConnection(connection);
        }

        private void prepareConnection(ClientConnection connection)
        {
            connection.Connected += Connection_Connected;
            connection.MessageRecieved += Connection_MessageRecieved;
            connection.Disconnected += Connection_Disconnected;
        }

        private void lstPeers_SelectedIndexChanged(object sender, EventArgs e)
        {
            User user = (User)lstPeers.SelectedItem;
            if (user != null)
                this.currentUser = user;

            this.lstMessages.Items.Clear();
            this.lstMessages.Items.AddRange(manager.GetUserMessages(user).ToArray());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.server?.Stop();
            ClientConnection[] cons = new ClientConnection[connections.Values.Count];
            connections.Values.CopyTo(cons, 0);
            foreach (var con in cons)
            {
                con.Disconnect();
            }

            broadcast?.Cancel();
            foreach (var receiver in udpReceivers)
            {
                receiver.Cancel();
            }
        }

        private void txtMessage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnSend.PerformClick();
                e.Handled = true;
            }
        }

        private void cmbAddresses_TextChanged(object sender, EventArgs e)
        {
            String text = this.cmbAddresses.Text;
            IPAddress ip;
            this.btnListen.Enabled = IPAddress.TryParse(text, out ip);
        }

        private void txtServerAddress_TextChanged(object sender, EventArgs e)
        {
            String text = this.txtServerAddress.Text;
            IPAddress ip;
            this.btnConnect.Enabled = IPAddress.TryParse(text, out ip);
        }

        private void txtServerAddress_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
