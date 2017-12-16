using ChattingTest.HelpingClasses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChattingTest.Connections
{
    class SocketManager
    {
        private const int BUFFER_SIZE = 1024;

        //Used as a buffer for Received data
        //We pass it to BeginReceive() which will store Received data in it in Connect function
        //We read data stored in it at OnReceive() Function
        private byte[] buffer = new byte[BUFFER_SIZE];

        //This variable will hold message temporarly If size of message is bigger than the buffer size
        //To see how it work go to OnReceive function
        private string incompleteMessage = "";

        private Socket socket;
        
        public User LocalUser { get; private set; }
        public User RemoteUser { get; private set; }

        public SocketManager(Socket socket, String localUserName)
        {
            this.socket = socket;
            this.State = ConnectionState.Connecting;
            this.LocalUser = new User(localUserName, (IPEndPoint)socket.RemoteEndPoint);
            socket.BeginReceive(buffer, 0, buffer.Length, 0,onReceive, null);
        }

        void onReceive(IAsyncResult result)
        {
            int len = 0;

            try
            {
                len = socket.EndReceive(result);
            }
            catch
            {
                this.Disconnect();
                return;
            }

            String msg = Encoding.Default.GetString(buffer, 0, len);

            if (len <= 0)
            {
                this.Disconnect();
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
                ProcessMessage(msg.Substring(0, msg.Length - Global.MESSAGE_END.Length));
            }
            else
            {
                if (len < buffer.Length)
                    throw new Exception("Invalid message Received");

                this.incompleteMessage = msg;
            }
            socket.BeginReceive(buffer, 0, buffer.Length, 0, onReceive, null);
        }

        private void ProcessMessage(String msg)
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

                    this.RemoteUser = new User(userName, (IPEndPoint)socket.RemoteEndPoint);
                    Connected?.Invoke(null, this.RemoteUser);
                }
                else
                {
                    throw new Exception("The other peer didn't send user name");
                }
            }
            else
            {
                //Don't forget to declare variable that represents the local user of app
                this.MessageRecieved?.Invoke(null, new Message(this.RemoteUser, this.LocalUser, msg));
            }
        }

        //Handled parameter indicates if the user want MessageSent event be fired
        //When message is sent, User don't want to get notify for example:
        //If the program want to send Internal data that is not messages from the user
        //As when the program starts, The two programs will exhange their users names
        public Message Send(string msg, bool handled = false)
        {
            byte[] msgBytes = Encoding.Default.GetBytes(msg + Global.MESSAGE_END);

            Message message = new Message(this.LocalUser, this.RemoteUser, msg);

            if (handled)
                socket.BeginSend(msgBytes, 0, msgBytes.Length, 0, null, null);
            else
                socket.BeginSend(msgBytes, 0, msgBytes.Length, 0, onSent, message);
            return message;
        }

        private void onSent(IAsyncResult result)
        {
            Message msg = (Message)result.AsyncState;
            int len = socket.EndSend(result);

            if (len <= 0)
            {
                throw new Exception("Couldn't send the message");
            }

            MessageSent?.Invoke(null, msg);
        }

        public ConnectionState State { get; protected set; }

        public void Disconnect()
        {
            if (this.socket != null)
            {
                this.socket.Close();
                this.State = ConnectionState.Closed;
                Disconnected?.Invoke(null, RemoteUser);
            }
        }

        public event ConnectedEventHandler Connected;
        public event DisconnectedEventHandler Disconnected;
        public event MessageRecievedEventHandler MessageRecieved;
        public event MessageSentEventHandler MessageSent;
    }
}
