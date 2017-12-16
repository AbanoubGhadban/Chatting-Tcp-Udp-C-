using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows.Forms;
using ChattingTest.HelpingClasses;
using System.Net.Sockets;

namespace ChattingTest.Connections
{
    internal delegate void ConnectionAcceptedEventHandler(ClientConnection connection);

    class ServerConnection
    {
        TcpListener listener;
        private Control control;

        public ServerConnection(Control control, String localUserName, IPEndPoint endPoing)
        {
            this.LocalUserName = localUserName;
            this.control = control;
            listener = new TcpListener(endPoing);
        }

        public String LocalUserName { get; set; }

        public void Start()
        {
            listener.Start();
            listener.BeginAcceptSocket(onAccept, null);
        }

        private void onAccept(IAsyncResult result)
        {
            try
            {
                Socket socket = this.listener.EndAcceptSocket(result);

                this.control.Invoke((MethodInvoker)(() =>
                {
                    ConnectionAccepted?.Invoke(new ClientConnection(this.control, this.LocalUserName, socket));
                }));
                listener.BeginAcceptSocket(onAccept, null);
            }
            catch (ObjectDisposedException)
            {

            }
        }

        public void Stop()
        {
            this.listener.Stop();
        }

        public event ConnectionAcceptedEventHandler ConnectionAccepted;
    }
}
