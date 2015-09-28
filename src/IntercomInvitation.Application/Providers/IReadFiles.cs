namespace IntercomInvitation.Application.Providers
{
    public interface IReadFiles
    {
        bool Exists(string filePath);

        string[] Read(string filePath);
    }
}