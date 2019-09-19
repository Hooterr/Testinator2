﻿using System;
using Testinator.TestSystem.Implementation;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Default implementation of <see cref="ITestInfoEditor"/>
    /// </summary>
    internal class TestInfoEditor : BaseEditor<TestInfo, ITestInfoEditor>, ITestInfoEditor
    {
        #region Private Members

        private int mNameMaxLen;
        private int mNameMinLen;

        private TimeSpan mTimeLimitMin;
        private TimeSpan mTimeLimitMax;

        #endregion

        #region Public Properties

        public string Name { get; set; }

        public TimeSpan TimeLimit { get; set; }

        public Category Category { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes the editor to create a new object
        /// </summary>
        /// <param name="version">The version of test system to use</param>
        public TestInfoEditor(int version) : base(version) { }

        /// <summary>
        /// Initializes the editor to edit an existing info
        /// </summary>
        /// <param name="originalObj">The object to edit</param>
        /// <param name="version">The version of test system to use</param>
        public TestInfoEditor(TestInfo originalObj, int version) : base(originalObj, version) { }

        #endregion

        #region Public Method

        public override bool Validate()
        {
            var validationPassed = true;

            if (string.IsNullOrEmpty(Name))
            {
                validationPassed = false;
                HandleErrorFor(x => x.Name, "The name must not be empty");
            }
            else if (Name.Length > mNameMaxLen || Name.Length < mNameMinLen)
            {
                validationPassed = false;
                HandleErrorFor(x => x.Name, $"The name must be from within the range of {mNameMinLen} to {mNameMaxLen} characters.");
            }

            if (TimeLimit == null)
            {
                validationPassed = false;
                HandleErrorFor(x => x.TimeLimit, "Time limit cannot be null");
            }
            else if (TimeLimit < mTimeLimitMin || TimeLimit > mTimeLimitMax)
            {
                validationPassed = false;
                HandleErrorFor(x => x.TimeLimit, $"Time limit must be from within the range of {mTimeLimitMin.ToString()} to {mTimeLimitMax.ToString()}.");
            }

            // TODO possibly more to come (?)
            return validationPassed;
        }

        #endregion

        #region Protected Methods

        protected override TestInfo BuildObject()
        {
            TestInfo info;
            if (IsInEditorMode())
                info = OriginalObject;
            else
                info = new TestInfo()
                {
                    CreationDate = DateTime.Now
                };

            info.Name = Name;
            info.TimeLimit = TimeLimit;
            return info;
        }

        protected override void OnInitialize()
        {
            LoadAttributes();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads values from the attributes
        /// </summary>
        private void LoadAttributes()
        {
            var nameConstraints = AttributeHelper.GetPropertyAttribute<TestInfo, string, StringLengthAttribute>
                (x => x.Name, Version);

            mNameMaxLen = nameConstraints.Max;
            mNameMinLen = nameConstraints.Min;

            var timeConstraints = AttributeHelper.GetPropertyAttribute<TestInfo, TimeSpan, TimeSpanLimitAttribute>
                (x => x.TimeLimit, Version);

            mTimeLimitMax = timeConstraints.Max;
            mTimeLimitMin = timeConstraints.Min;
        }

        #endregion
    }
}
