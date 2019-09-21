using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Testinator.Server.Domain
{
    public class FolderNameAttribute : Attribute
    {
        public string FolderName { get; private set; }

        public FolderNameAttribute(string folderName)
        {
            if (string.IsNullOrEmpty(folderName))
                throw new ArgumentException("This is not a valid folder name.");

            foreach (var illegalChar in Path.GetInvalidPathChars().Concat(new char[] { '\\', '/' }))
            {
                if(folderName.Contains(illegalChar))
                    throw new ArgumentException("This is not a valid folder name.");
            }

            FolderName = folderName;
        }
    }
}
