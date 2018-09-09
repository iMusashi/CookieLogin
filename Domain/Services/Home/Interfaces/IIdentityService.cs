using AutomobileCMS.Areas.Home.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using System.Threading.Tasks;

namespace AutomobileCMS.Domain.Services.Home.Interfaces
{
    public interface IIdentityService
    {
        Task LoginAsync(LoginVM loginVM);
        TokenValidationParameters GetTokenValidationParameters();
        string CreateSanitizedJwtAndStoreInCookie(string jwt);
        HttpRequestMessage GetHttpRequestMessage(LoginVM loginVM);
        Task CallService();
    }
}
