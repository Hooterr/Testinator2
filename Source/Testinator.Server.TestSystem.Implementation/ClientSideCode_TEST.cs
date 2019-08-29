using System.Collections.Generic;
using System.Linq;
using Testinator.TestSystem.Abstractions.Questions;

namespace Testinator.Server.TestSystem.Implementation
{
    public static class ClientSideCode_TEST
    {
        public static void A()
        {

            var listOfQuestions = new List<IQuestion>();

            // OK, I know what you're thinking. How do I know what editor to pick when I got IQuestion.
            // No need to do a type matching. We will have a editor different page in WPF for every question.
            // In the view model constructor of said page we know what type of question we expect so we can choose an editor, no problem.
            // When we got a list of IQuestion, and user chooses a question to edit, now it gets a bit tricky. 
            // What I would like to do is for every editor page (or view model for that page) put an attribute such as [EditorFor(typeof(MultipleChoiceQuestion))].
            // Then write a simple class that holds a key value pairs of [questionType, editorPage]. That collection would be generated at runtime using reflection.
            // Then we just ask it for the corresponding editor: var editorPageType = EditorLocalizer.Resolve(question) and 
            // optionally throw an exception UnsupportedQuestion or something. 

            var editor = Editors.MultipleChoiceQuestion
                .NewQuestion()
                .UseNewestVersion()
                .Build();

            var TaskErrorMessage = string.Empty;

            editor.Task.Text.OnErrorFor(x => x.Content, (msg) => TaskErrorMessage = msg);

            editor.Task.Text.Content = "";
            editor.Task.Text.Markup = Testinator.TestSystem.Abstractions.Questions.Task.MarkupLanguage.Html;
            editor.Task.Images.AddImage(null);

            var operation = editor.Build();

            if (operation.Succeeded)
            {
                listOfQuestions.Add(operation.Result);
            }
            else
            { 
                // When building fails this list contains all the error messages 
                // that haven't already been handled
                var errorlist = operation.Errors;
            }
        }
    }
}
