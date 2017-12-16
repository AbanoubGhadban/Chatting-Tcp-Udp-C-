using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ChattingTest.Broadcast
{
    delegate void ServerFoundEventHandler(IPAddress address);

    class UdpBroadcast
    {
        UdpClient client;
        const String BROADCAST_MESSAGE = "ER%%^$#r54t";
        const String CLIENT_MESSAGE = "IU@#))df55.";
        readonly byte[] BROADCAST_BYTES = Encoding.ASCII.GetBytes(BROADCAST_MESSAGE);
        readonly byte[] CLIENT_BYTES = Encoding.ASCII.GetBytes(CLIENT_MESSAGE);

        const int BROADCAST_PORT = 6523;

        public void Start(IPAddress address)
        {
            client = new UdpClient(new IPEndPoint(address, BROADCAST_PORT));
            client.BeginReceive(onReceive, null);
        }

        public void Cancel()
        {
            client?.Close();
        }

        void onReceive(IAsyncResult result)
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, BROADCAST_PORT);
                byte[] recievedBytes = client.EndReceive(result, ref endPoint);

                if (Encoding.ASCII.GetString(recievedBytes).Equals(BROADCAST_MESSAGE))
                {
                    client.Send(CLIENT_BYTES, CLIENT_BYTES.Length, endPoint);
                }
                this.client.BeginReceive(this.onReceive, null);
            }
            catch (ObjectDisposedException)
            {
                Cancel();
            }
        }
    }

    class UdpReceiver
    {
        UdpClient client;
        const String BROADCAST_MESSAGE = "ER%%^$#r54t";
        const String CLIENT_MESSAGE = "IU@#))df55.";
        readonly byte[] BROADCAST_BYTES = Encoding.ASCII.GetBytes(BROADCAST_MESSAGE);
        readonly byte[] CLIENT_BYTES = Encoding.ASCII.GetBytes(CLIENT_MESSAGE);

        const int BROADCAST_PORT = 6523;

        private bool cancelled = false;

        private Control control;

        public UdpReceiver(Control control, IPAddress address)
        {
            this.client = getFreeUdpClient(address);
            this.control = control;
            this.client.BeginReceive(this.onReceive, null);
        }

        public void Start()
        {
            cancelled = false;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, BROADCAST_PORT);

            ThreadPool.QueueUserWorkItem((a) =>
            {
                while (!cancelled)
                {
                    client.Send(BROADCAST_BYTES, BROADCAST_BYTES.Length, endPoint);
                    Thread.Sleep(1000);
                }
            });
        }

        UdpClient getFreeUdpClient(IPAddress address)
        {
            int currentPort = BROADCAST_PORT + 1;
            UdpClient udbClient = null;

            while (true)
            {
                try
                {
                    udbClient = new UdpClient(new IPEndPoint(address, currentPort));
                    break;
                }
                catch (SocketException ex)
                {
                    if (currentPort == BROADCAST_PORT)
                        break;
                    currentPort += 1;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    currentPort = 1025;
                }
            }
            return udbClient;
        }

        public void Cancel()
        {
            this.cancelled = true;
            client?.Close();
        }

        void onReceive(IAsyncResult result)
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, BROADCAST_PORT);
                byte[] recievedBytes = client.EndReceive(result, ref endPoint);

                if (Encoding.ASCII.GetString(recievedBytes).Equals(CLIENT_MESSAGE))
                    onServerFound(endPoint.Address);
                this.client.BeginReceive(this.onReceive, null);
            }
            catch (ObjectDisposedException)
            {
                Cancel();
            }
        }

        void onServerFound(IPAddress address)
        {
            this.control.Invoke((MethodInvoker)(() =>
            {
                ServerFound?.Invoke(address);
            }));
        }

        public event ServerFoundEventHandler ServerFound;
    }
}
