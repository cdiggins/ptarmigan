﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ptarmigan.Utils
{
    public struct Email
    {
        public Email(string value) => Value = IsValid(value) ? value : throw new ArgumentException(value, nameof(value));
        public string Value { get; }
        public static bool IsValid(string email) => email != null && email.Length >= 5 && email.Count(x => x == '@') == 1 && email.Contains(".") && !email.Contains(' ');
        public static implicit operator string(Email email) => email.Value;
        public static implicit operator Email(string value) => new Email(value);
        public static Email Default = "username@example.com";
    }

}
