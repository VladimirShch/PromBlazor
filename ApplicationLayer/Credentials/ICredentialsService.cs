using System.Threading.Tasks;

namespace Web_Prom.Core.Blazor.ApplicationLayer.Credentials
{
    public interface ICredentialsService
    {
        Task<CredentialsCheckingResult> CheckAsync(string username, string passwordHash);
        Task<bool> ChangePasswordAsync(string username, string currentPasswordHash, string newPasswordHash); // Добавил позже. Может, не в этот интерфейс?
    }
}
