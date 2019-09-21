using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Testinator.Server.Domain
{
    public static class ApplicationDataFoldersExternsions
    {
        public static string GetFolderName(this ApplicationDataFolders folder)
        {
            var memberInfo =( typeof(ApplicationDataFolders).GetMember(folder.ToString())).First();
            var attr = memberInfo.GetCustomAttributes<FolderNameAttribute>().FirstOrDefault();
            if (attr != null)
            {
                return attr.FolderName;
            }
            else
                return folder.ToString();
        }
    }
}
