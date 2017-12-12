using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows.Forms;
using ChattingTest.HelpingClasses;
using System.Net.Sockets;

namespace ChattingTest.Connections
{
    class ServerConnection : TcpConnection
    {
        TcpListener listener;
        Dictionary<User, Socket> sockets;

        private const int BUFFER_SIZE = 1024;

        //Used as a buffer for Received data
        //We pass it to BeginReceive() which will store Received data in it in Connect function
        //We read data stored in it at OnReceive() Function
        private byte[] buffer = new byte[BUFFER_SIZE];

        //This variable will hold message temporarly If size of message is bigger than the buffer size
        //To see how it work go to OnReceive function
        private string incompleteMessage = "";

        public ServerConnection(Control control, string localUserName, IPEndPoint endPoint) : base(control, localUserName, endPoint)
        {
        }

        protected override void Connect(IPEndPoint endPoint)
        {
            this.State = ConnectionState.Connecting;
            sockets = new Dictionary<User, Socket>();
            this.listener = new TcpListener(getLocalIPAddress(), Global.PORT);
            listener.Start();
            listener.BeginAcceptSocket(onAccepted, null);
        }

        void onAccepted(IAsyncResult result)
        {
            Socket socket = listener.EndAcceptSocket(result);

            String nameMessage = Global.USER_NAME_PREFIX + this.localUserName + Global.MESSAGE_END;
            byte[] nameBytes = Encoding.Default.GetBytes(nameMessage);
            socket.BeginSend(nameBytes, 0, nameBytes.Length, 0, null, null);

            socket.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(OnReceive), socket);
            listener.BeginAcceptSocket(onAccepted, null);
        }

        private void OnReceive(IAsyncResult result)
        {
            Socket socket = (Socket)result.AsyncState;
            int len = 0;
            try
            {
                len = socket.EndReceive(result);
            } 
            catch
            {
                this.Disconnect(this.RemoteUsersList[0]);
                return;
            }
            String msg = Encoding.Default.GetString(buffer, 0, len);

            if (len <= 0)
            {
                this.Disconnect(RemoteUsersList[0]);
                return;
            }

            //We ensure that the Received bytes in buffer are a complete message
            //Because size of message may be bigger than the buffer size
            //Go to Global.MESSAGE_END for more information
            //We test that incompleteMessage.Length == 0 to ensure that we didn't received previous parts of the message before
            if (incompleteMessage.Length == 0 && msg.Length < Global.MESSAGE_END.Length)
                throw new Exception("Invalid message Received");

            //We ensure that the message ended with Global.MESSAGE_END
            msg = this.incompleteMessage + msg;
            if (msg.EndsWith(Global.MESSAGE_END))
            {
                this.incompleteMessage = "";
                ProcessMessage(msg.Substring(0, msg.Length - Global.MESSAGE_END.Length), socket);
            }
            else
            {
                if (len < buffer.Length)
                    throw new Exception("Invalid message Received");

                this.incompleteMessage = msg;
            }
            socket.BeginReceive(buffer, 0, buffer.Length, 0, OnReceive, socket);
        }

        //This function will called when a complete message is received
        //We process the received messages in OnReceive() function
        //Then we connect the received messages to ensure that we received a complete message
        //If message size is bigger that buffer size
        private void ProcessMessage(String msg, Socket socket)
        {
            //Check if The other peer has sent its user name
            //If he didn't send it, We expect that the first message the peer wil send
            //is the user name prefixed by Global.USER_NAME_PREFIX
            if (this.State == ConnectionState.Connecting)
            {
                if (msg.StartsWith(Global.USER_NAME_PREFIX))
                {
                    String userName = msg.Substring(Global.USER_NAME_PREFIX.Length);
                    this.State = ConnectionState.Connected;

                    User user = new User(userName, (IPEndPoint)socket.RemoteEndPoint);
                    this.RemoteUsersList.Add(user);
                    this.sockets.Add(user, socket);
                    User localUser = new User(this.localUserName, (IPEndPoint)socket.LocalEndPoint);
                    this.LocalUser = localUser;
                    base.OnConnected(user);
                }
                else
                {
                    throw new Exception("The other peer didn't send user name");
                }
            }
            else
            {
                //Don't forget to declare variable that represents the local user of app
                this.OnMessageReceived(new Message(this.RemoteUsersList[0], this.LocalUser, msg));
            }
        }

        private IPAddress getLocalIPAddress()
        {
            String hostname = Dns.GetHostName();
			foreach (var ip in Dns.GetHostAddresses(hostname))
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
					return ip;
			}
			return null;
        }

        public override void Disconnect(User user)
        {
            Socket socket = this.sockets[user];
            if (socket == null)
            {
                //throw new Exception("You try to close non-established connection");
            }
            else
            {
                try { socket.Close(); } catch { }
            }
            OnDisconnected(user);
        }

        public override Message Send(User receiver, string msg)
        {
            Socket socket = sockets[receiver];
            byte[] msgBytes = Encoding.Default.GetBytes(msg + Global.MESSAGE_END);

            Message message = new Message(this.LocalUser, this.RemoteUsersList[0], msg);
            socket.BeginSend(msgBytes, 0, msgBytes.Length, 0, onSent, message);
            return message;
        }

        private void onSent(IAsyncResult result)
        {
            Message msg = (Message)result.AsyncState;
            Socket socket = this.sockets[msg.Reciever];
            int len = socket.EndSend(result);

            if (len <= 0)
            {
                throw new Exception("Couldn't send the message");
            }

            OnMessageSent(msg);
        }
    }
}
