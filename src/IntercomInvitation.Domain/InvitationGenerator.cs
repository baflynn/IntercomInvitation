using IntercomInvitation.Domain.Model;
using IntercomInvitation.Domain.Providers;
using IntercomInvitation.Domain.Writers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntercomInvitation.Domain
{
    public class InvitationGenerator
    {
        private IProvideCustomerRecords _customerRecordsProvider;
        private IWriteInvitations _invitationWriter;

        public InvitationGenerator(IProvideCustomerRecords customerRecordsProvider, IWriteInvitations invitationWriter)
        {
            if (customerRecordsProvider == null)
            {
                throw new ArgumentNullException("customerRecordsProvider");
            }

            if (invitationWriter == null)
            {
                throw new ArgumentNullException("invitationWriter");
            }

            _customerRecordsProvider = customerRecordsProvider;
            _invitationWriter = invitationWriter;
        }

        public void Generate(TerraLocation officeLocation, double distanceFromOfficeInKm)
        {
            if (officeLocation == null)
            {
                throw new ArgumentNullException("officeLocation");
            }

            if (distanceFromOfficeInKm <= 0)
            {
                throw new ArgumentOutOfRangeException("distanceFromOffice", "Value should be greater than zero");
            }

            IEnumerable<CustomerRecord> invitees = SelectCustomersWithinRange(officeLocation, distanceFromOfficeInKm);

            WriteInvitations(invitees);
        }

        private IEnumerable<CustomerRecord> SelectCustomersWithinRange(TerraLocation officeLocation, double distanceFromOfficeInKm)
        {
            Dictionary<int, CustomerRecord> invitees = new Dictionary<int, CustomerRecord>();
            List<CustomerRecord> customerRecords = _customerRecordsProvider.GetCustomerRecords();

            foreach (var customerRecord in customerRecords)
            {
                if (CustomerIsWithinRange(officeLocation, distanceFromOfficeInKm, customerRecord))
                {
                    invitees.Add(customerRecord.UserId, customerRecord);
                }
            }

            return invitees.Values.OrderBy(c=> c.UserId);
        }

        private bool CustomerIsWithinRange(TerraLocation officeLocation, double distanceFromOfficeInKm, CustomerRecord customerRecord)
        {
            double distanceToCustomer = officeLocation.CalculateDistanceInKmTo(customerRecord.Location);

            return distanceToCustomer <= distanceFromOfficeInKm;
        }

        private void WriteInvitations(IEnumerable<CustomerRecord> invitees)
        {
            _invitationWriter.Write(invitees);
        }
    }
}