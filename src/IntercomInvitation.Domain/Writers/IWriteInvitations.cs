using IntercomInvitation.Domain.Model;
using System.Collections.Generic;

namespace IntercomInvitation.Domain.Writers
{
    public interface IWriteInvitations
    {
        void Write(IEnumerable<CustomerRecord> invitees);
    }
}