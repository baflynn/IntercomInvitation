using IntercomInvitation.Domain.Model;
using IntercomInvitation.Domain.Writers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntercomInvitation.Infrastructure
{
    public class ConsoleInvitationWriter : IWriteInvitations
    {
        public void Write(IEnumerable<CustomerRecord> invitees)
        {
            Console.WriteLine("Inviting {0} customers", invitees.Count());

            foreach (var invitee in invitees)
            {
                Console.WriteLine(invitee.ToString());
            }
        }
    }
}