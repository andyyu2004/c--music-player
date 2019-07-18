using System.Threading.Tasks;
using CMusicPlayer.Internal.Types.Functional;

namespace CMusicPlayer.Data.Network
{
    internal interface IHttpService
    {
        Task<Try<string>> GetAsync(string uri);
        Task<Try<string>> PostAsync(string url, string obj);
    }
}