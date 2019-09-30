#pragma warning disable IDE0003 // Remove qualification, don't complain about using this.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Attributes;
using Testinator.TestSystem.Implementation;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Default implementation of <see cref="IGradingEditor"/>
    /// </summary>
    internal class GradingEditor : NestedEditor<Grading, IGradingEditor>, IGradingEditor
    {
        #region Editor Properties

        public List<KeyValuePair<int, IGrade>> Thresholds { get; set; }

        public bool ContainsPoints { get; set; }

        public int TotalPointScore => mMaxPointScore;

        public int MaxThresholdsCount { get; private set; }

        public int MinThresholdCount { get; private set; }
        public int InitialThresholdCount => 5; // TODO: Get this amount from attributes or wherever you want

        #endregion

        // TODO: Fix this
        internal int mMaxPointScore;

        #region All Constructors

        /// <summary>
        /// Initializes the editor to create a new grading
        /// </summary>
        /// <param name="version">The version of test system to use</param>
        public GradingEditor(int version) : base(version) { }

        /// <summary>
        /// Initializes the editor to edit an existing grading
        /// </summary>
        /// <param name="grading">The grading to edit</param>
        /// <param name="version">The version of test system to use</param>
        public GradingEditor(Grading grading, int version) : base(grading, version) { }

        #endregion

        #region Public Methods

        public void UsePreset(IGradingPreset preset)
        {
            if (preset == null)
                throw new ArgumentNullException(nameof(preset));

            ContainsPoints = false;
            Thresholds = new List<KeyValuePair<int, IGrade>>(preset.PercentageThresholds);
        }

        #endregion

        #region Overridden

        protected override void OnInitialize()
        {
            LoadAttributeValues();
            if (IsInEditorMode())
            {
                mMaxPointScore = OriginalObject.MaxPointScore;
                ContainsPoints = OriginalObject.Strategy.ContainsPoints;
                Thresholds = new List<KeyValuePair<int, IGrade>>(OriginalObject.Strategy.Thresholds);
                
            }
            else
            {
                ContainsPoints = true;
                Thresholds = new List<KeyValuePair<int, IGrade>>();
            }
        }

        protected override bool Validate()
        {
            var validationPassed = true;

            if (!ValidateCustomThresholds())
                    validationPassed = false;
          
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

            if(ContainsPoints)
            {
                strategy = new PointsGradingStrategy()
                {
                    Thresholds = new ReadOnlyCollection<KeyValuePair<int, IGrade>>(this.Thresholds),
                };
            }
            else
            {
                strategy = new PercentageGradingStrategy()
                {
                    Thresholds = new ReadOnlyCollection<KeyValuePair<int, IGrade>>(this.Thresholds),
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
        /// Validates <see cref="Thresholds"/>
        /// </summary>
        /// <returns>True - all good, false - there are some errors</returns>
        private bool ValidateCustomThresholds()
        {
            var passed = true;

            // Sort of a mess but works for now

            if (Thresholds.Count < 2)
            {
                passed = false;
                ErrorHandlerAdapter.HandleErrorFor(x => x.Thresholds, "There must be at least 2 custom thresholds.");
            }
            else
            {
                if (ContainsPoints)
                {
                    if(false == Thresholds.TrueForAll(x => x.Key <= mMaxPointScore))
                    {
                        passed = false;
                        ErrorHandlerAdapter.HandleErrorFor(x => x.Thresholds, $"Not a single threshold can exceed the max point score ({mMaxPointScore}).");
                    }
                    
                    if (Thresholds.OrderByDescending(x => x.Key).First().Key != mMaxPointScore)
                    {
                        passed = false;
                        ErrorHandlerAdapter.HandleErrorFor(x => x.Thresholds, $"The last threshold's upper limit must be equal to the max point score ({mMaxPointScore})");
                    }
                }
                else
                {
                    if (false == Thresholds.TrueForAll(x => x.Key <= 100))
                    {
                        passed = false;
                        ErrorHandlerAdapter.HandleErrorFor(x => x.Thresholds, $"Not a single threshold can exceed 100%.");
                    }

                    if(Thresholds.OrderByDescending(x => x.Key).First().Key != 100)
                    {
                        passed = false;
                        ErrorHandlerAdapter.HandleErrorFor(x => x.Thresholds, $"The last threshold's upper limit must be a 100%");
                    }
                }

                if (Thresholds.Select(x => x.Key).Distinct().Count() != Thresholds.Count())
                {
                    passed = false;
                    ErrorHandlerAdapter.HandleErrorFor(x => x.Thresholds, $"Found multiple threshold with the same upper limit.");
                }
                if (Thresholds.Select(x => x.Value.Name).Distinct().Count() != Thresholds.Count())
                {
                    passed = false;
                    ErrorHandlerAdapter.HandleErrorFor(x => x.Thresholds, $"Found multiple thresholds with the same grade.");
                }
            }

            return passed;
        }

        public void OnErrorFor(Expression<Func<IGradingEditor, object>> propertyExpression, ICollection<string> handler)
        {
            ErrorHandlerAdapter.OnErrorFor(propertyExpression, handler);
        }


        bool IErrorListener<IGradingEditor>.Validate()
        {
            return Validate();
        }

        private void LoadAttributeValues()
        {
            var thresholdsAttr = AttributeHelper.GetPropertyAttribute<Grading, IGradingStrategy, CollectionCountAttribute>(x => x.Strategy, this.mVersion);
            MaxThresholdsCount = thresholdsAttr.Max;
            MinThresholdCount = thresholdsAttr.Min;
        }

        #endregion
    }
}
