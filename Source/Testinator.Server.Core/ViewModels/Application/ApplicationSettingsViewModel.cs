﻿using System;
using System.ComponentModel;
using Testinator.Core;
using Testinator.Server.Database;

namespace Testinator.Server.Core
{
    /// <summary>
    /// The view model for global server application settings
    /// </summary>
    public class ApplicationSettingsViewModel : BaseViewModel
    {
        #region Private Members

        private readonly ISettingsRepository mSettingsRepository;

        /// <summary>
        /// Index of the language used in this application
        /// 0 - Polish
        /// 1 - English
        /// </summary>
        private int mLanguageIndex = 0;

        #endregion

        #region Private Properties

        /// <summary>
        /// Allows to get the property of this view model by simply calling its name
        /// </summary>
        /// <param name="propertyName">The name of the property to get/set</param>
        private object this[string propertyName]
        {
            get => GetType().GetProperty(propertyName).GetValue(this, null);
            set => GetType().GetProperty(propertyName).SetValue(this, value, null);
        }

        /// <summary>
        /// The config xml file reader which handles config properties loading from local config file
        /// </summary>
        private XmlReader ConfigFileReader { get; set; } = new XmlReader(SaveableObjects.Config);

        /// <summary>
        /// The config xml file writer which handles config properties saving/deleting from local config file
        /// </summary>
        private XmlWriter ConfigFileWriter { get; set; } = new XmlWriter(SaveableObjects.Config);

        #endregion

        #region Public Properties // TODO: Set this in UI

        /// <summary>
        /// The path to the log file of this application
        /// </summary>
        public string LogFilePath { get; set; } = "log.txt";

        /// <summary>
        /// If false, prevent showing informational message boxes in this application
        /// </summary>
        public bool AreInformationMessageBoxesAllowed { get; set; } = true;

        /// <summary>
        /// Indicates if next question after adding previous one in TestEditor
        /// should be the same Type as it was in this previous one
        /// If false - then next question is blank page and user can choose which Type he wants now
        /// </summary>
        public bool IsNextQuestionTypeTheSame { get; set; } = true;

        /// <summary>
        /// Index of the language used in this application
        /// 0 - Polish
        /// 1 - English
        /// </summary>
        public int LanguageIndex
        {
            get => mLanguageIndex;
            set
            {
                // Set new value
                mLanguageIndex = value;

                // Change app's language based on that
                switch(mLanguageIndex)
                {
                    case 1:
                        DI.UI.ChangeLanguage("en-US");
                        break;

                    // 0 or any not found index is default - Polish language
                    default:
                        DI.UI.ChangeLanguage("pl-PL");
                        break;
                }
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ApplicationSettingsViewModel(ISettingsRepository settingsRepository)
        {
            // Inject DI services
            mSettingsRepository = settingsRepository;

            // Load initial settings configuration from database
            InitializeSettings();

            // Hook to property changed event, so everytime settings are being changed, we save it to the database
            PropertyChanged += SettingValueChanged;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes this view model state with values that are currently saved in the database
        /// </summary>
        private void InitializeSettings()
        {
            // Get every setting from database
            var settingList = mSettingsRepository.GetAllSettings();

            // For each one...
            foreach (var setting in settingList)
            {
                try
                {
                    // Save its value to appropriate property
                    this[setting.Name] = Convert.ChangeType(setting.Value, setting.Type);
                }
                // If something fails, that means the setting in database is broken
                // Therefore, default value of a property will be used and future changes will repair database failures
                // So no need to do anything after catching the exception
                catch { }
            }
        }

        /// <summary>
        /// Fired everytime any of this view model's properties get changed
        /// </summary>
        private void SettingValueChanged(object sender, PropertyChangedEventArgs e)
        {
            // Get changed property's name
            var propertyName = e.PropertyName;

            // Get its type and value based on that
            var propertyValue = this[propertyName];
            var propertyType = this[propertyName].GetType();

            // Create new property info
            var propertyInfo = new SettingsPropertyInfo
            {
                Name = propertyName,
                Type = propertyType,
                Value = propertyValue
            };

            // Save it to the database
            mSettingsRepository.SaveSetting(propertyInfo);
        }

        #endregion
    }
}
