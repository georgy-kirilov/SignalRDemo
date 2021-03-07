namespace SignalRDemo.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    public class Database
    {
        public bool DoesUserExist(string username, string password)
        {
            Dictionary<string, string> userPasswordPairs = File
                    .ReadAllLines("/Resources/Users.txt")
                    .Select(x => x.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    .ToDictionary(x => x[0], x => x[1]);

            return userPasswordPairs.ContainsKey(username) && userPasswordPairs[username] == password;
        }
    }
}
