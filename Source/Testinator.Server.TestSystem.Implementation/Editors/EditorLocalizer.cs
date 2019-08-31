using System;
using System.Linq;
using System.Reflection;

namespace Testinator.Server.TestSystem.Implementation
{
    internal static class EditorLocalizer
    {
        public static Type FindImplementation(Type questionType)
        {
            var implementations 
                = Assembly.GetExecutingAssembly()
                          .GetTypes()
                          .Where(type => type.GetCustomAttributes(typeof(EditorForAttribute), false).Any())
                          .Select(type => new
                          {
                              Type = type,
                              Attribute = (EditorForAttribute)type.GetCustomAttribute(typeof(EditorForAttribute), false)
                          })
                          .Where(x => x.Attribute.QuestionType == questionType);

            if (implementations.Count() == 0)
                throw new NotSupportedException($"Editor for {questionType.Name} is not implemented!\n" +
                    $"Did you forget to put {nameof(EditorForAttribute)} on the class that implements the editor?");

            // This may come in handy when we need to swap implementations at runtime
            if (implementations.Count() > 1)
                throw new AmbiguousMatchException($"Multiple editor implementations for {questionType.Name} were found.");

            return implementations.First().Type;
        }
    }
}
