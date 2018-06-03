using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.GameStoreApplication.Models.Validators
{
    public static class TitleValidator
    {
        public static bool IsValid(string title)
        {
            if(title.Length<3 || title.Length > 100)
            {
                return false;
            }

            if (!char.IsUpper(title[0]))
            {
                return false;
            }

            return true;
        }
    }
}
