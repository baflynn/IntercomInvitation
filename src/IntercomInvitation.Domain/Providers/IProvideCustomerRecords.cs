using IntercomInvitation.Domain.Model;
using System.Collections.Generic;

namespace IntercomInvitation.Domain.Providers
{
    public interface IProvideCustomerRecords
    {
        List<CustomerRecord> GetCustomerRecords();
    }
}