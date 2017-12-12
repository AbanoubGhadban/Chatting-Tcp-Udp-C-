using ChattingTest.HelpingClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChattingTest
{
    class MessageManager
    {
        //Key of dictionary represents the user and value represents messages of user
        private List<Message> messages;

        public MessageManager()
        {
            this.messages = new List<Message>();
        }

        public void AddMessage(Message msg)
        {
            messages.Add(msg);
        }

        public List<Message> GetUserMessages(User user)
        {
            List<Message> list = new List<Message>();
            foreach (var msg in this.messages)
            {
                if (msg.Sender.Equals(user) || msg.Reciever.Equals(user))
                    list.Add(msg);
            }
            return list;
        }

        public List<Message> GetUserRecievedMessages(User user)
        {
            List<Message> list = new List<Message>();
            foreach (var msg in this.messages)
            {
                if (msg.Reciever.Equals(user))
                    list.Add(msg);
            }
            return list;
        }

        public List<Message> GetUserSentMessages(User user)
        {
            List<Message> list = new List<Message>();
            foreach (var msg in this.messages)
            {
                if (msg.Sender.Equals(user))
                    list.Add(msg);
            }
            return list;
        }
    }
}
