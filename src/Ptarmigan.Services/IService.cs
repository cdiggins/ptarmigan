using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Domo;
using Ptarmigan.Utils;

namespace Ptarmigan.Services
{
    public interface IService : IDisposingNotifier
    {
        IApi Api { get; }
    }
}