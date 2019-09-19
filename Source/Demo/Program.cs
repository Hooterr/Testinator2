using Demo.Task;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Editors;
using Testinator.TestSystem.Implementation.Questions;
using Testinator.TestSystem.Implementation.Serialization;

namespace Demo
{
    public class Program
    {
        private static IRandomizerString randomizerFullName = RandomizerFactory.GetRandomizer(new FieldOptionsFullName());
        private static IRandomizerString randomizerWords = RandomizerFactory.GetRandomizer(new FieldOptionsText()
        {
            Min = 2,
            Max = 10,
            UseUppercase = true,
            UseLowercase = true,
            UseSpace = false,
            UseSpecial = false,
            UseNullValues = false,
            UseLetter = true,
            UseNumber = false,
        });
        private static Random rng = new Random();

        public static void Main(string[] args)
        {
#if false

            for(var i = 0; i < 10; i++)
            {
                var fileHeader = new FileHeader();
                var questionFileHeader = GetDemoFileHeaderQuestion();
                fileHeader.SetCustomHeader(questionFileHeader);
                var bytes = fileHeader.GetBytes();

                var fs = new FileStream(questionFileHeader.AbsolutePath, FileMode.Create, FileAccess.ReadWrite);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
            }

#endif
            var watch = Stopwatch.StartNew();

            var allQuestions = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\demo", "*.qtn").ToList();

            var questionInfoes = new List<QuestionInfo>();
            allQuestions.ForEach(x => questionInfoes.Add(new QuestionFileReader(x).Read()));
            watch.Stop();

            foreach (var questionInfo in questionInfoes)
            {
                Console.WriteLine($"Author: {questionInfo.Author}");
                Console.WriteLine($"Version: {questionInfo.Version}");
                Console.WriteLine($"Path: {questionInfo.AbsolutePath}");
                Console.WriteLine($"Tags: {(questionInfo.Categories.Length == 0 ? "none" : "#")}{string.Join(" #", questionInfo.Categories)}");
                Console.WriteLine(new string('-', 45));
            }

            Console.WriteLine($"======= Found: {questionInfoes.Count()} question files in {watch.ElapsedMilliseconds.ToString()}ms =======");
            Console.ReadKey();
        }

        public static QuestionFileHeader GetDemoFileHeaderQuestion()
        {
            var cts = new string[rng.Next(5)];
            for (var i = 0; i < cts.Length; i++)
                cts[i] = randomizerWords.Generate();

            var questionFileHeader = new QuestionFileHeader()
            {
                Version = (byte)(rng.Next(10)),
                Categories = cts,
                AbsolutePath = "C:\\Users\\root\\Desktop\\demo\\test"+ rng.Next(999999).ToString() +".qtn",
                Author = randomizerFullName.Generate(),
            };
            return questionFileHeader;
        }

        private static Testinator.TestSystem.Implementation.Questions.MultipleChoiceQuestion GetDemoOriginalQuestion()
        {
            var editor = AllEditors.MultipleChoiceQuestion
                .NewQuestion()
                .Build();
            editor.Task.Text.Content = "Some task content haha";
            editor.Options.SetOptions("options a", "options b", "options c", "option d");
            editor.Scoring.CorrectAnswerIdx = 2;
            editor.Scoring.MaximumScore = 10;
            var operation = editor.Build();
            if (operation.Failed)
            {
                Console.WriteLine(string.Join('\n', operation.Errors));
                Debugger.Break();
            }

            operation.Result.Category = new Category()
            {
                Name = "Programming",
                SubCategory = new Category()
                {
                    Name = "C++",
                    SubCategory = new Category()
                    {
                        Name = "Constructors",
                        SubCategory = new Category()
                        {
                            Name = "Static Constructors"
                        }
                    }
                }
            };

            operation.Result.Author = randomizerFullName.Generate();
            return operation.Result;
        }

        private static MultipleChoiceQuestion GetDemoShortQuestion()
        {
            var question = GetDemoOriginalQuestion();
            var result = new MultipleChoiceQuestion()
            {
                Author = question.Author,
                Category = question.Category,
                Options = new MultipleChoiceQuestionOptions()
                {
                    Options = new System.Collections.Generic.List<string>(question.Options.Options),
                },
                Scoring = new MultipleChoiceQuestionScoring()
                {
                    CorrectAnswerIdx = question.Scoring.CorrectAnswerIdx,
                    MaximumScore = question.Scoring.MaximumScore,
                    Strategy = new AllCorrectScoringStrategy(),
                },
                Task = new QuestionTask()
                {
                    Images = null,
                    Text = new TextContent()
                    {
                        Text = question.Task.Text.Text,
                        Markup = question.Task.Text.Markup,
                    },
                },
                Version = question.Version,
            };
            return result;
        }
    }
}
