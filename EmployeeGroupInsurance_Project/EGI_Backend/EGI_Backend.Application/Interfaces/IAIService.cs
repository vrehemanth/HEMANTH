using System.Threading.Tasks;

namespace EGI_Backend.Application.Interfaces
{
    public interface IAIService
    {
        Task<string> GenerateResponse(string prompt);
        Task<string> GenerateChatResponse(System.Collections.Generic.List<(string role, string content)> messages);
    }
}
