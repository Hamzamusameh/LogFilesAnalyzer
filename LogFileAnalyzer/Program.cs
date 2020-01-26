using System;
using System.Collections.Generic;
using System.IO;

namespace LogFilesReader
{
    class MyPair
    {
        public string key;
        public int value;
        public MyPair(string key, int value)
        {
            this.value = value;
            this.key = key;ghfgh
        }
    }

    class Comparer : IComparer<MyPair>
    {
        public int Compare(MyPair x, MyPair y)
        {
            return y.value.CompareTo(x.value);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string line;
            string[] nth = new string[5] { "first", "second", "third", "fourth", "fifth" };

            HashSet<string> users = new HashSet<string>();
            HashSet<string> UniqueRecords = new HashSet<string>();

            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            List<MyPair> listOfSortedResult = new List<MyPair>();

            Comparer comparer = new Comparer();

            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("D:\\trackFile2015-03-08.log");

                //Read the first line of text
                line = sr.ReadLine();

                while (line != null)
                {
                    //Remove DateTime from string
                    line = line.Substring(25);

                    //Split cleared line into User , Operation
                    var splittedLine = line.Split('/');

                    users.Add(splittedLine[0]);

                    UniqueRecords.Add(line);

                    //Read the next line
                    line = sr.ReadLine();
                }

                //Count how much every operation has been occured
                foreach (string item in UniqueRecords)
                {
                    string operation = item.Split('/')[1];
                    if (dictionary.ContainsKey(operation))
                        dictionary[operation]++;
                    else
                        dictionary.Add(operation, 1);
                }

                //Fill items in dictionary in list to be accessible by index and sorting easily
                foreach (var item in dictionary)
                {
                    MyPair temp = new MyPair(item.Key, item.Value);
                    listOfSortedResult.Add(temp);
                }

                //Sort the list in descending order
                listOfSortedResult.Sort(comparer);

                //Print The Result based on number of operations
                Console.Write("Please Enter Number of operations: ");
                int numberOfOperationNeeded = Convert.ToInt32(Console.ReadLine());
                for (int i = 0; i < numberOfOperationNeeded; i++)
                    Console.WriteLine($"operation '{listOfSortedResult[i].key}' is the most {nth[i]} common and is used by {(listOfSortedResult[i].value / Convert.ToDouble(users.Count)) * 100}% of our users");

                //Close the file
                sr.Close();

                //Remain Console Opened
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
    }
}