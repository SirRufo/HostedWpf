using System.Threading.Tasks;

namespace HostedWpf.Services
{
    public interface ISampleService
    {
        string GetCurrentDate();
        Task<string> GetSomeDataAsync();
    }
}
