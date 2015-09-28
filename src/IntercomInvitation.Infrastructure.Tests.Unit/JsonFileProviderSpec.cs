using IntercomInvitation.Application;
using NUnit.Framework;
using System;

namespace IntercomInvitation.Infrastructure.Tests.Unit
{
    [TestFixture]
    public class JsonFileProviderSpec
    {
        private JsonFileProvider _sut;
        private MockFileReader _mockFileReader;

        [SetUp]
        public virtual void SetUp()
        {
            _mockFileReader = new MockFileReader();
            _sut = new JsonFileProvider("file", _mockFileReader);
        }

        public class when_the_file_contents_is_correct : JsonFileProviderSpec
        {
            private string _fileContents = "";

            public override void SetUp()
            {
                base.SetUp();
            }

            public void it_should_return_the_customer_records()
            {
            }
        }
    }

    internal class MockFileReader : IReadFiles
    {
        private string[] _fileContent;

        public void Set(string[] fileContent)
        {
            _fileContent = fileContent;
        }

        public string[] Read(string filePath)
        {
            return _fileContent;
        }

        public bool Exists(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}