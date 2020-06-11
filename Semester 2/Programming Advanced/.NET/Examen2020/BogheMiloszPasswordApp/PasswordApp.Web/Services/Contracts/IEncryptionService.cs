
namespace PasswordApp.Web.Services.Contracts
{
    public interface IEncryptionService
    {
        string Encrypt(string input, string salt);
        string Decrypt(string input, string salt);
    }
}