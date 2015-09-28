namespace IntercomInvitation.Domain.Model
{
    public class CustomerRecord
    {
        public string Name { get; private set; }
        public int UserId { get; private set; }
        public TerraLocation Location { get; private set; }

        public CustomerRecord(string name, int userId, double latitude, double longitude)
        {
            Name = name;
            UserId = userId;
            Location = new TerraLocation(latitude, longitude);
        }

        public override string ToString()
        {
            return string.Format("Name={0}, UserId={1}, Latitude={2}, Longitute={3}", Name, UserId, Location.Latitude, Location.Longitude);
        }
    }
}