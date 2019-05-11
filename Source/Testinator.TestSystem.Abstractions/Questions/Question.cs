namespace Testinator.TestSystem.Abstractions
{
    /// <summary>
    /// The question which is base object in test system
    /// </summary>
    public class Question
    {
        /// <summary>
        /// The content of this question as <see cref="IContent"/>
        /// So it can be text or image or whatever implements this interface
        /// </summary>
        public IContent Content { get; set; }

        /// <summary>
        /// The scoring of this question as <see cref="IScoring"/>
        /// </summary>
        public IScoring Scoring { get; set; }

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

        // TODO: Answers or however we call them
    }
}
