using Ptarmigan.Utils;

namespace Ptarmigan.Services
{
    public interface IPlugin : IDisposingNotifier
    {
        string Name { get; }
        void Initialize(IApi api);
    }
}
