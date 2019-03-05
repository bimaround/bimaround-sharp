
namespace BIMAroundClient.Interfaces
{
    public interface IAuthorize
    {
        string GetToken(string login, string password);
    }
}
