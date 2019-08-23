﻿using System.Threading.Tasks;
using System.Windows.Input;
using Testinator.Core;
using Testinator.Server.Core;

namespace Testinator.Server
{
    /// <summary>
    /// The class for MessageDialog box with a view model for it
    /// </summary>
    public class MessageDialogUserControl : BaseDialogUserControl
    {
        #region Override Methods

        /// <summary>
        /// Shows the dialog
        /// </summary>
        /// <typeparam name="T">Type of view model</typeparam>
        /// <param name="viewmodel">The view model for the content of that dialog</param>
        public override Task ShowDialog<T>(T viewmodel)
        {
            // Check if ApplicationSettings allow showing this type of dialog box
            if (!DI.Settings.AreInformationMessageBoxesAllowed)
                return Task.Delay(1);

            // Now we can show the message
            base.ShowDialog<T>(viewmodel);

            return Task.Delay(1);
        }

        #endregion
    }
}
