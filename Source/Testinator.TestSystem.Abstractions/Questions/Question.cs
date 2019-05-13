namespace Testinator.TestSystem.Abstractions
{
    /// <summary>
    /// The question which is base object in test system
    /// </summary>
    public class Question
    {
        /// <summary>
        /// The content of this question task as <see cref="IQuestionTask"/>
        /// So it can be text or image or whatever implements this interface
        /// </summary>
        public IQuestionTask Task { get; set; }

        /// <summary>
        /// The scoring for this question as <see cref="IQuestionScoring"/>
        /// </summary>
        public IQuestionScoring Scoring { get; set; }

        public IQuestionAnswer Answer { get; set; }

        /// <summary>
        /// The author of this question
        /// NOTE: It's the person who created this question, not necessary the one who owns it
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The category of this question
        /// Can contain subcategories
        /// </summary>
        public Category Category { get; set; }
    }
}
