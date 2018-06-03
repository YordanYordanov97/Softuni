using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Application.Models
{
    public static class Admin
    {
        public static string Username { get; set; }
        public static string Password { get; set; }


        public static bool IsAdmin()
        {
            if(Username=="suAdmin" && Password== "aDmInPw17")
            {
                return true;
            }

            return false;
        }
    }
}
