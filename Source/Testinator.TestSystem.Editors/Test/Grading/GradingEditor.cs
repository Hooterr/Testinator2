#pragma warning disable IDE0003 // Remove qualification, don't complain about using this.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Implementation;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Default implementation of <see cref="IGradingEditor"/>
    /// </summary>
    internal class GradingEditor : BaseEditor<Grading, IGradingEditor>, IGradingEditor
    {
        #region Editor Properties

        public IGradingPreset Preset { get; set; }

        public List<KeyValuePair<int, IGrade>> CustomThresholds { get; set; }

        public bool Custom { get; set; }

        public bool ContainsPoints { get; set; }

        #endregion

        // Fix this
        internal int mMaxPointScore;

        #region All Constructors

        /// <summary>
        /// Initializes the editor to create a new grading
        /// </summary>
        /// <param name="version">The version of test system to use</param>
        public GradingEditor(int version, IInternalErrorHandler errorHandler) : base(version, errorHandler) { }

        /// <summary>
        /// Initializes the editor to edit an existing grading
        /// </summary>
        /// <param name="grading">The grading to edit</param>
        /// <param name="version">The version of test system to use</param>
        public GradingEditor(Grading grading, int version, IInternalErrorHandler errorHandler) : base(grading, version, errorHandler) { }

        #endregion

        #region Overridden

        protected override void OnInitialize()
        {
            if (IsInEditorMode())
            {
                mMaxPointScore = OriginalObject.MaxPointScore;
                ContainsPoints = OriginalObject.Strategy.ContainsPoints;
                CustomThresholds = new List<KeyValuePair<int, IGrade>>(OriginalObject.Strategy.Thresholds);
                
            }
            else
            {
                Custom = true;
                ContainsPoints = true;
                // Already null, but keep for good measure 
                Preset = null;
            }
        }

        protected override bool Validate()
        {
            var validationPassed = true;

            if (Custom)
            {
                if (!ValidateCustomThresholds())
                    validationPassed = false;
            }
            else
            {
                if(Preset == null)
                {
                    validationPassed = false;
                    mErrorHandlerAdapter.HandleErrorFor(x => x.Preset, "Preset cannot be null if not using custom thresholds.");
                }
            }

            return validationPassed;
        }

        protected override Grading BuildObject()
        {
            Grading result;
            if (IsInEditorMode())
                result = OriginalObject;
            else
                result = new Grading();

            IGradingStrategy strategy;
            if (Custom)
            {
                if(ContainsPoints)
                {
                    strategy = new PointsGradingStrategy()
                    {
                        Thresholds = new ReadOnlyCollection<KeyValuePair<int, IGrade>>(this.CustomThresholds),
                    };
                }
                else
                {
                    strategy = new PercentageGradingStrategy()
                    {
                        Thresholds = new ReadOnlyCollection<KeyValuePair<int, IGrade>>(this.CustomThresholds),
                        MaxPointScore = mMaxPointScore,
                    };
                }
            }
            // Use preset
            else
            {
                strategy = new PercentageGradingStrategy()
                {
                    Thresholds = new ReadOnlyCollection<KeyValuePair<int, IGrade>>(this.CustomThresholds),
                    MaxPointScore = mMaxPointScore,
                };
            }

            result.Strategy = strategy;
            result.MaxPointScore = mMaxPointScore;

            return result;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Validates <see cref="CustomThresholds"/>
        /// </summary>
        /// <returns>True - all good, false - there are some errors</returns>
        private bool ValidateCustomThresholds()
        {
            var passed = true;

            // Sort of a mess but works for now

            if (CustomThresholds.Count < 2)
            {
                passed = false;
                mErrorHandlerAdapter.HandleErrorFor(x => x.CustomThresholds, "There must be at least 2 custom thresholds.");
            }
            else
            {
                if (ContainsPoints)
                {
                    if(false == CustomThresholds.TrueForAll(x => x.Key <= mMaxPointScore))
                    {
                        passed = false;
                        mErrorHandlerAdapter.HandleErrorFor(x => x.CustomThresholds, $"Not a single threshold can exceed the max point score ({mMaxPointScore}).");
                    }
                    
                    if (CustomThresholds.OrderByDescending(x => x.Key).First().Key != mMaxPointScore)
                    {
                        passed = false;
                        mErrorHandlerAdapter.HandleErrorFor(x => x.CustomThresholds, $"The last threshold's upper limit must be equal to the max point score ({mMaxPointScore})");
                    }
                }
                else
                {
                    if (false == CustomThresholds.TrueForAll(x => x.Key <= 100))
                    {
                        passed = false;
                        mErrorHandlerAdapter.HandleErrorFor(x => x.CustomThresholds, $"Not a single threshold can exceed 100%.");
                    }

                    if(CustomThresholds.OrderByDescending(x => x.Key).First().Key != 100)
                    {
                        passed = false;
                        mErrorHandlerAdapter.HandleErrorFor(x => x.CustomThresholds, $"The last threshold's upper limit must be a 100%");
                    }
                }

                if (CustomThresholds.Select(x => x.Key).Distinct().Count() != CustomThresholds.Count())
                {
                    passed = false;
                    mErrorHandlerAdapter.HandleErrorFor(x => x.CustomThresholds, $"Found multiple threshold with the same upper limit.");
                }
                if (CustomThresholds.Select(x => x.Value.Name).Distinct().Count() != CustomThresholds.Count())
                {
                    passed = false;
                    mErrorHandlerAdapter.HandleErrorFor(x => x.CustomThresholds, $"Found multiple thresholds with the same grade.");
                }
            }

            return passed;
        }

        public void OnErrorFor(Expression<Func<IGradingEditor, object>> propertyExpression, Action<string> action)
        {
            mErrorHandlerAdapter.OnErrorFor(propertyExpression, action);
        }

        bool IErrorListener<IGradingEditor>.Validate()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
