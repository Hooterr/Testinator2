﻿using Testinator.TestSystem.Implementation.Questions;

namespace Testinator.TestSystem.Editors
{
    using ABCQuestionEditor = IQuestionEditor<MultipleChoiceQuestion, IMultipleChoiceQuestionOptionsEditor, IMultipleChoiceQuestionScoringEditor>;

    /// <summary>
    /// Provides editor builders for every question type
    /// </summary>
    public static class AllEditors
    {
        /// <summary>
        /// The editor for <see cref="Questions.MultipleChoiceQuestion"/>
        /// </summary>
        public static IEditorBuilder<ABCQuestionEditor, MultipleChoiceQuestion> MultipleChoiceQuestion
            => new EditorBuilder<ABCQuestionEditor, MultipleChoiceQuestion>();

        // TODO create a builder
        public static ITestEditor TestEditor => new TestEditor(1);

        // TODO: Porób tutaj buildery implementacje itp., byleby metody zostaly te same
        public static ITestInfoEditor InfoEditor => new TestInfoEditor(1);
        public static IGradingEditor GradingEditor => new GradingEditor(1);
        public static ITestOptionsEditor OptionsEditor => new TestOptionsEditor(1);
    }
}
