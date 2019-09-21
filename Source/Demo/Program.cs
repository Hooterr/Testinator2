using Demo.Task;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Testinator.Files;
using Testinator.Server.Domain;
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
            Console.WriteLine(ApplicationDataFolders.Tests.GetFolderName());
            /*var fs = new FileService();

            var finfo = new Testinator.Files.FileInfo()
            {
                Version = 1,
                AbsolutePath = "C:\\Users\\root\\Desktop\\demo\\something.qtn",
                Metadata = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()
                {
                    { "Name", "brzęczyszczykiewicz" },
                    { "Author", "Józef Żdziebełko" },
                    { "Tags", "#there#is#no#way#this#fucking#thing#works" },
                }),
            };

            fs.SaveFile(finfo, new byte[] { 0x01, 0x02, 0x03, 0x04 });
            

            var fi = fs.GetFileInfo("C:\\Users\\root\\Desktop\\demo\\something.qtn");

            
            var passwd = "someTestPassword!@";

            var _iterations = 2;
            var _keySize = 256;

            var _hash = "SHA1";
            var _salt = "aselrias38490a32"; // Random
            var _vector = "8947az34awl34kjq"; // Random

            var vectorBytes = Encoding.ASCII.GetBytes(_vector);
            var saltBytes = Encoding.ASCII.GetBytes(_salt);

            // encrypt
            var _passwordBytes = new PasswordDeriveBytes(passwd, saltBytes, _hash, _iterations);
            Console.WriteLine("Encrypted: " + BitConverter.ToString(_passwordBytes.GetBytes(_keySize / 8)).Replace("-", ""));

            _salt = "fjdl9kfjsdflkjsdgl";
            _vector = "f23fee3ef";
            vectorBytes = Encoding.ASCII.GetBytes(_vector);
            saltBytes = Encoding.ASCII.GetBytes(_salt);

            _passwordBytes = new PasswordDeriveBytes(passwd, saltBytes, _hash, _iterations);
            Console.WriteLine("Decrypted: " + BitConverter.ToString(_passwordBytes.GetBytes(_keySize / 8)).Replace("-", ""));
            */


            /*
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
            */
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
    public static class Cryptography
    {
        #region Settings

        private static int _iterations = 2;
        private static int _keySize = 256;

        private static string _hash = "SHA1";
        private static string _salt = "aselrias38490a32"; // Random
        private static string _vector = "8947az34awl34kjq"; // Random

        #endregion

        public static string Encrypt(string value, string password)
        {
            return Encrypt<AesManaged>(value, password);
        }
        public static string Encrypt<T>(string value, string password)
                where T : SymmetricAlgorithm, new()
        {
            byte[] vectorBytes = Encoding.ASCII.GetBytes(_vector);
            byte[] saltBytes = Encoding.ASCII.GetBytes(_salt);
            byte[] valueBytes = Encoding.ASCII.GetBytes(value);

            byte[] encrypted;
            using (T cipher = new T())
            {
                PasswordDeriveBytes _passwordBytes =
                    new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                cipher.Mode = CipherMode.CBC;

                using (ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
                {
                    using (MemoryStream to = new MemoryStream())
                    {
                        using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                        {
                            writer.Write(valueBytes, 0, valueBytes.Length);
                            writer.FlushFinalBlock();
                            encrypted = to.ToArray();
                        }
                    }
                }
                cipher.Clear();
            }
            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string value, string password)
        {
            return Decrypt<AesManaged>(value, password);
        }
        public static string Decrypt<T>(string value, string password) where T : SymmetricAlgorithm, new()
        {
            byte[] vectorBytes = Encoding.ASCII.GetBytes(_vector);
            byte[] saltBytes = Encoding.ASCII.GetBytes(_salt);
            byte[] valueBytes = Convert.FromBase64String(value);

            byte[] decrypted;
            int decryptedByteCount = 0;

            using (T cipher = new T())
            {
                PasswordDeriveBytes _passwordBytes = new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                cipher.Mode = CipherMode.CBC;

                try
                {
                    using (ICryptoTransform decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes))
                    {
                        using (MemoryStream from = new MemoryStream(valueBytes))
                        {
                            using (CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                            {
                                decrypted = new byte[valueBytes.Length];
                                decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return String.Empty;
                }

                cipher.Clear();
            }
            return Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount);
        }

    }
}
