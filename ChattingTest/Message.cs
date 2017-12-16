using ChattingTest.HelpingClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChattingTest
{
    class Message
    {
        public Message (User sender, User reciever, String content)
        {
            this.Sender = sender;
            this.Reciever = reciever;
            this.Content = content;
        }

        public User Sender { get; private set; }
        public User Reciever { get; private set; }
        public String Content { get; private set; }

        public override string ToString()
        {
            return Sender.Name + ": " + this.Content;
        }
    }
}
