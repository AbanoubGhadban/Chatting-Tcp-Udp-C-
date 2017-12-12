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
        private TcpConnection connection;
        private MessageManager manager;

        User currentUser;

        public Form1()
        {
            InitializeComponent();
            this.lstMessages.Items.Clear();
            this.lstPeers.Items.Clear();
            manager = new MessageManager();
        }

        private void Connection_MessageRecieved(Message msg)
        {
            manager.AddMessage(msg);

            if (msg.Reciever.Equals(currentUser) || msg.Sender.Equals(currentUser))
                lstMessages.Items.Add(msg);
        }

        private void Connection_Connected(HelpingClasses.User user)
        {
            this.lstPeers.Items.Add(user);
            this.lstMessages.SelectedItem = user;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Message msg = this.connection.Send(currentUser, this.txtMessage.Text);
            this.lstMessages.Items.Add(msg);
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            IPAddress address = IPAddress.Parse(txtServerAddress.Text);
            connection = new ServerConnection(this, txtUserName.Text, new IPEndPoint(address, Global.PORT));
            connection.Connected += Connection_Connected;
            connection.MessageRecieved += Connection_MessageRecieved;
            connection.Disconnected += Connection_Disconnected;
        }

        private void Connection_Disconnected(User user)
        {
            this.lstPeers.Items.Remove(user);
            MessageBox.Show(user.ToString());
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            IPAddress address = IPAddress.Parse(this.txtServerAddress.Text);

            connection = new ClientConnection(this, this.txtUserName.Text, new IPEndPoint(address, Global.PORT));
            connection.Connected += Connection_Connected;
            connection.MessageRecieved += Connection_MessageRecieved;
            connection.Disconnected += Connection_Disconnected;
        }

        private void lstPeers_SelectedIndexChanged(object sender, EventArgs e)
        {
            User user = (User)lstPeers.SelectedItem;
            if (user != null)
                this.currentUser = user;
        }
    }
}
