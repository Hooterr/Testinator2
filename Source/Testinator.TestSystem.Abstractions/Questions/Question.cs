namespace Testinator.TestSystem.Abstractions
{
    /// <summary>
    /// The question which is base object in test system
    /// </summary>
    public abstract class Question
    {
        /// <summary>
        /// The content of this question task as <see cref="IQuestionTask"/>
        /// So it can be text or image or whatever implements this interface
        /// </summary>
        public abstract IQuestionTask Task { get; protected set; }

        /// <summary>
        /// The scoring for this question as <see cref="IEvaluable"/>
        /// </summary>
        public abstract IEvaluable Scoring { get; protected set; }

        public abstract IQuestionAnswer Answer { get; protected set; }

        /// <summary>
        /// The author of this question
        /// NOTE: It's the person who created this question, not necessary the one who owns it
        /// </summary>
        public string Author { get; protected set; }

        /// <summary>
        /// The category of this question
        /// Can contain subcategories
        /// </summary>
        public Category Category { get; protected set; }

        public virtual bool IsValid()
        {
            var isComplete = true;
            isComplete &= Task != null && !Task.IsEmpty();
            isComplete &= Scoring != null && !Scoring.IsWellDefined();
            isComplete &= Answer != null && !Answer.IsWellDefined();
            return isComplete;                
        }
    }
}
