﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ptarmigan.Utils
{
    public static class StringUtil
    {

        public static string IfEmpty(this string self, string other)
            => string.IsNullOrWhiteSpace(self) ? other : self;

        public static string ElidedSubstring(this string self, int start, int length, int max)
            => (length > max) ? self.Substring(start, max) + "..." : self.Substring(start, length);


        public static string ToUtf8(this byte[] bytes)
            => Encoding.UTF8.GetString(bytes);

        public static string ToAscii(this byte[] bytes)
            => Encoding.ASCII.GetString(bytes);

        public static byte[] ToBytesUtf8(this string s)
            => Encoding.UTF8.GetBytes(s);

        public static byte[] ToBytesAscii(this string s)
            => Encoding.ASCII.GetBytes(s);

        public static string ToHex(this byte[] bytes, bool upperCase = false)
            => string.Join("", bytes.Select(b => b.ToString(upperCase ? "X2" : "x2")));

        public static string Base64ToHex(this string base64)
            => Convert.FromBase64String(base64).ToHex();

        public static string ToBase64(this byte[] bytes)
            => Convert.ToBase64String(bytes);
        public static string[] SplitAtNull(this string s)
            => s.Split('\0');

        public static string[] SplitWhitespace(this string value)
            => value.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

        public static string JoinEllided<T>(string separator, IEnumerable<T> values, int count = 5)
        {
            var tmp = values.Take(count).ToList();
            var prefix = string.Join(separator, tmp);
            if (tmp.Count < count)
                return prefix;
            var last = values.Last();
            return $"{prefix} ... {last}";
        }

        public static string JoinWithNull(this IEnumerable<string> strings)
            => string.Join("\0", strings);

        public static string JoinDistinct(this IEnumerable<string> strings, string delim = ";")
            => string.Join(delim, strings.Distinct().OrderBy(x => x));

        public static string Drop(this string s, int n)
            => s.Substring(0, Math.Max(0, s.Length - n));


        public static string JoinStrings<T1, T2>(this IDictionary<T1, T2> self, string sep = ";")
            => self.Select(kv => $"{kv.Key}={kv.Value}").JoinStrings(sep);

        public static string JoinStrings<T>(this IEnumerable<T> self, string sep = ";")
            => string.Join(sep, self);


        /// <summary>
        /// Remove starting and ending quotes.
        /// </summary>
        public static string StripQuotes(this string s)
            => s.Length >= 2 && s[0] == '"' && s[s.Length - 1] == '"' ? s.Substring(1, s.Length - 2) : s;

        /// <summary>
        /// Creates a string using the given quote delimiters. If endQuote is null, the beginQuote is used at the end as well.
        /// </summary>
        public static string Quote(this string s, string beginQuote = "\"", string endQuote = null)
            => $"{beginQuote}{s}{endQuote ?? beginQuote}";


        public static string ToIdentifier(this string self)
            => string.IsNullOrEmpty(self) ? "_" : self.ReplaceNonAlphaNumeric("_");

        public static string ReplaceNonAlphaNumeric(this string self, string replace)
            => Regex.Replace(self, "[^a-zA-Z0-9]", replace);

        public static string ToBitConverterLowerInvariant(this byte[] bytes)
            => BitConverter.ToString(bytes).Replace("-", string.Empty).ToLowerInvariant();    
    }
}