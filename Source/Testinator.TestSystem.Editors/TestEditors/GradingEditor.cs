using Testinator.Server.TestSystem.Implementation;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Editors
{
    internal class GradingEditor : BaseEditor<Grading, IGradingEditor>, IGradingEditor
    {
        public IGradingStrategy Strategy { get; internal set; }

        internal int mMaxPointScore;

        public string Name { get; set; }
         
        public GradingEditor(int version) : base(version) { }

        public GradingEditor(Grading grading, int version) : base(grading, version) { }

        protected override Grading BuildObject()
        {
            Grading result = null;
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
    }
}
