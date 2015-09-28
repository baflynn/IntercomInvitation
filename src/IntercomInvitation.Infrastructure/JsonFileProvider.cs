using IntercomInvitation.Domain.Model;
using IntercomInvitation.Domain.Providers;
using IntercomInvitation.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace IntercomInvitation.Application
{
    public class JsonFileProvider : IProvideCustomerRecords
    {
        private IReadFiles _fileReader;
        private string _filePath;

        public JsonFileProvider(string filePath, IReadFiles fileReader)
        {
            _filePath = filePath;
            _fileReader = fileReader;
        }

        public List<CustomerRecord> Get()
        {
            if (!_fileReader.Exists(_filePath))
            {
                throw new JsonFileProviderException("No file found at {0}", _filePath);
            }

            string[] fileContents = _fileReader.Read(_filePath);

            if (fileContents.Length <= 0)
            {
                throw new JsonFileProviderException("No content found in file {0}", _filePath);
            }

            List<CustomerRecord> customerRecords = new List<CustomerRecord>();

            foreach (var jsonString in _fileReader.Read(_filePath))
            {
                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    throw new JsonFileProviderException("Found empty line in file {0}", _filePath);
                }

                try
                {
                    dynamic item = JsonConvert.DeserializeObject(jsonString);
                    customerRecords.Add(new CustomerRecord((string)item.name, (int)item.user_id, (double)item.longitude, (double)item.latitude));
                }
                catch (Exception e)
                {
                    throw new JsonFileProviderException(e, "Could not deserialise line {0}", jsonString);
                }
            }

            return customerRecords;
        }
    }

    public class JsonFileProviderException : Exception
    {
        public JsonFileProviderException(string messageFormat, params object[] messageArgs)
            : base(string.Format(messageFormat, messageArgs))
        {
        }

        public JsonFileProviderException(Exception innerException, string messageFormat, params object[] messageArgs)
            : base(string.Format(messageFormat, messageArgs), innerException)
        {
        }
    }
}