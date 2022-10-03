using Ptarmigan.Utils;

namespace Ptarmigan.Services
{
    public interface IService : IDisposingNotifier
    {
        IApi Api { get; }
    }
}