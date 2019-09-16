using System;
using System.Linq;
using System.Reflection;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Helper class to find editor implementations
    /// </summary>
    internal static class EditorLocalizer
    {
        /// <summary>
        /// Finds implementation of question editor for the given question type
        /// </summary>
        /// <param name="questionType">The question type to find the editor for</param>
        /// <returns>The type of editor implementation for the given question type</returns>
        public static Type FindImplementation(Type questionType)
        {
            Type[] types;

            // Try to get all the types from the assembly
            try
            {
                types = Assembly.GetExecutingAssembly().GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                // If there were some types that were not loaded correctly, we don't really care at this point
                // Editor implementations are IN this assembly, but the GetTypes() call seems to search the dependencies as well, which we know 
                // don't contain our editor

                // Use the types there were loaded correctly 
                types = ex.Types;
            }

            // Find all the type that have the editor for attribute with the matching QuestionType 
            var implementations = types.Where(type => type.GetCustomAttributes(typeof(EditorForAttribute), false).Any())
                                       .Select(type => new
                                       {
                                           Type = type,
                                           Attribute = (EditorForAttribute)type.GetCustomAttribute(typeof(EditorForAttribute), false)
                                       })
                                       .Where(x => x.Attribute.QuestionType == questionType);

            // No implementations found
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
