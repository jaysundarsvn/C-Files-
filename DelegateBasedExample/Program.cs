using System;
using System.IO;

namespace DelegateBasedExample
{
    class Program
    {
        delegate void LogDel(string text, DateTime date_time);

        public static void Main(string[] args)
        {
            Console.WriteLine("Write something as input or get popped ez");
            var name = Console.ReadLine();
            Log log = new Log();

            LogDel LogTextToScreenDel, LogTextToFileDel;

            LogTextToScreenDel = new LogDel(log.LogToText);
            LogTextToFileDel = new LogDel(log.LogTextToFile);

            LogDel MultLogDel = LogTextToFileDel + LogTextToScreenDel;

            MultLogDel(name, DateTime.Now);
        }

        static void LogText(LogDel log_del, string text, DateTime date_time)
        {
            log_del(text, date_time);
        }

        public class Log
        {
            public void LogToText(string text, DateTime date_time)
            {
                Console.WriteLine($"Log to text data {date_time} : {text}");
            }

            public void LogTextToFile(string text, DateTime date_time)
            {
                using StreamWriter writer = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log.txt"), true);
                writer.WriteLine($"Log text to file data {date_time} : {text}");
            }
        }
    }
}
