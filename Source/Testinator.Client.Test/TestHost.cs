using System;
using System.Collections.Generic;
using System.Timers;
using Testinator.Client.Domain;
using Testinator.Core;

namespace Testinator.Client.Test
{
    /// <summary>
    /// Responsible for hosting a test
    /// </summary>
    public class TestHost : BaseViewModel, ITestHost
    {
        #region Private Members

        private readonly ILogFactory mLogger;
        private readonly IUIManager mUIManager;
        private readonly ApplicationViewModel mApplicationVM;
        private readonly IClientNetwork mClientNetwork;
        private readonly IClientModel mClientModel;
        private readonly IViewModelProvider mVMProvider;

        /// <summary>
        /// Timer to handle cutdown
        /// </summary>
        private Timer mTestTimer = new Timer(1000);

        /// <summary>
        /// Indicates current question staring from 0
        /// </summary>
        private int mCurrentQuestion = 0;

        #endregion

        #region Public Properties

        /// <summary>
        /// The test that is currently hosted
        /// </summary>
        public Testinator.Core.Test CurrentTest { get; private set; }

        /// <summary>
        /// List of all questions in the test
        /// </summary>
        public List<Question> Questions => CurrentTest.Questions;

        /// <summary>
        /// List of every answer given by the user throughout the test
        /// </summary>
        public List<Answer> UserAnswers { get; private set; }

        /// <summary>
        /// The results binary file writer which handles results saving/deleting from local folder
        /// </summary>
        public BinaryWriter ResultFileWriter { get; private set; } = new BinaryWriter(SaveableObjects.Results);

        /// <summary>
        /// Indicates if the test is in progress
        /// </summary>
        public bool IsTestInProgress { get; private set; }

        /// <summary>
        /// A flag indicating if we have any test to show,
        /// to show corresponding content in the WaitingPage
        /// </summary>
        public bool IsTestReceived => CurrentTest != null;

        /// <summary>
        /// Indicated if the test result has been sucesfully sent for this session
        /// </summary>
        public bool IsResultSent { get; private set; }

        /// <summary>
        /// Indicates if the user has completed the test
        /// </summary>
        public bool IsTestCompleted => mCurrentQuestion > Questions.Count;

        /// <summary>
        /// Indicates if fullscreen should be enabled 
        /// </summary>
        public bool IsFullScreenEnabled { get; private set; }

        /// <summary>
        /// Indicates how much time is left
        /// </summary>
        public TimeSpan TimeLeft { get; private set; }

        /// <summary>
        /// Indicates if the server app has allowed user to check his answers just after they finish the test
        /// </summary>
        public bool AreResultsAllowed { get; private set; }

        /// <summary>
        /// Indicates if the applicating is currently showing any of the result pages
        /// </summary>
        public bool IsShowingResultPage { get; private set; }

        /// <summary>
        /// Shows which question is currently shown
        /// </summary>
        public string CurrentQuestionString { get; private set; }

        /// <summary>
        /// The user's score
        /// </summary>
        public int UserScore { get; private set; }

        /// <summary>
        /// The identifier of current test session 
        /// </summary>
        public Guid SessionIdentifier { get; private set; }

        /// <summary>
        /// The user's mark
        /// </summary>
        public Marks UserMark { get; private set; }

        /// <summary>
        /// The order of the questions. Used to match answers to the questions on server side, or something along those lines
        /// </summary>
        public List<int> QuestionsOrder { get; private set; }

        /// <summary>
        /// The viewmodels for the result page, contaning question, user answer and the correct answer
        /// </summary>
        public List<BaseViewModel> QuestionViewModels { get; private set; } = new List<BaseViewModel>();

        #endregion

        #region Public Events

        /// <summary>
        /// Fired when a test is received
        /// </summary>
        public event Action OnTestReceived = () => { };

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestHost(ILogFactory logger,
                        IUIManager uiManager,
                        ApplicationViewModel applicationVM,
                        IClientModel clientModel,
                        IClientNetwork clientNetwork,
                        IViewModelProvider vmProvider)
        {
            // Inject DI services
            mLogger = logger;
            mUIManager = uiManager;
            mApplicationVM = applicationVM;
            mClientModel = clientModel;
            mClientNetwork = clientNetwork;
            mVMProvider = vmProvider;

            mTestTimer.Elapsed += HandleTimer;
        }

        #endregion

        #region Public Helpers

        /// <summary>
        /// Starts the test 
        /// </summary>
        public void StartTest()
        {
            // If there is no test to start or the test has already started or the result page is displayed don't do anything
            if (!IsTestReceived || IsTestInProgress || IsShowingResultPage)
                return;

            if (CurrentTest == null)
                throw new NullReferenceException("Cannot start the test");

            // Indicate that test is starting
            mLogger.Log("Starting test...");

            IsTestInProgress = true;

            // Initialize the answer list so user can add their answers to it
            UserAnswers = new List<Answer>();

            // Start the test timer
            mTestTimer.Start();

            // Enable full screen if required
            if (IsFullScreenEnabled)
                mUIManager.EnableFullscreenMode();

            // Show the first question
            GoNextQuestion();
        }

        /// <summary>
        /// Stops the test forcefully, if asked by the server
        /// </summary>
        public void AbortTest()
        {
            // Stop the test only if it is in progress
            if (!IsTestInProgress)
                return;

            mLogger.Log("Test has been stopped forcefully");

            // Show a message box with info about it
            mUIManager.ShowMessage(new MessageBoxDialogViewModel()
            {
                Title = "Test został zatrzymany!",
                Message = "Test został zatrzymany na polecenie serwera.",
                OkText = "Ok"
            });

            // Reset the test host
            Reset();

            // Return to the main screen
            mApplicationVM.ReturnMainScreen(mClientNetwork.IsConnected);
        }

        /// <summary>
        /// Binds the test to this view model 
        /// </summary>
        /// <param name="test">Test to be hosted</param>
        public void BindTest(Testinator.Core.Test test)
        {
            // Don't do anything in this case
            if (IsTestInProgress || IsShowingResultPage)
                return;

            // Save the test if it is not null
            CurrentTest = test ?? throw new NullReferenceException("Test cannot be null");

            // Set the timeleft to the duration time
            TimeLeft = test.Info.Duration;

            // Randomize question order
            mLogger.Log("Shuffling questions");
            QuestionsOrder = (List<int>)Questions.Shuffle();

            // Indicate that we have received test
            OnTestReceived.Invoke();
        }

        /// <summary>
        /// Resets the test host and clears all the flags and properties
        /// </summary>
        public void Reset()
        {
            mLogger.Log("Reseting test host...");

            ResetQuestionNumber();

            // Stop the timer
            mTestTimer.Stop();

            // Clear all properties
            CurrentTest = null;
            UserAnswers = new List<Answer>();
            UserScore = 0;
            UserMark = default(Marks);
            QuestionViewModels = new List<BaseViewModel>();

            // Clear flags
            IsTestInProgress = false;
            IsShowingResultPage = false;
            IsResultSent = false;
            AreResultsAllowed = true;

            mLogger.Log("Reseting test host done.");
        }

        /// <summary>
        /// Saves the answer for the current question
        /// </summary>
        /// <param name="answer">The answer itself</param>
        public void SaveAnswer(Answer answer)
        {
            // Save the answer
            UserAnswers.Add(answer);

            mLogger.Log($"User answer saved for question nr {CurrentQuestionString}.");

            // Create view data from the results page if it is allowed to be shown
            if (AreResultsAllowed)
            {
                QuestionViewModels.Add(ConvertQuestionToViewModel(Questions[mCurrentQuestion - 1], answer, QuestionViewModels.Count));
            }
        }

        /// <summary>
        /// Goes to the next question, or
        /// shows the end screen if there is no more questions
        /// </summary>
        public void GoNextQuestion()
        {
            // Update question number
            UpdateQuestionNumber();

            // Send the update
            SendUpdate();

            // If last question was the last question, finish the test
            if (mCurrentQuestion > Questions.Count)
            {
                TestFinished();
                return;
            }

            // Indicate that we are going to the next question
            mLogger.Log("Going to the next question");

            // Go to the next question
            // NOTE: Although mCurrentQuestion starts from 0, it has been incremented above so we need to subtract one here
            ShowQuestionPage(CurrentTest.Questions[mCurrentQuestion - 1]);
        }

        /// <summary>
        /// Sets the test startup arguments send by the server
        /// </summary>
        /// <param name="args">The arguments to be set</param>
        public void SetupArguments(TestStartupArgs args)
        {
            AreResultsAllowed = args.IsResultsPageAllowed;
            IsFullScreenEnabled = args.FullScreenMode;
            TimeLeft = CurrentTest.Info.Duration - args.TimerOffset;
        }

        /// <summary>
        /// Compares the both answers and questions lists and calculates user point score based on that
        /// </summary>
        public void CalculateScore()
        {
            // Total point score
            var totalScore = 0;

            mLogger.Log("Calculating user's score...");

            for (var i = 0; i < CurrentTest.Questions.Count; i++)
            {
                // We can stop here as all remaining answers are blank
                if (UserAnswers[i] == null)
                    break;

                totalScore += CurrentTest.Questions[i].CheckAnswer(UserAnswers[i]);
            }

            // Set calculated point score to the property and set the corresponding mark
            UserScore = totalScore;
            UserMark = CurrentTest.Grading.GetMark(UserScore);

            mLogger.Log($"User score calculation finished. Total score: {totalScore}.");
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Attempts to send the results to the server
        /// </summary>
        /// <returns>True if results have been sent successfully; otherwise, false</returns>
        private bool TrySendResult()
        {
            // Send or save the data
            if (mClientNetwork.IsConnected)
            {
                // Create the data package first
                var data = new DataPackage(PackageType.ResultForm)
                {
                    Content = new ResultFormPackage()
                    {
                        Answers = UserAnswers,
                        PointsScored = UserScore,
                        Mark = UserMark,
                        QuestionsOrder = QuestionsOrder,
                    },
                };

                // Send it
                mClientNetwork.SendData(data);
                IsResultSent = true;

                mLogger.Log($"Test results sent to the server");

                return true;
            }
            else
                return false;

        }

        /// <summary>
        /// Saves results to file 
        /// </summary>
        /// <returns>True if operation was successful; otherwise, false</returns>
        private bool TrySaveResultsToFile()
        {
            try
            {
                // Try to write the results to file
                ResultFileWriter.WriteToFile(new ClientTestResults()
                {
                    Answers = UserAnswers,
                    SessionIdentifier = SessionIdentifier,
                    Client = new TestResultsClientModel()
                    {
                        Name = mClientModel.Name,
                        LastName = mClientModel.LastName,
                        MachineName = mClientModel.MachineName,
                        Mark = UserMark,
                        PointsScored = UserScore,
                        QuestionsOrder = QuestionsOrder,
                    },
                    Test = CurrentTest,
                });

                mUIManager.ShowMessage(new MessageBoxDialogViewModel
                {
                    Title = "Wyniki testu w pliku",
                    Message = "Wyniki testu zostały zapisane do pliku, ponieważ połączenie z serwerem zostało utracone.",
                    OkText = "Ok"
                });

                mLogger.Log($"Test results saved to file");

                return true;

            }
            catch (Exception ex)
            {
                mUIManager.ShowMessage(new MessageBoxDialogViewModel
                {
                    Title = "Błąd zapisu",
                    Message = "Nie udało się zapisać ani wysłać wyników testu." +
                                "\nTreść błędu: " + ex.Message,
                    OkText = "Ok"
                });

                mLogger.Log($"Unable to save the results, error message: {ex.Message}");

                return false;
            }

        }

        /// <summary>
        /// Fired when the current test is finished
        /// </summary>
        private void TestFinished()
        {
            CalculateScore();

            // If sending results falied try save them to file
            if (!TrySendResult())
            {
                // If that also falied...
                if (!TrySaveResultsToFile())
                {
                    // TODO: handle that situation 
                }

            }

            // Test is not in progress now
            IsTestInProgress = false;

            // If full screen mode was fired, disable it
            if (IsFullScreenEnabled)
                mUIManager.DisableFullscreenMode();

            // Change page to the result page
            mUIManager.DispatcherThreadAction(() => mApplicationVM.GoToPage(ApplicationPage.ResultOverviewPage));

            // Indicate that we're in the result page
            IsShowingResultPage = true;

            // Dont need the connection in the result page so stop reconnecting if meanwhile connection has been lost
            mClientNetwork.StopReconnecting();
        }

        /// <summary>
        /// Fired when the cutdown reaches 0
        /// </summary>
        private void TimesUp()
        {
            mUIManager.ShowMessage(new MessageBoxDialogViewModel()
            {
                Title = "Koniec czasu",
                Message = "Czas przeznaczony na rozwiązanie testu minął!",
                OkText = "OK",
            });

            TestFinished();
        }

        /// <summary>
        /// Sends the update to the server
        /// </summary>
        private void SendUpdate()
        {
            // Create package
            var data = new DataPackage(PackageType.ReportStatus)
            {
                Content = new StatusPackage()
                {
                    // Send progress the user has made
                    CurrentQuestion = mCurrentQuestion,
                },
            };

            // Send it to the server
            mClientNetwork.SendData(data);

            // Log it
            mLogger.Log("Sending progress package to the server");
        }

        /// <summary>
        /// Resets the question number
        /// </summary>
        private void ResetQuestionNumber()
        {
            mCurrentQuestion = 0;
            CurrentQuestionString = string.Empty;
        }

        /// <summary>
        /// Updates the current question number
        /// </summary>
        private void UpdateQuestionNumber()
        {
            mCurrentQuestion++;
            // Add one because mCurrentQuestion starts from 0
            CurrentQuestionString = $"{mCurrentQuestion} / {Questions.Count}";
        }

        /// <summary>
        /// Handles the cutdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleTimer(object sender, ElapsedEventArgs e)
        {
            // Every second substract one second from time left property
            TimeLeft = TimeLeft.Subtract(new TimeSpan(0, 0, 1));

            // If we reach 0, time has run out and so the test
            if (TimeLeft.TotalSeconds == 0)
            {
                mTestTimer.Stop();
                TimesUp();
            }
        }

        /// <summary>
        /// Changes page to the questions corresponding page
        /// </summary>
        /// <param name="questionToShow"></param>
        private void ShowQuestionPage(Question questionToShow)
        {
            // Based on next question type...
            switch (questionToShow.Type)
            {
                case QuestionType.MultipleChoice:
                    {
                        // Get the view model of a question and pass it as a parameter to new site
                        var questionViewModel = mVMProvider.GetInjectedPageViewModel<QuestionMultipleChoiceViewModel>();
                        questionViewModel.AttachQuestion(questionToShow as MultipleChoiceQuestion);
                        mUIManager.DispatcherThreadAction(() => mApplicationVM.GoToPage(ApplicationPage.QuestionMultipleChoice, questionViewModel));
                        break;
                    }

                case QuestionType.MultipleCheckboxes:
                    {
                        // Get the view model of a question and pass it as a parameter to new site
                        var questionViewModel = mVMProvider.GetInjectedPageViewModel<QuestionMultipleCheckboxesViewModel>();
                        questionViewModel.AttachQuestion(questionToShow as MultipleCheckBoxesQuestion);
                        mUIManager.DispatcherThreadAction(() => mApplicationVM.GoToPage(ApplicationPage.QuestionMultipleCheckboxes, questionViewModel));
                        break;
                    }

                case QuestionType.SingleTextBox:
                    {
                        // Get the view model of a question and pass it as a parameter to new site
                        var questionViewModel = mVMProvider.GetInjectedPageViewModel<QuestionSingleTextBoxViewModel>();
                        questionViewModel.AttachQuestion(questionToShow as SingleTextBoxQuestion);
                        mUIManager.DispatcherThreadAction(() => mApplicationVM.GoToPage(ApplicationPage.QuestionSingleTextBox, questionViewModel));
                        break;
                    }
            }
        }

        /// <summary>
        /// Gets view data for restuls page from a question
        /// </summary>
        /// <param name="question"></param>
        /// <param name="answer"></param>
        /// <param name="Index">Index in the list</param>
        /// <returns></returns>
        private BaseViewModel ConvertQuestionToViewModel(Question question, Answer answer, int Index)
        {
            switch (question.Type)
            {
                case QuestionType.MultipleChoice:
                    {
                        var questionViewModel = mVMProvider.GetInjectedPageViewModel<QuestionMultipleChoiceViewModel>();
                        questionViewModel.UserAnswer = (MultipleChoiceAnswer)answer;
                        questionViewModel.Index = Index;
                        questionViewModel.AttachQuestion(question as MultipleChoiceQuestion, true);
                        return questionViewModel;
                    }

                case QuestionType.MultipleCheckboxes:
                    {
                        var questionViewModel = mVMProvider.GetInjectedPageViewModel<QuestionMultipleCheckboxesViewModel>();
                        questionViewModel.UserAnswer = (MultipleCheckBoxesAnswer)answer;
                        questionViewModel.Index = Index;
                        questionViewModel.AttachQuestion(question as MultipleCheckBoxesQuestion, true);
                        return questionViewModel;
                    }

                case QuestionType.SingleTextBox:
                    {
                        var questionViewModel = mVMProvider.GetInjectedPageViewModel<QuestionSingleTextBoxViewModel>();
                        questionViewModel.UserAnswer = (SingleTextBoxAnswer)answer;
                        questionViewModel.Index = Index;
                        questionViewModel.AttachQuestion(question as SingleTextBoxQuestion, true);
                        return questionViewModel;
                    }
                default:
                    return null;
            }

        }

        #endregion
    }
}
