using System;
using System.Windows.Input;

namespace Testinator.WebApp
{
    /// <summary>
    /// The extensions for <see cref="ICommand"/> interface
    /// </summary>
    public static class CommandExtensions
    {
        /// <summary>
        /// Helpful extension for commands to convert into runable actions
        /// It shortens the code in every button onclicks, since Blazor is not fully into MVVM yet
        /// </summary>
        /// <param name="command">The command to run</param>
        /// <param name="parameter">Optional parameter to use if command is parameterized</param>
        /// <returns>Runnable action that simply fires the command</returns>
        public static Action ToAction(this ICommand command, object parameter = null) => () => command.Execute(parameter);
    }
}
