using System;
using System.IO;

namespace IntercomInvitation.Infrastructure
{
    public class FileReader : IReadFiles
    {
        public bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        public string[] Read(string filePath)
        {
            return File.ReadAllLines(filePath);
        }
    }
}