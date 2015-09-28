using IntercomInvitation.Domain.Model;
using NUnit.Framework;

namespace IntercomInvitation.Domain.Tests.Unit.Model
{
    [TestFixture]
    public class TerraLocationSpec
    {
        public class when_supplied_valid_cordinates
        {
            private TerraLocation _source;

            [SetUp]
            public void SetUp()
            {
                _source = new TerraLocation(53.3472, -6.259);
            }

            [Test]
            [TestCase(37.7749, -122.4194, 8176.890)]
            [TestCase(51.5073, -0.1277, 463.127)]
            [TestCase(-33.8674869, 151.2069, 17214.299)]
            public void it_should_calculate_the_correct_distance(double latitude, double longitude, double expectedDistance)
            {
                var destination = new TerraLocation(latitude, longitude);

                double actualDistance = _source.CalculateDistanceInKmTo(destination);

                Assert.AreEqual(expectedDistance, actualDistance);
            }
        }
    }
}