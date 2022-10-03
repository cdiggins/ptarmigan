using System.Linq;
using Domo;

namespace Ptarmigan.Services
{
    public static class ApiExtensions
    {
        public static TService GetService<TService>(this IApi api) where TService: IService
            => api.GetServices().OfType<TService>().FirstOrDefault();

        public static TRepo GetRepo<TRepo>(this IApi api) where TRepo: IRepository
            => api.GetRepositories().OfType<TRepo>().FirstOrDefault();
    }
}