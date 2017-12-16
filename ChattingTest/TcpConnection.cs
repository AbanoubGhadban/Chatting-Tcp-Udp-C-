using ChattingTest.HelpingClasses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace ChattingTest.Connections
{
    internal delegate void ConnectedEventHandler(ClientConnection connection, User user);
    internal delegate void DisconnectedEventHandler(ClientConnection connection, User user);
    internal delegate void MessageRecievedEventHandler(ClientConnection connection, Message msg);
    internal delegate void MessageSentEventHandler(ClientConnection connection, Message msg);

    enum ConnectionState
    {
        Ready,
        Connecting,
        Connected,
        Closed,
        Failed
    }
}
