using System;
using System.IO;
using System.Linq;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// Indicates the name of the folder in <see cref="ApplicationDataFolders"/>
    /// </summary>
    public class FolderNameAttribute : Attribute
    {
        /// <summary>
        /// The name of the folder
        /// </summary>
        public string FolderName { get; private set; }

        public FolderNameAttribute(string folderName)
        {
            if (string.IsNullOrEmpty(folderName))
                throw new ArgumentException("This is not a valid folder name.");

            foreach (var illegalChar in Path.GetInvalidPathChars())
            {
                if(folderName.Contains(illegalChar))
                    throw new ArgumentException("This is not a valid folder name.");
            }

            FolderName = folderName;
        }
    }
}
