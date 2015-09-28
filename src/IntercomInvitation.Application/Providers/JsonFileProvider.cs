using IntercomInvitation.Domain.Model;
using IntercomInvitation.Domain.Providers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace IntercomInvitation.Application.Providers
{
    public class JsonFileProvider : IProvideCustomerRecords
    {
        private IReadFiles _fileReader;
        private string _filePath;

        public JsonFileProvider(string filePath, IReadFiles fileReader)
        {
            if (fileReader == null)
            {
                throw new ArgumentNullException("fileReader");
            }

            if (filePath == null)
            {
                throw new ArgumentNullException("filePath");
            }

            _filePath = filePath;
            _fileReader = fileReader;
        }

        public List<CustomerRecord> GetCustomerRecords()
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
                    customerRecords.Add(new CustomerRecord((string)item.name, (int)item.user_id, (double)item.latitude, (double)item.longitude));
                }
                catch (Exception e)
                {
                    throw new JsonFileProviderException(e, "Could not deserialise line {0}", jsonString);
                }
            }

            return customerRecords;
        }
    }
}