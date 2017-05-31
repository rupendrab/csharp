using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace TestApplication
{
    class CsvReader
    {
        public string filename { get; set; }
        public string[] headerFields;

        public CsvReader(string filename)
        {
            this.filename = filename;
        }

        public static void printFields(string[] fields)
        {
            int i = 0;
            foreach (string field in fields)
            {
                if (i > 0)
                {
                    Console.Write("\t");
                }
                Console.Write(field.Replace("\t", "\\t").Replace("\n", "\\n").Replace("\r", "\\r"));
                i++;
            }
            Console.WriteLine();
        }

        public void read()
        {
            using (TextFieldParser csvParser = new TextFieldParser(filename))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                int lineNo = 0;
                while (!csvParser.EndOfData)
                {
                    string[] fields = csvParser.ReadFields();
                    if (lineNo == 0)
                    {
                        headerFields = fields;
                        Console.WriteLine("Headers");
                        CsvReader.printFields(headerFields);
                        Console.WriteLine("Data");
                    }
                    else
                    {
                        CsvReader.printFields(fields);
                    }
                    lineNo++;
                }
                Console.WriteLine("Number of Data Lines = {0}", lineNo-1);
            }

        }
    }
}
