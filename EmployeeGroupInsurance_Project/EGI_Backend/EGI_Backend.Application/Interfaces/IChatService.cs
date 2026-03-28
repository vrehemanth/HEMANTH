using System.Threading.Tasks;

namespace EGI_Backend.Application.Interfaces
{
    public interface IChatService
    {
        Task<string> ProcessMessage(string userId, string role, string message);
    }
}
