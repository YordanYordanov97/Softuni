using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WebServer.GameStoreApplication.Models.Validators
{
    public static class EmailValidator
    {
        public static bool IsValid(string email)
        {
            var pattern = (@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Regex regex = new Regex(pattern);
            Match match = regex.Match(email);

            if (match.Success)
            {
                return true;
            }

            return false;
        }
    }
}
