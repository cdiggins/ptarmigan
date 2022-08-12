using System.Reflection;

namespace Ptarmigan.Utils
{
    public static class AssemblyUtil
    {
        public static AssemblyData ToAssemblyData(this Assembly assembly)
            => new AssemblyData(assembly);
    }
}