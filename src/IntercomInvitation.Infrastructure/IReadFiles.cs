namespace IntercomInvitation.Infrastructure
{
    public interface IReadFiles
    {
        bool Exists(string filePath);

        string[] Read(string filePath);
    }
}