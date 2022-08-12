using System;

namespace Ptarmigan.Utils.Roslyn
{
    public static class TestUtils
    {
        static readonly string[] ByteSuffixes = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB

        public static string BytesToString(long byteCount, int numPlacesToRound = 1)
        {
            var bytes = Math.Abs(byteCount);
            var place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            var num = Math.Round(bytes / Math.Pow(1024, place), numPlacesToRound);
            return $"{Math.Sign(byteCount) * num}{ByteSuffixes[place]}";
        }

        public static string ToFormattedString(this TimeSpan ts)
        {
            return $"{ts.Minutes}:{ts.Seconds}.{ts.Milliseconds:D3}";
        }
    }
}
