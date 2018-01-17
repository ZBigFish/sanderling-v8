using System;

namespace sanderling_v8
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Sanderling-NodeJS Project");
            Console.WriteLine("-------------------------\r\n");

            using (App app = App.initialize(args)) {
                app.start();
            }

            Console.ReadLine();
        }
    }
}

