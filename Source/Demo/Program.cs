using System;
using System.Collections.Generic;
using Testinator.Server.Domain;
using Testinator.Server.Files;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Editors;
using Testinator.TestSystem.Implementation;
using Testinator.TestSystem.Implementation.Questions;

namespace Demo
{
    public class Program
    {

        public static void Main()
        {
            var mTestEditor = AllEditors.TestEditor
                .NewTest()
                .UseNewestVersion()
                .Build();

            var mQuestionEditor = AllEditors.MultipleChoiceQuestion
                .NewQuestion()
                .UseNewestVersion()
                .Build();

            mQuestionEditor.Task.Text.Content = "New super question. What is 2 + 2 * 2?";
            mQuestionEditor.Options.ABCD = new List<string>() { "8", "6", "10", "duzo" };
            mQuestionEditor.Scoring.CorrectAnswerIdx = 1;
            mQuestionEditor.Scoring.MaximumScore = 12;
            var questionBuild = mQuestionEditor.Build();

            Console.WriteLine($"question build success: {questionBuild.Succeeded}");

            mTestEditor.Info.Name = "This is the name of this test. As you can see it's so long that it's almost TLDR.złóęążźźżźłóęęęęę";
            mTestEditor.Info.Description = "This is the description of this test. It's pretty short in fact.";
            mTestEditor.Questions.Add(questionBuild.Result);
            mTestEditor.Questions.Add(questionBuild.Result);
            mTestEditor.Questions.Add(questionBuild.Result);
            mTestEditor.Questions.Add(questionBuild.Result);

            mTestEditor.Info.TimeLimit = TimeSpan.FromMinutes(5);

            mTestEditor.Grading.ContainsPoints = true;
            mTestEditor.Grading.Thresholds = new List<KeyValuePair<int, IGrade>>()
            {
                new KeyValuePair<int, IGrade>(24, new Grade("nzal")),
                new KeyValuePair<int, IGrade>(48, new Grade("zal")),
            };

            var testBuild = mTestEditor.Build();
            Console.WriteLine($"test build success: {testBuild.Succeeded}");

            ITestFileManager files = new TestFileManager(new FileAccessService());
            files.Save(options =>
            {
                options.InApplicationFolder(ApplicationDataFolders.Tests)
                    .WithName("this shit better works");
            }, (Test)testBuild.Result);

            var contexts = files.GetTestContexts(options =>
            {
                options.InApplicationFolder(ApplicationDataFolders.Tests);
            });

            var test = files.Read(options =>
            {
                options.InApplicationFolder(ApplicationDataFolders.Tests)
                    .WithName("this shit better works");
            });

            var editor = AllEditors.MultipleChoiceQuestion
                .SetInitialQuestion(test.Questions[0].GetQuestion() as MultipleChoiceQuestion)
                .Build();
            

            Console.ReadKey();
        }
    }
}
