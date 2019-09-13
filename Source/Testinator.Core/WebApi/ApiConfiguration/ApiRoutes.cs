namespace Testinator.Core
{
    /// <summary>
    /// The relative routes to all API calls in the web
    /// </summary>
    public static class ApiRoutes
    {
        private const string ApiPrefix = "api/";

        public const string AccountPrefixRoute = ApiPrefix + "account/";
        public const string LoginRoute = AccountPrefixRoute + "login";
        public const string RegisterRoute = AccountPrefixRoute + "register";
    }
}
