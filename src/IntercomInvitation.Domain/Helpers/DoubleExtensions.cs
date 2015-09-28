using System;

namespace IntercomInvitation.Domain.Helpers
{
    public static class DoubleExtensions
    {
        public static double ConvertToRadians(this double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}