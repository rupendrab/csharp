using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication
{
    class PostIt : Message
    {
        public PostIt() : base ("")
        {
            Console.WriteLine("PostIt created");
        }

        public PostIt(String bodyOfText) : base(bodyOfText)
        {
        }

        public PostIt(int id) : base("")
        {
            this.id = id;
        }

        static PostIt()
        {
            Console.WriteLine("Static Constructor Call");
        }

        public override void setBodyOfText(string message)
        {
            base.setBodyOfText(message);
            BodyOfText = "Post It: " + BodyOfText;
        }

    }
}
