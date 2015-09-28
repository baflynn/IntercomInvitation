using IntercomInvitation.Domain.Model;
using IntercomInvitation.Domain.Providers;
using IntercomInvitation.Domain.Writers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntercomInvitation.Domain.Tests.Unit
{
    [TestFixture]
    public class InvitationGeneratorSpec
    {
        private InvitationGenerator _sut;
        private MockCustomerRecordsProvider _customerRecordsProvider;
        private StubInvitationsWriter _invitationWriter;

        [SetUp]
        public virtual void SetUp()
        {
            _customerRecordsProvider = new MockCustomerRecordsProvider();
            _invitationWriter = new StubInvitationsWriter();

            _sut = new InvitationGenerator(_customerRecordsProvider, _invitationWriter);
        }

        private List<CustomerRecord> CreateCustomers()
        {
            return new List<CustomerRecord>
                {
                    new CustomerRecord("Name3", 3, -33.8674869, 151.2069),
                    new CustomerRecord("Name2", 2, 51.5073, -0.1277),
                    new CustomerRecord("Name4", 4, -33.8674869, 151.2069),                   
                    new CustomerRecord("Name1", 1, 37.7749, -122.4194)
                                    
                };
        }

        public class when_the_office_location_is_null : InvitationGeneratorSpec
        {
            [Test]
            public void it_should_throw_an_ArgumentNullException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.Generate(null, 100));
            }
        }

        public class when_the_distance_is_less_than_zero : InvitationGeneratorSpec
        {
            [Test]
            public void it_should_throw_an_ArgumentNullException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Generate(new TerraLocation(53.3381985, -6.2592576), -1));
            }
        }

        public class when_no_customers_are_within_the_required_distance : InvitationGeneratorSpec
        {
            public override void SetUp()
            {
                base.SetUp();

                _customerRecordsProvider.CustomerRecords = CreateCustomers();

                _sut.Generate(new TerraLocation(53.3381985, -6.2592576), 100);
            }

            [Test]
            public void it_should_not_invite_the_customers()
            {
                Assert.IsEmpty(_invitationWriter.Invitees);
            }
        }

        public class when_customers_are_within_the_required_distance : InvitationGeneratorSpec
        {
            public override void SetUp()
            {
                base.SetUp();

                _customerRecordsProvider.CustomerRecords = CreateCustomers();

                _sut.Generate(new TerraLocation(53.3381985, -6.2592576), 10000);
            }

            [Test]
            public void it_should_invite_the_correct_number_of_customer()
            {
                Assert.AreEqual(2, _invitationWriter.Invitees.Count());
            }

            [Test]
            public void it_should_invite_the_correct_customers()
            {
                Assert.IsTrue(_invitationWriter.Invitees.Any(c => c.UserId == 1));
                Assert.IsTrue(_invitationWriter.Invitees.Any(c => c.UserId == 2));
            }

            [Test]
            public void it_should_order_the_customers()
            {
                Assert.IsTrue(_invitationWriter.Invitees.First().UserId == 1);
                Assert.IsTrue(_invitationWriter.Invitees.Last().UserId == 2);
            }
        }
    }

    internal class MockCustomerRecordsProvider : IProvideCustomerRecords
    {
        public List<CustomerRecord> CustomerRecords { get; set; }

        public List<CustomerRecord> GetCustomerRecords()
        {
            return CustomerRecords;
        }
    }

    internal class StubInvitationsWriter : IWriteInvitations
    {
        public IEnumerable<CustomerRecord> Invitees { get; private set; }

        public StubInvitationsWriter()
        {
            Invitees = new List<CustomerRecord>();
        }

        public void Write(IEnumerable<CustomerRecord> invitees)
        {
            Invitees = invitees;
        }
    }
}