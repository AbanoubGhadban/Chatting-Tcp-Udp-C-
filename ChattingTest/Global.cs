using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChattingTest
{
    abstract class Global
    {
        //This string will be added to the end of every message will be sent
        //That make us able to ensure that the recieved message is comlete message
        //Because size of message may be bigger than the buffer size
        //Go to ClientConnection or ServerConnection Classes and find buffer & incompleteMessage varibles
        internal const String MESSAGE_END = "E$$%)_";

        //This is the prefix of User Name
        //The first message that will be sent when connection is established is as the following:
        //USER_NAME_PREFIX + UserName
        //That make every peer get the user name that connect from the other peer
        internal const String USER_NAME_PREFIX = "un3$%:";

        internal const int PORT = 2489;

        public static IPAddress[] getLocalIPAddresses()
        {
            String hostname = Dns.GetHostName();
            List<IPAddress> addresses = new List<IPAddress>();
            foreach (var ip in Dns.GetHostAddresses(hostname))
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    addresses.Add(ip);
            }
            
            return addresses.ToArray();
        }
    }
}
