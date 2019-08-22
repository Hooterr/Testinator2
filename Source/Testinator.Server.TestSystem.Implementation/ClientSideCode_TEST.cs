using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Questions;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation
{
    public static class ClientSideCode_TEST
    {
        public static void A()
        {

            var listOfQuestions = new List<Question>();
            var editor = Editors.MultipleChoiceQuestion
                .NewQuestion()
                .UseNewestVersion()
                .Build();
            //editor.Task.Images.

        }
    }
}
