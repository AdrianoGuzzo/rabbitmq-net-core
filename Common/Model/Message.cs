using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Message
    {
        public string Text { get; set; }
        public Message(string text)
        {
            Text = text;
        }
        public Message()
        {

        }
    }
}
