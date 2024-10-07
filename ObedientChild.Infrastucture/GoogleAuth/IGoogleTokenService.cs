using ObedientChild.WebApi;
using System.Threading.Tasks;

namespace ObedientChild.Infrastructure.GoogleAuth
{
    public interface IGoogleTokenService
    {
        Task<GoogleAccessTokenResponse> GetAccessTokenAsync(string authorizationCode);
        Task<string> RefreshAccessTokenAsync(string refreshToken);
    }
}