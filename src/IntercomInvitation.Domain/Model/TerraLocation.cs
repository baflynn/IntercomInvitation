using IntercomInvitation.Domain.Helpers;
using System;

namespace IntercomInvitation.Domain.Model
{
    public class TerraLocation
    {
        private const double TerraRadiusInKm = 6371;

        public double Longitude { get; private set; }
        public double Latitude { get; private set; }

        public TerraLocation(double latitude, double longitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public double CalculateDistanceInKmTo(TerraLocation location)
        {
            double latitude1Rad = Latitude.ConvertToRadians();
            double longitude1Rad = Longitude.ConvertToRadians();
            double latititude2Rad = location.Latitude.ConvertToRadians();
            double longitude2Rad = location.Longitude.ConvertToRadians();

            double logitudeDiff = Math.Abs(longitude1Rad - longitude2Rad);

            double centralAngle =
                Math.Acos(
                    Math.Sin(latititude2Rad) * Math.Sin(latitude1Rad) +
                    Math.Cos(latititude2Rad) * Math.Cos(latitude1Rad) * Math.Cos(logitudeDiff));

            return Math.Round(TerraRadiusInKm * centralAngle, 3, MidpointRounding.AwayFromZero);
        }
    }
}