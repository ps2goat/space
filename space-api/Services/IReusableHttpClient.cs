using System.Threading.Tasks;

namespace SpaceApi.Services
{
    public interface IReusableHttpClient
    {
        Task<T> GetAsync<T>(string requestUri);
    }
}