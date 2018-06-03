using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WebServer.GameStoreApplication.Models.Validators
{
    public static class PasswordValidator
    {
        public static bool IsValid(string password)
        {
            if (password.Length < 6)
            {
                return false;
            }

            var passwordToChar = password.ToCharArray();

            if (!passwordToChar.Any(x => char.IsDigit(x)))
            {
                return false;
            }

            if (!passwordToChar.Any(x => char.IsLower(x)))
            {
                return false;
            }

            if (!passwordToChar.Any(x => char.IsUpper(x)))
            {
                return false;
            }

            return true;
        }
    }
}
