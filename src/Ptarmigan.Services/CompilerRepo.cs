using System;
using System.Collections.Generic;
using Domo;

namespace Ptarmigan.Services
{
    public class CompilerState
    {
        public string InputDirectory;
        public IReadOnlyList<string> InputFiles = Array.Empty<string>();
        public IReadOnlyList<string> Diagnostics = Array.Empty<string>();
        public string OutputDll;
        public bool Success;
    }

    public class CompilerRepo : SingletonRepository<CompilerState>
    {
    }
}