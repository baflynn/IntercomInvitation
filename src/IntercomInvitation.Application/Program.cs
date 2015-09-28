using System;
using System.IO;

namespace IntercomInvitation.Application
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            App.GenerateInviations(Path.GetFullPath(args[0]));

            Console.WriteLine("");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}