using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication
{
    class Message : ISuperLock, ITarget
    {
        string bodyOfText = "My Message";
        public string title { get; set; } // Auto Implement getter and setter
        protected int id;
        const string COMPANY_NAME = "I360"; // These are implicitly static as well
        readonly string CompanyName = "Company"; // Read Only Fields, Constant, but can change in the constructor of a class

        public Message(string bodyOfText)
        {
            this.bodyOfText = bodyOfText;
        }

        public Message(string bodyOfText, string title) : this(bodyOfText)
        {
            this.title = title;
        }

        // Implement ILock

        // Overloaded Operator

        public static Message operator +(Message x, Message y)
        {
            Message message = new Message(x.BodyOfText + " " + y.BodyOfText, x.title + " " + y.title);
            return message;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            return this == (Message)obj;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Message x, Message y)
        {
            return x.title == y.title && x.BodyOfText == y.BodyOfText;
        }

        public static bool operator !=(Message x, Message y)
        {
            return x.title != y.title || x.BodyOfText != y.BodyOfText;
        }

        // Implement Indexer for a class
        public char this[int index]
        {
            get
            {
                return bodyOfText[index];
            }
            set
            {
                char[] bodyTemp = bodyOfText.ToCharArray();
                bodyTemp[index] = value;
                bodyOfText = new string(bodyTemp);
            }
        }

        public void SetMessage(ref string message)
        {
            message += bodyOfText;
        }

        public void SetMessage(string message, string whatToSet)
        {
            message = whatToSet;
        }

        public void setMessages(params string[] messages)
        {
            foreach (string message in messages)
            {
                bodyOfText += " " + message;
            }
            Console.WriteLine(bodyOfText);
        }

        public string BodyOfText
        {
            get
            {
                return bodyOfText;
            }
            set
            {
                bodyOfText = value;
            }
        }

        public int BodyOfTextSize
        {
            get
            {
                return bodyOfText.Length;
            }
            set
            {
                bodyOfText = bodyOfText.Substring(0, value);
            }
        }

        public string getBodyOfText()
        {
            return bodyOfText;
        }

        // virtual means a subclass may handle this method differently
        public virtual void setBodyOfText(string message)
        {
            bodyOfText = message;
        }

        public void Lock()
        {
            Console.WriteLine("Message is locked");
        }

        void ITarget.Lock()
        {
            Console.WriteLine("Message is target locked");
        }

        public void Unlock()
        {
            Console.WriteLine("Message is unlocked");
        }

        public void SuperLock()
        {
            Console.WriteLine("Message is superlocked");
        }

        public void SuperUnlock()
        {
            Console.WriteLine("Message is superunlocked");
        }
    }
}
