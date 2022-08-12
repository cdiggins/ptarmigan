﻿using System;
using System.IO;
using System.Text;

namespace Ptarmigan.Utils
{
    public class CustomTextWriter : TextWriter
    {
        public readonly Action<char> OnWriteChar;
        public override Encoding Encoding => throw new NotImplementedException();
        public CustomTextWriter(Action<char> onWriteChar) => OnWriteChar = onWriteChar;
        public override void Write(char value) => OnWriteChar(value);
    }
}
