using System;

namespace Ptarmigan.Utils
{
    public class UserData
    {
        public string UserName { get; set; } = Environment.UserName;

        public static UserData Default => new UserData();
    }
}   