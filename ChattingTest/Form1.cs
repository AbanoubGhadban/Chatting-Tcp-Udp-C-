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

        private MessageManager manager;
        User currentUser;

        public Form1()
        {
            InitializeComponent();
            this.lstMessages.Items.Clear();
            this.lstPeers.Items.Clear();
            manager = new MessageManager();
            connections = new Dictionary<User, ClientConnection>();

            IPAddress address = Global.getLocalIPAddress();
            this.txtServerAddress.Text = address.ToString();
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
                this.btnListen.Text = "Listen";
            }
            else
            {
                IPAddress address = Global.getLocalIPAddress();
                this.server = new ServerConnection(this, txtUserName.Text, new IPEndPoint(address, Global.PORT));
                this.server.LocalUserName = this.txtUserName.Text;
                this.server.ConnectionAccepted += Server_ConnectionAccepted;
                server.Start();
                this.btnListen.Text = "Stop";
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
        }

        private void txtMessage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnSend.PerformClick();
                e.Handled = true;
            }
        }
    }
}
