namespace WebServer.Server.HTTP
{
    using System.Collections.Concurrent;

    public static class SessionStore
    {
        public const string SessionCookieKey = "User_SID";
        public const string CurrentUserKey = "^%Current_User_Session_Key%^";
        public const string CurrentCartKey = "^%Current_Cart_Session_Key%^";
        public const string CurrentAdminKey= "^%Current_Admin_Session_Key%^";

        private static readonly ConcurrentDictionary<string, HttpSession> sessions =
            new ConcurrentDictionary<string, HttpSession>();

        public static HttpSession Get(string id)
            => sessions.GetOrAdd(id, _ => new HttpSession(id));
    }
}
