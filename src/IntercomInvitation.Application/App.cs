using IntercomInvitation.Application.Providers;
using IntercomInvitation.Application.Writers;
using IntercomInvitation.Domain;
using IntercomInvitation.Domain.Providers;
using IntercomInvitation.Domain.Writers;

namespace IntercomInvitation.Application
{
    public static class App
    {
        public static void GenerateInviations(string filePath)
        {
            IProvideCustomerRecords recordsProvider = new JsonFileProvider(filePath, new FileReader());
            IWriteInvitations invitationWriter = new ConsoleInvitationWriter();

            InvitationGenerator generator = new InvitationGenerator(recordsProvider, invitationWriter);

            generator.Generate(new Domain.Model.TerraLocation(53.3381985, -6.2592576), 100);
        }
    }
}