using ChattingTest.HelpingClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace ChattingTest.Connections
{
    class ClientConnection
    {
        private TcpClient client;
        private SocketManager socketManager;

        protected Control control;

        public ClientConnection(Control control, String localUserName, IPEndPoint endPoint) {
            this.control = control;
            this.State = ConnectionState.Ready;
            this.Connect(endPoint.Address, endPoint.Port, localUserName);
        }

        public ClientConnection(Control control, String localUserName, Socket socet)
        {
            this.control = control;
            this.State = ConnectionState.Ready;
            this.prepareSocket(socet, localUserName);
        }

        public ConnectionState State { get; protected set; }

        void Connect(IPAddress address, int port, String localUserName)
        {
            this.State = ConnectionState.Connecting;
            client = new TcpClient();
            client.BeginConnect(address, port, new AsyncCallback(OnConnected), localUserName);
        }

        private void OnConnected(IAsyncResult result)
        {
            client.EndConnect(result);
            String localUserName = result.AsyncState.ToString();

            prepareSocket(client.Client, localUserName);
        }

        void prepareSocket(Socket socket, String localUserName)
        {
            socketManager = new SocketManager(socket, localUserName);
            socketManager.Connected += onConnected;
            socketManager.Disconnected += onDisconnected;
            socketManager.MessageRecieved += onMessageReceived;
            socketManager.MessageSent += onMessageSent;

            String nameMessage = Global.USER_NAME_PREFIX + localUserName;
            this.socketManager.Send(nameMessage, true);
        }

        public void Disconnect()
        {
            try
            {
                if (client != null && client.Connected)
                {
                    client.Close();
                    this.socketManager.Disconnect();
                }
            }
            catch (ObjectDisposedException) { }
        }

        public Message Send(string msg)
        {
            return this.socketManager.Send(msg);
        }

        protected void onConnected(ClientConnection connection, User user)
        {
            this.State = ConnectionState.Connected;
            this.control.Invoke((MethodInvoker)(() =>
            {
                Connected?.Invoke(this, user);
            }));
        }

        protected void onDisconnected(ClientConnection connection, User user)
        {
            this.State = ConnectionState.Closed;
            this.control.Invoke((MethodInvoker)(() =>
            {
                Disconnected?.Invoke(this, user);
            }));
        }

        protected void onMessageReceived(ClientConnection connection, Message msg)
        {
            this.control.Invoke((MethodInvoker)(() =>
            {
                MessageRecieved?.Invoke(this, msg);
            }));
        }

        protected void onMessageSent(ClientConnection connection, Message msg)
        {
            this.control.Invoke((MethodInvoker)(() =>
            {
                MessageSent?.Invoke(this, msg);
            }));
        }

        public event ConnectedEventHandler Connected;
        public event DisconnectedEventHandler Disconnected;
        public event MessageRecievedEventHandler MessageRecieved;
        public event MessageSentEventHandler MessageSent;
    }
}
