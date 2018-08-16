using System.Threading.Tasks;

namespace AbitYour.Models
{
    public interface IAbitYourParser
    {
        Task<IResultList> ParseAsync(string url, string userName, string userScore);
    }
}