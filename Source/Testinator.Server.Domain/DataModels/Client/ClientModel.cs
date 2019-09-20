﻿using System;
using System.Collections.Generic;
using Testinator.Core;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// Defines the structure of the client connected to the sever
    /// </summary>
    public class ClientModel : Client
    {
        #region Public Properties

        /// <summary>
        /// Indicates if the user can start the test, meaning: is not in the result page but in the waiting for test page
        /// </summary>
        public bool CanStartTest { get; set; } = true;

        /// <summary>
        /// Question number this client is currently solving
        /// </summary>
        public int CurrentQuestion { get; set; }

        /// <summary>
        /// The number of qustions in the test,
        /// makes sense only if the test in progress 
        /// </summary>
        public int QuestionsCount { get; set; }
        
        /// <summary>
        /// The value for the progress bar
        /// </summary>
        public int ProgressBarValue => CurrentQuestion - 1;

        /// <summary>
        /// The percentage value for the progress bar;
        /// </summary>
        public string ProgressBarPercentage => Math.Floor((double)ProgressBarValue / QuestionsCount * 100).ToString() + "%";

        /// <summary>
        /// Indicates if there is any connection problems with this client
        /// </summary>
        public bool HasConnectionProblem { get; set; }

        /// <summary>
        /// Indicates if this client has sent their results to the server
        /// </summary>
        public bool HasResultsBeenReceived { get; set; }

        /// <summary>
        /// The answer given by this user
        /// </summary>
        public List<Answer> Answers { get; set; }

        /// <summary>
        /// Points scored by the user
        /// </summary>
        public int PointsScored { get; set; }

        /// <summary>
        /// The client mark
        /// </summary>
        public Marks Mark { get; set; }

        /// <summary>
        /// The order in which the client answered questions
        /// </summary>
        public List<int> QuestionsOrder { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Resets this client's model properties so it can start a new test
        /// </summary>
        /// <param name="QuestionsCount">The number of question in new test so they progress bar can show correct values</param>
        public void ResetForNewTest(int QuestionsCount)
        {
            CanStartTest = false;
            CurrentQuestion = 0;
            this.QuestionsCount = QuestionsCount;
            HasConnectionProblem = false;
            HasResultsBeenReceived = false;
            Answers = new List<Answer>();
            PointsScored = 0;
            Mark = default(Marks);
        }

        #endregion
    }
}