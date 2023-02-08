using System.Threading.Tasks;
using Web_Prom.Core.Blazor.ApplicationLayer.Credentials;

namespace Web_Prom.Core.Blazor.DataAccess.Credentials
{
    public class CredentialsService : ICredentialsService
    {
        private readonly Geolog.Contracts.IPass _passService;

        public CredentialsService(Geolog.Contracts.IPass passService)
        {
            _passService = passService;
        }
      
        public async Task<CredentialsCheckingResult> CheckAsync(string username, string passwordHash)
        {
            string[] result = await _passService.GetRegistrationAsync(username, passwordHash);

            return result switch
            {
                _ when result[1] == "False"  => CredentialsCheckingResult.UserNotRegistered,
                _ when result[3] == "False" => CredentialsCheckingResult.WrongPassword,
                _ when result.Length < 5 => CredentialsCheckingResult.AccessDenied,
                _ => CredentialsCheckingResult.OK
            };
        }

        public async Task<bool> ChangePasswordAsync(string username, string curerntPasswordHash, string newPasswordHash)
        {
            return await _passService.ChangePasswordAsync(username, curerntPasswordHash, newPasswordHash);
        }
    }
}
