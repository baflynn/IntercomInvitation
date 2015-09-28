using IntercomInvitation.Application.Providers;
using IntercomInvitation.Domain.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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

        public class when_the_file_does_not_exist : JsonFileProviderSpec
        {
            public override void SetUp()
            {
                base.SetUp();

                _mockFileReader.FileExists = false;
            }

            [Test]
            public void it_should_throw_an_JsonFileProviderException()
            {
                Assert.Throws<JsonFileProviderException>(() => _sut.GetCustomerRecords());
            }
        }

        public class when_the_file_is_empty : JsonFileProviderSpec
        {
            public override void SetUp()
            {
                base.SetUp();

                _mockFileReader.FileExists = true;
                _mockFileReader.FileContent = new string[0];
            }

            [Test]
            public void it_should_throw_an_JsonFileProviderException()
            {
                Assert.Throws<JsonFileProviderException>(() => _sut.GetCustomerRecords());
            }
        }

        public class when_the_file_contians_an_empty_line : JsonFileProviderSpec
        {
            public override void SetUp()
            {
                base.SetUp();

                _mockFileReader.FileExists = true;
                _mockFileReader.FileContent = new string[] { "" };
            }

            [Test]
            public void it_should_throw_an_JsonFileProviderException()
            {
                Assert.Throws<JsonFileProviderException>(() => _sut.GetCustomerRecords());
            }
        }

        public class when_the_file_contains_an_invalid_line : JsonFileProviderSpec
        {
            public override void SetUp()
            {
                base.SetUp();

                _mockFileReader.FileExists = true;
                _mockFileReader.FileContent = new string[] { "Invalid Line" };
            }

            [Test]
            public void it_should_throw_an_JsonFileProviderException()
            {
                Assert.Throws<JsonFileProviderException>(() => _sut.GetCustomerRecords());
            }
        }

        public class when_the_file_contains_an_invalid_field : JsonFileProviderSpec
        {
            public override void SetUp()
            {
                base.SetUp();

                _mockFileReader.FileExists = true;
                _mockFileReader.FileContent = new string[] { "{\"lat\": \"53.761389\", \"user_id\": 30, \"name\": \"Nick Enright\", \"longitude\": \" -7.2875\"}" };
            }

            [Test]
            public void it_should_throw_an_JsonFileProviderException()
            {
                Assert.Throws<JsonFileProviderException>(() => _sut.GetCustomerRecords());
            }
        }

        public class when_the_file_contains_valid_lines : JsonFileProviderSpec
        {
            private List<CustomerRecord> _customerRecords = new List<CustomerRecord>();

            public override void SetUp()
            {
                base.SetUp();

                _mockFileReader.FileExists = true;
                _mockFileReader.FileContent = new string[] { "{\"latitude\": \"53.761389\", \"user_id\": 30, \"name\": \"Nick Enright\", \"longitude\": \" -7.2875\"}",
                "{\"latitude\": \"53.761389\", \"user_id\": 31, \"name\": \"David Ahearn\", \"longitude\": \" -7.2875\"}" };

                _customerRecords = _sut.GetCustomerRecords();
            }

            [Test]
            public void it_should_return_the_correct_number_of_customers()
            {
                Assert.AreEqual(2, _customerRecords.Count);
            }

            [Test]
            public void it_should_deserialise_the_line_correctly()
            {
                Assert.AreEqual("Nick Enright", _customerRecords[0].Name);
                Assert.AreEqual(30, _customerRecords[0].UserId);
                Assert.AreEqual(53.761389, _customerRecords[0].Location.Latitude);
                Assert.AreEqual(-7.2875, _customerRecords[0].Location.Longitude);
            }
        }
    }

    internal class MockFileReader : IReadFiles
    {
        public string[] FileContent { get; set; }
        public bool FileExists { get; set; }

        public string[] Read(string filePath)
        {
            return FileContent;
        }

        public bool Exists(string filePath)
        {
            return FileExists;
        }
    }
}