using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Editors
{
    // TODO make a base class for these builders
    internal class GradingPresetEditorBuilder : IGradingPresetEditorBuilder
    {
        #region Private Members

        /// <summary>
        /// The version of test system model to use
        /// </summary>
        private int mVersion;

        /// <summary>
        /// Initial object to edit
        /// If null, create a new one
        /// </summary>
        private Implementation.GradingPreset mInitialPreset;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public GradingPresetEditorBuilder()
        {
            NewPreset();
        }

        #endregion

        #region Public Methods

        public IGradingPresetEditor Build()
        {
            GradingPresetEditor editor;
            if (mInitialPreset != null)
                editor = new GradingPresetEditor(mInitialPreset, mVersion);
            else
                editor = new GradingPresetEditor(mVersion);

            editor.Initialize();
            return editor;
        }

        public IGradingPresetEditorBuilder SetInitialPreset(Implementation.GradingPreset preset)
        {
            mInitialPreset = preset;
            mVersion = preset.Version;
            return this;
        }

        public IGradingPresetEditorBuilder NewPreset()
        {
            mInitialPreset = null;
            mVersion = Versions.Highest;
            return this;
        }

        public IGradingPresetEditorBuilder SetVersion(int version)
        {
            // There already is a test to edit and the caller wants to change the version
            if (mInitialPreset != null && mInitialPreset.Version != version)
            {
                throw new NotSupportedException("Changing test version is not supported yet.");
                // Tho it may be one day
            }

            if (Versions.NotInRange(version))
            {
                throw new ArgumentException($"Version must be between highest ({Versions.Highest}) and lowest ({Versions.Lowest}).");
            }
            return this;
        }

        public IGradingPresetEditorBuilder UseNewestVersion()
        {
            return SetVersion(Versions.Highest);
        }

        #endregion
    }
}
