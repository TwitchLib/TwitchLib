using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Tests.Tests;

namespace TwitchLib.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("TwitchLib - Testing Application");
            Console.WriteLine("Testing chat client parsing...");
            PrintResults(TestChatClient("swiftyspiffy"));

            Console.ReadKey();
        }

        private static List<TestResult> TestChatClient(string channel)
        {
            List<TestResult> results = new List<TestResult>();
            results.Add(new ChatClientTest(ChatClientTest.ChatClientTestType.Connected,
                ChatClientTesting.TestStrings.Connected.CONNECTED_IDENTIFIER, channel));
            results.Add(new ChatClientTest(ChatClientTest.ChatClientTestType.NewSubscriber,
                ChatClientTesting.TestStrings.Subscribers.NEW_SUBSCRIBER_1, channel));
            results.Add(new ChatClientTest(ChatClientTest.ChatClientTestType.NewSubscriber,
                ChatClientTesting.TestStrings.Subscribers.NEW_SUBSCRIBER_2, channel));
            return results;
        }

        private static void PrintResults(List<TestResult> results)
        { 
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(String.Format("{0, 5} {1, 20} {2, 20} {3, 20} {4, 20}", "Test #", "Test Name", "Test Result", "Exception", "Extra Info"));
            /*int currentTest = 1;
            int testsRun = 0;
            int testsPassed = 0;
            int testsFailed = 0;
            foreach(TestResult result in results)
            {
                Console.WriteLine(String.Format("{0, 5} {1, 20} {2, 20} {3, 20} {4, 20}", currentTest, ((ChatClientTest)result).TestType, ((ChatClientTest)result).Successful, ((ChatClientTest)result).FailedException?.ToString() ?? "N/A", ((ChatClientTest)result).ExtraInfo));
                currentTest++;
            }*/
        }
    }
}
