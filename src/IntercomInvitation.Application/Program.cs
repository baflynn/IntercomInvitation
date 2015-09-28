using System;
using System.IO;

namespace IntercomInvitation.Application
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                App.GenerateInviations(Path.GetFullPath(args[0]));
            }
            catch (Exception e)
            {
                Console.WriteLine("There was a problem processing the file");
                Console.WriteLine("Exception is {0}", e.ToString());
            }

            Console.WriteLine("");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}