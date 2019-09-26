using System.Linq;
using System.Reflection;
using Testinator.Server.Domain;

namespace Testinator.Server.Files
{
    public static class ApplicationDataFoldersExternsions
    {
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
