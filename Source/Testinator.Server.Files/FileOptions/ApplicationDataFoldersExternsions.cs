using System.Linq;
using System.Reflection;
using Testinator.Server.Domain;

namespace Testinator.Server.Files
{
    /// <summary>
    /// Extension methods for <see cref="ApplicationDataFolders"/>
    /// </summary>
    public static class ApplicationDataFoldersExternsions
    {
        /// <summary>
        /// Get the directory name from an <see cref="ApplicationDataFolders"/> enum value
        /// If this enum value doesn't have the <see cref="FolderNameAttribute"/> the name of the value is used
        /// </summary>
        /// <param name="folder">The value of the enum</param>
        /// <returns>The name of the folder</returns>
        public static string GetFolderName(this ApplicationDataFolders folder)
        {
            var memberInfo =( typeof(ApplicationDataFolders).GetMember(folder.ToString())).First();
            var attr = memberInfo.GetCustomAttributes<FolderNameAttribute>().FirstOrDefault();
            if (attr != null)
                return attr.FolderName;
            else
                return folder.ToString();
        }
    }
}
