using System;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Tests;
using Testinator.TestSystem.Implementation;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// Handles reading and saving <see cref="GradingPreset"/>s
    /// </summary>
    public interface IGradingPresetFileManager
    {
        /// <summary>
        /// Gets grading preset context from a file
        /// </summary>
        /// <param name="configureOptions">The action to configure options with</param>
        /// <returns>Grading preset file context</returns>
        GradingPresetFileContext GetGradingPresetContext(Action<GetFileOptions> configureOptions);

        /// <summary>
        /// Gets grading presets contexts of all test in a given directory
        /// </summary>
        /// <param name="configureOptions">The action to configure options with</param>
        /// <returns>Grading presets files contexts</returns>
        GradingPresetFileContext[] GetGradingPresetsContexts(Action<GetFilesFromDirectoryOptions> configureOptions);

        /// <summary>
        /// Saves a grading preset to a file
        /// </summary>
        /// <param name="gradingPreset">The grading preset to save</param>
        /// <param name="fileName">The name of the file</param>
        /// <param name="absoluteFolderPath">Absolute path to the file</param>
        /// <returns>True if successful, otherwise false</returns>
        bool Save(Action<GetFileOptions> configureOptions, IGradingPreset gradingPreset);

        /// <summary>
        /// Read a grading preset from a file
        /// </summary>
        /// <param name="configureOptions">The action to configure options with</param>
        /// <returns>The grading preset that was read from file</returns>
        IGradingPreset Read(Action<GetFileOptions> configureOptions);
    }
}
