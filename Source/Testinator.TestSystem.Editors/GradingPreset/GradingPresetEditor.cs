using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Implementation;

namespace Testinator.TestSystem.Editors
{
    internal class GradingPresetEditor : MasterEditor<IGradingPreset, IGradingPresetEditor>, IGradingPresetEditor
    {
        #region Public Properties

        public string Name { get; set; }

        public List<KeyValuePair<int, IGrade>> Thresholds { get; set; }

        public DateTime CreatedDate { get; private set; }

        public DateTime LastModified { get; private set; } 

        #endregion

        #region All Constructors

        public GradingPresetEditor(int version) : base(version) { }

        public GradingPresetEditor(GradingPreset preset, int version) : base(preset, version) { }

        #endregion
        
        protected override bool Validate()
        {
            var validationPassed = true;

            if (Thresholds.Count < 1)
            {
                validationPassed = false;
                //HandleErrorFor(x => x.Thresholds, "There must be at least 2 thresholds.");

            }
            else
            {
                // There are some identical thresholds
                if (Thresholds.Select(x => x.Key).Distinct().Count() != Thresholds.Count())
                { 
                    validationPassed = false;
                    //HandleErrorFor(x => x.Thresholds, "There cannot be identical threshold.");
                }
                // At least 2 thresholds, and all unique
                else
                {
                    // Last element's upper limit must be 100%
                    if (Thresholds.OrderBy(x => x.Key).Last().Key != 100)
                    {
                        validationPassed = false;
                        //HandleErrorFor(x => x.Thresholds, "The last threshold's upper limit must be 100%.");
                    }
                }

                // Check for identical grades
                if(Thresholds.Select(x => x.Value.Name).Distinct().Count() != Thresholds.Select(x => x.Value.Name).Count())
                {
                    validationPassed = false;
                    //HandleErrorFor(x => x.Thresholds, "Grades must have unique names");
                }

            }
                  
            return validationPassed;
        }

        protected override IGradingPreset BuildObject()
        {
            GradingPreset preset;
            if (IsInEditorMode())
                preset = OriginalObject as GradingPreset;
            else
                preset = new GradingPreset()
                {
                    CreationDate = DateTime.Now,
                };

            preset.Name = Name;
            preset.LastEdited = DateTime.Now;
            var list = new List<KeyValuePair<int, IGrade>>(Thresholds.Count);
            Thresholds
                .OrderBy(x => x.Key)
                .ToList()
                .ForEach(x => list.Add(new KeyValuePair<int, IGrade>(x.Key, x.Value)));

            preset.PercentageThresholds = new ReadOnlyCollection<KeyValuePair<int, IGrade>>(list);
            preset.Version = mVersion;

            return preset;
        }

        protected override void InitializeCreateNewObject()
        {
            base.InitializeCreateNewObject();
            Thresholds = new List<KeyValuePair<int, IGrade>>();
            Name = default;
        }

        protected override void InitializeEditExistingObject()
        {
            base.InitializeEditExistingObject();
            Thresholds = OriginalObject.PercentageThresholds.Select(x => new KeyValuePair<int, IGrade>(x.Key, x.Value)).ToList();
            Name = OriginalObject.Name;
            CreatedDate = OriginalObject.CreationDate;
            LastModified = OriginalObject.LastEdited;
        }

        public void OnErrorFor(Expression<Func<IGradingPresetEditor, object>> propertyExpression, ICollection<string> handler)
        {
            ErrorHandlerAdapter.OnErrorFor(propertyExpression, handler);
        }

        bool IErrorListener<IGradingPresetEditor>.Validate()
        {
            return Validate();
        }
    }
}
