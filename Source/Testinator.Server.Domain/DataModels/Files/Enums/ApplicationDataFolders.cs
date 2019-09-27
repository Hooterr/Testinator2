namespace Testinator.Server.Domain
{
    /// <summary>
    /// Represents folders in main application data folder
    /// </summary>
    public enum ApplicationDataFolders
    {
        /// <summary>
        /// Folder that contains tests
        /// </summary>
        [FolderName("Tests")]
        Tests,

        /// <summary>
        /// Folder that contains grading presets
        /// </summary>
        [FolderName("GradingPresets")]
        GradingPresets,
    }
}
