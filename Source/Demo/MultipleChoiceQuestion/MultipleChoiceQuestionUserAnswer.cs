using Testinator.TestSystem.Abstractions;

namespace Demo
{
    public class MultipleChoiceQuestionUserAnswer
    {
        /// <summary>
        /// 0-based index of the answer the user selected
        /// 0-A, 1-B etc...
        /// </summary>
        public int SelectedAnswerIdx { get; set; }
        public int QuestionId { get; set; }
    }
}
