using System;
using System.Collections.Generic;
using Testinator.Core;

namespace Testinator.Client.Domain
{
    public interface ITestHost
    {
        bool AreResultsAllowed { get; }
        string CurrentQuestionString { get; }
        Core.Test CurrentTest { get; }
        bool IsFullScreenEnabled { get; }
        bool IsResultSent { get; }
        bool IsShowingResultPage { get; }
        bool IsTestCompleted { get; }
        bool IsTestInProgress { get; }
        bool IsTestReceived { get; }
        List<Question> Questions { get; }
        List<int> QuestionsOrder { get; }
        List<BaseViewModel> QuestionViewModels { get; }
        BinaryWriter ResultFileWriter { get; }
        Guid SessionIdentifier { get; }
        TimeSpan TimeLeft { get; }
        List<Answer> UserAnswers { get; }
        Marks UserMark { get; }
        int UserScore { get; }

        event Action OnTestReceived;

        void AbortTest();
        void BindTest(Core.Test test);
        void CalculateScore();
        void GoNextQuestion();
        void Reset();
        void SaveAnswer(Answer answer);
        void SetupArguments(TestStartupArgs args);
        void StartTest();
    }
}
