using ChattingTest.HelpingClasses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace ChattingTest.Connections
{
    internal delegate void ConnectedEventHandler(User user);
    internal delegate void DisconnectedEventHandler(User user);
    internal delegate void MessageRecievedEventHandler(Message msg);
    internal delegate void MessageSentEventHandler(Message msg);

    enum ConnectionState
    {
        Ready,
        Connecting,
        Connected,
        Failed
    }

    abstract class TcpConnection
    {
        protected Control control;
        protected String localUserName;
        protected TcpConnection(Control control, String localUserName, IPEndPoint endPoint)
        {
            this.RemoteUsersList = new List<User>();
            this.control = control;
            this.State = ConnectionState.Ready;
            this.localUserName = localUserName;
            this.Connect(endPoint);
        }

        public User LocalUser { get; protected set; }

        protected List<User> RemoteUsersList { get; private set; }

        public User[] RemoteUsers { get { return this.RemoteUsersList.ToArray(); } }

        protected void OnConnected(User user)
        {
            this.control.Invoke((MethodInvoker)(() =>
            {
                Connected?.Invoke(user);
            }));
        }

        protected void OnDisconnected(User user)
        {
            this.control.Invoke((MethodInvoker)(() =>
            {
                Disconnected?.Invoke(user);
            }));
        }

        protected void OnMessageReceived(Message msg)
        {
            this.control.Invoke((MethodInvoker)(() =>
            {
                MessageRecieved?.Invoke(msg);
            }));
        }

        protected void OnMessageSent(Message msg)
        {
            this.control.Invoke((MethodInvoker)(() =>
            {
                MessageSent?.Invoke(msg);
            }));
        }

        public ConnectionState State { get; protected set; }
        
        protected abstract void Connect(IPEndPoint endPoint);
        public abstract void Disconnect(User user);
        public abstract Message Send(User receiver, String msg);
        
        public event ConnectedEventHandler Connected;
        public event DisconnectedEventHandler Disconnected;
        public event MessageRecievedEventHandler MessageRecieved;
        public event MessageSentEventHandler MessageSent;
    }
}
