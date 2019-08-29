using System;
using System.Linq;
using System.Reflection;

namespace Testinator.Server.TestSystem.Implementation
{
    internal static class EditorImplementationLocalizer
    {
        public static Type FindImplementation(Type questionType)
        {
            var implementations 
                = Assembly.GetExecutingAssembly()
                          .GetTypes()
                          .Where(type => type.GetCustomAttributes(typeof(ConcreteEditorForAttribute), false).Any())
                          .Select(type => new
                          {
                              Type = type,
                              Attribute = (ConcreteEditorForAttribute)type.GetCustomAttribute(typeof(ConcreteEditorForAttribute), false)
                          })
                          .Where(x => x.Attribute.QuestionType == questionType);

            if (implementations.Count() == 0)
                throw new NotSupportedException($"Editor for {questionType.Name} is not implemented!\n" +
                    $"Did you forget to put {nameof(ConcreteEditorForAttribute)} on the class that implements the editor?");

            // This may come in handy when we need to swap implementations at runtime
            if (implementations.Count() > 1)
                throw new AmbiguousMatchException($"Multiple editor implementations for {questionType.Name} were found.");

            return implementations.First().Type;
        }
    }
}
