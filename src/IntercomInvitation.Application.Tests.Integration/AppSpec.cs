using NUnit.Framework;
using System.IO;

namespace IntercomInvitation.Application.Tests.Integration
{
    [TestFixture]
    public class AppSpec
    {
        [Test]
        public void Given_a_valid_file_it_should_run_without_any_errors()
        {
            App.GenerateInviations(Path.GetFullPath("customers.txt"));
        }
    }
}