﻿using System.Drawing;
using Testinator.Core;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// Viewmodel for a images editor item
    /// </summary>
    public class ImagesEditorItemViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// Id of this item in list
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Original image of this item
        /// </summary>
        public Image OriginalImage { get; set; }

        /// <summary>
        /// The thumbnail for this item
        /// </summary>
        public Image Thumbnail { get; set; }

        #endregion
    }
}
