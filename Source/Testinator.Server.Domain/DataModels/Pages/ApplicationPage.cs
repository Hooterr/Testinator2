namespace Testinator.Server.Domain
{
    /// <summary>
    /// Every main page in this application as an enum
    /// </summary>
    public enum ApplicationPage
    {
        /// <summary>
        /// No page
        /// </summary>
        None = 0,

        /// <summary>
        /// The initial login page
        /// </summary>
        Login,

        /// <summary>
        /// The home page shown after logging in
        /// </summary>
        Home,

        /// <summary>
        /// The test editor initial page
        /// </summary>
        TestCreatorInitial,

        /// <summary>
        /// The screen stream page
        /// </summary>
        ScreenStream,

        /// <summary>
        /// The settings page
        /// </summary>
        Settings,

        /// <summary>
        /// Info about the application
        /// </summary>
        About
    }
}
