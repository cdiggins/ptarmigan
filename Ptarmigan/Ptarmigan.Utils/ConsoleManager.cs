using System;
using System.Runtime.InteropServices;

namespace Ptarmigan.Utils
{
    public static class ConsoleManager
    {
        private const string Kernel32_DllName = "kernel32.dll";

        [DllImport(Kernel32_DllName)]
        private static extern bool AllocConsole();

        [DllImport(Kernel32_DllName)]
        private static extern bool FreeConsole();

        [DllImport(Kernel32_DllName)]
        private static extern IntPtr GetConsoleWindow();

        public static bool HasConsole 
            => GetConsoleWindow() != IntPtr.Zero;

        public static bool Show()
            => HasConsole || AllocConsole();

        public static bool Hide()
            => !HasConsole || FreeConsole();

        public static bool Toggle()
            => HasConsole ? Hide() : Show();
    }

    public static class WindowManager
    { }

    public static class DebugManager
    {
    }

    public static class AssemblyManager
    { }

}
