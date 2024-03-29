﻿using System.IO;

namespace Ptarmigan.Utils
{
    public readonly struct DirectoryPath
    {
        public string Value { get; }
        public DirectoryPath(string path) => Value = path;
        public override string ToString() => Value;
        public static implicit operator string(DirectoryPath path) => path.Value;
        public static implicit operator DirectoryPath(string path) => new DirectoryPath(path);
        public DirectoryPath RelativeFolder(string folder) => Path.Combine(Value, folder);
        public FilePath RelativeFile(string file) => Path.Combine(Value, file);
    }
}