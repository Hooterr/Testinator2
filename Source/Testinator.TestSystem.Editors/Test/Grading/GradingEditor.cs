using Testinator.TestSystem.Implementation;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Default implementation of <see cref="IGradingEditor"/>
    /// </summary>
    internal class GradingEditor : BaseEditor<Grading, IGradingEditor>, IGradingEditor
    {
        #region Editor Properties

        public string Name { get; set; }

        #endregion

        public IGradingStrategy Strategy { get; internal set; }

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

        protected override Grading BuildObject()
        {
            Grading result;
            if (IsInEditorMode())
                result = OriginalObject;
            else
                result = new Grading();

            // TODO, default for now
            result.Strategy = new Standard6GradesPercentageStrategy(mMaxPointScore);
            result.MaxPointScore = mMaxPointScore;
            result.Name = Name;

            return result;
        }

        #endregion
    }
}
