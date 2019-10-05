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
        /// The test creator initial page with test and grading lists inside
        /// </summary>
        TestCreatorInitial,

        /// <summary>
        /// The test creator grading presets page
        /// </summary>
        TestCreatorGradingPresets,

        /// <summary>
        /// The test creator test info page to fill all basic test data
        /// </summary>
        TestCreatorTestInfo,

        /// <summary>
        /// The test creator questions master page
        /// </summary>
        TestCreatorQuestions,

        /// <summary>
        /// The test creator test grading page
        /// </summary>
        TestCreatorTestGrading,

        /// <summary>
        /// The test creator test options page
        /// </summary>
        TestCreatorTestOptions,

        /// <summary>
        /// The test creator test finalization page
        /// </summary>
        TestCreatorTestFinalize,

        /// <summary>
        /// The screen stream page
        /// </summary>
        ScreenStream,

        /// <summary>
        /// The application's settings page
        /// </summary>
        Settings,

        /// <summary>
        /// Info about the application
        /// </summary>
        About
    }
}
