#pragma warning disable IDE0003 // Remove qualification

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public List<KeyValuePair<int, IGrade>> Thresholds { get; set; }

        private IGradingStrategy Strategy { get; set; }

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
        public GradingEditor(int version) : base(version) { }

        /// <summary>
        /// Initializes the editor to edit an existing grading
        /// </summary>
        /// <param name="grading">The grading to edit</param>
        /// <param name="version">The version of test system to use</param>
        public GradingEditor(Grading grading, int version) : base(grading, version) { }

        #endregion

        #region Overridden

        protected override void OnInitialize()
        {
            if (IsInEditorMode())
            {
                mMaxPointScore = OriginalObject.MaxPointScore;
                if (OriginalObject.Preset != null)
                {
                    Preset = OriginalObject.Preset;
                    Custom = false;
                    //PointThresholds = Convert

                }
                else
                {
                    ContainsPoints = OriginalObject.Strategy.ContainsPoints;
                    Thresholds = new List<KeyValuePair<int, IGrade>>(OriginalObject.Strategy.Thresholds);
                }
            }
            else
            {
                Custom = true;
                ContainsPoints = true;
                Strategy = null;
                Preset = null;
            }
        }

        public override bool Validate()
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
                    HandleErrorFor(x => x.Preset, "Preset cannot be null if not using custom thresholds.");
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
                Preset = null;
                if(ContainsPoints)
                {
                    strategy = new PointsGradingStrategy()
                    {
                        Thresholds = new ReadOnlyCollection<KeyValuePair<int, IGrade>>(Thresholds),
                    };
                }
                else
                {
                    strategy = new PercentageGradingStrategy()
                    {
                        Thresholds = new ReadOnlyCollection<KeyValuePair<int, IGrade>>(Thresholds),
                        MaxPointScore = mMaxPointScore,
                    };
                }
            }
            else
            {
                // Use preset
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

        private bool ValidateCustomThresholds()
        {
            var passed = true;

            if (Thresholds.Count < 2)
            {
                passed = false;
                HandleErrorFor(x => x.Thresholds, "There must be at least 2 custom thresholds.");
            }
            else
            {
                if (ContainsPoints)
                {
                    if(false == Thresholds.TrueForAll(x => x.Key <= mMaxPointScore))
                    {
                        passed = false;
                        HandleErrorFor(x => x.Thresholds, $"Not a single threshold can exceed the max point score ({mMaxPointScore}).");
                    }
                    
                    if (Thresholds.OrderByDescending(x => x.Key).First().Key != mMaxPointScore)
                    {
                        passed = false;
                        HandleErrorFor(x => x.Thresholds, $"The last threshold's upper limit must be equal to the max point score ({mMaxPointScore})");
                    }
                }
                else
                {
                    if (false == Thresholds.TrueForAll(x => x.Key <= 100))
                    {
                        passed = false;
                        HandleErrorFor(x => x.Thresholds, $"Not a single threshold can exceed 100%.");
                    }

                    if(Thresholds.OrderByDescending(x => x.Key).First().Key != 100)
                    {
                        passed = false;
                        HandleErrorFor(x => x.Thresholds, $"The last threshold's upper limit must be a 100%");
                    }
                }

                if (Thresholds.Select(x => x.Key).Distinct().Count() != Thresholds.Count())
                {
                    passed = false;
                    HandleErrorFor(x => x.Thresholds, $"Found multiple threshold with the same upper limit.");
                }
                if (Thresholds.Select(x => x.Value.Name).Distinct().Count() != Thresholds.Count())
                {
                    passed = false;
                    HandleErrorFor(x => x.Thresholds, $"Found multiple thresholds with the same grade.");
                }
            }

            return passed;
        }

        #endregion
    }
}
