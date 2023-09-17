using System;
using System.Runtime.CompilerServices;

namespace Ptarmigan.Utils
{
    public static class Verifier
    {
        public static void Assert<T>(T input,
            Predicate<T> predicate,
            string message = "",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string fileName = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Assert(predicate(input), () => $"{message}: predicate failed with input {input}", memberName, fileName, lineNumber);
        }

        public static void Assert(bool condition, string message, [CallerMemberName] string memberName = "",
            [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0)
        {
            Assert(condition, () => message, memberName, fileName, lineNumber);
        }

        public static void AssertEquals(object value, object expected, [CallerMemberName] string memberName = "",
            [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0)
        {
            Assert(value?.Equals(expected) == true, () => $"Value {value} not equal to expected {expected}", memberName, fileName, lineNumber);
        }

        public static void AssertNotNull(object obj, string name,
            [CallerMemberName] string memberName = "", [CallerFilePath] string fileName = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Assert(obj != null, $"{name} was null", memberName, fileName, lineNumber);
        }

        public static void Assert(bool condition, Func<string> messageGen = null,  [CallerMemberName] string memberName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0)
        {
            if (!condition)
            {
                var msg = messageGen != null ? $" with message [{messageGen?.Invoke()}]" : "";
                throw new Exception(
                    $"Assertion failed{msg} in member {memberName} at line {lineNumber} in file {fileName}");
            }
        }
    }
}
