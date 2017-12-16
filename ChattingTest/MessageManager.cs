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
            return messages.FindAll((m) =>
            {
                return m.Sender.Equals(user) || m.Reciever.Equals(user);
            });
        }

        public List<Message> GetUserRecievedMessages(User user)
        {
            return messages.FindAll((m) =>
            {
                return m.Reciever.Equals(user);
            });
        }

        public List<Message> GetUserSentMessages(User user)
        {
            return messages.FindAll((m) =>
            {
                return m.Sender.Equals(user);
            });
        }

        public void RemoveSentUserMessages(User user)
        {
            messages.RemoveAll((m) =>
            {
                return m.Sender.Equals(user);
            });
        }

        public void RemoveReceivedUserMessages(User user)
        {
            messages.RemoveAll((m) =>
            {
                return m.Reciever.Equals(user);
            });
        }

        public void RemoveUserMessages(User user)
        {
            messages.RemoveAll((m) =>
            {
                return m.Reciever.Equals(user) || m.Sender.Equals(user);
            });
        }
    }
}
