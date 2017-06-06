using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Tools;
using FantasyBook = TestApplication.Books.Fantasy;
using Newtonsoft.Json;
using Confluent.Kafka.Serialization;
using Confluent.Kafka;

namespace TestApplication
{
	class Program
	{
		// Class Instance Fields
		int number01;

        delegate string NoteFunction();

		static void Main(string[] args)
		{
			int number = 0;
			float floatNumber = 43.9F;
			bool boolean = true;

			number += 1;

			var implicitVariable = false; // Guessed at compile time
			Console.WriteLine(implicitVariable.GetType());

			// implict conversion (when there is no loss of information)
			// floatNumber = number;

			// explicit conversion (needed when there is a loss of information)
			number = (int) floatNumber;
			Console.WriteLine(number);

			// Definite assignment
			int number2 = 100;
			Console.WriteLine(number2);
			Console.WriteLine((new Program()).number01);

			int[] numbers = new int[4];
			Console.WriteLine(numbers[0]);

			float i = (1 + 2) / 6 + 4;
			Console.WriteLine(i);

            Tools.Wrench wrench = new Tools.Wrench();
            Wrench wrench2 = new Wrench();
            FantasyBook fantasy = new FantasyBook();

            Message goodMorning = new Message("Body");
            string pieceOfText = "Good Morning ";
            goodMorning.SetMessage(ref pieceOfText);
            Console.WriteLine(pieceOfText);
            goodMorning.setMessages("Hello", "Hi", "Bye Bye");
            Console.WriteLine(goodMorning.BodyOfText);
            goodMorning.BodyOfText = "Hello";
            Console.WriteLine(goodMorning.BodyOfText);
            Console.WriteLine(goodMorning.BodyOfTextSize);
            goodMorning.BodyOfTextSize = 2;
            Console.WriteLine(goodMorning.BodyOfText);
            Console.WriteLine("-----");
            goodMorning.title = "Title";
            Console.WriteLine(goodMorning.title);
            PostIt postIt = new PostIt();
            PostIt postIt2 = new PostIt();
            goodMorning.BodyOfText = "Rupendra Bandyopadhyay";
            Console.WriteLine(goodMorning[3]);
            goodMorning[1] = (char) ((int) goodMorning[1] + 'A' - 'a');
            Console.WriteLine(goodMorning.BodyOfText);

            // Operator Overloading
            Message message = new Message("Body 1", "Title 1");
            Message message1 = new Message("Body 2", "Title 2");
            message = message + message1;
            Console.WriteLine(message.title + ", " + message.BodyOfText);

            // Inheritance
            PostIt postIt3 = new PostIt();
            postIt3.title = "My PostIT";
            postIt3.BodyOfText = "New message for my post it";
            Console.WriteLine(postIt3.BodyOfText);
            Message m1 = (Message) postIt3 + new Message("\nBye");
            Console.WriteLine(m1.BodyOfText);

            // Csv Reader
            CsvReader csvReader = new CsvReader(@"C:\Users\rupen\Documents\test_01.csv");
            csvReader.read();

            // Polymorphism
            Message messageP1 = new Message("");
            PostIt postItP1 = new PostIt();
            SetMessages(messageP1);
            SetMessages(postItP1);

            messageP1.Lock();
            message1.Unlock();

            Paper paper = new Paper();
            paper.Lock();
            paper.Unlock();

            ILock[] locks = new ILock[2];
            locks[0] = messageP1;
            locks[1] = paper;
            foreach(ILock lok in locks)
            {
                lok.Lock();
                lok.Unlock();
            }

            Console.Write("Calling Lock on ITarget: ");
            ITarget target = messageP1;
            target.Lock();
            UseDelegate();
            UseMulticastDelegate();
            Console.WriteLine("Get JSON");
            Console.WriteLine(GetJSON());
            string json = GetJSON();
            Account account = GetAccountFromJSON(json);
            Console.WriteLine(account);
            Console.WriteLine("Begin Publish to Kafka");
            PublishToKafka("104.196.197.123:9092", "hello-kafka");
            // PublishToKafka("105.196.197.123:9092", "hello-kafka");
        }

        public static void SetMessages(Message message)
        {
            message.setBodyOfText("Polymorphism");
            Console.WriteLine(message.BodyOfText);
        }

        public static void UseDelegate()
        {
            Console.WriteLine("Start UseDelegate");
            NoteFunction write, read;

            write = new NoteFunction(WriteMethod);
            write = WriteMethod;
            read = ReadMethod;
            write();
            read();
            WriteNoteInformation(write);
            WriteNoteInformation(new NoteFunction(CalculateMethod));
            Console.WriteLine("End UseDelegate");
        }

        public static void UseMulticastDelegate()
        {
            Console.WriteLine("Start UseMulticastDelegate");
            NoteFunction multicastDelegate = null;
            multicastDelegate += WriteMethod;
            multicastDelegate += ReadMethod;
            multicastDelegate -= WriteMethod;
            WriteNoteInformation(multicastDelegate);
            string returnString = multicastDelegate();
            Console.WriteLine(returnString);
            Console.WriteLine("End UseMulticastDelegate");
        }

        private static string WriteMethod()
        {
            Console.WriteLine("Writing");
            return "Writing Method";
        }

        private static string ReadMethod()
        {
            Console.WriteLine("Reading");
            return "Reading Method";
        }

        private static string CalculateMethod()
        {
            Console.WriteLine(5 + 5 * 3 / 2);
            return "Calculate Method";
        }

        private static string WriteNoteInformation(NoteFunction writtenNote)
        {
            string returnString = "";

            Console.WriteLine();
            Console.WriteLine("-------------------------------- Note Information ---------------------------------");
            if (writtenNote != null)
            {
                returnString = writtenNote();
            }
            Console.WriteLine("-------------------------------- End Note Information ---------------------------------");
            Console.WriteLine();

            return returnString;
        }

        private static string GetJSON()
        {
            Account account = new Account
            {
                Email = "james@example.com",
                Active = true,
                CreatedDate = new DateTime(2013, 1, 20, 0, 0, 0, DateTimeKind.Utc),
                Roles = new List<string>
                {
                    "User",
                    "Admin"
                }
            };
            string json = JsonConvert.SerializeObject(account);
            return json;
        }

        public static Account GetAccountFromJSON(string json)
        {
            return JsonConvert.DeserializeObject<Account>(json);
        }

        public static void PublishToKafka(string brokerList, string topicName)
        {
            var config = new Dictionary<string, object> { { "bootstrap.servers", brokerList } };

            using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
            {
                Console.WriteLine($"{producer.Name} producing on {topicName}. q to exit.");

                string text;
                while ((text = Console.ReadLine()) != "q")
                {
                    var deliveryReport = producer.ProduceAsync(topicName, null, text);
                    deliveryReport.ContinueWith(task =>
                    {
                        Console.WriteLine($"Partition: {task.Result.Partition}, Offset: {task.Result.Offset}");
                    });
                }

                // Tasks are not waited on synchronously (ContinueWith is not synchronous),
                // so it's possible they may still in progress here.
                producer.Flush(TimeSpan.FromSeconds(10).Milliseconds);
            }
        }
    }
}
