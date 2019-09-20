using System.Collections.Generic;
using Testinator.Core;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The interface for repository that handles application's settings
    /// </summary>
    public interface ISettingsRepository : IRepository<Setting, int>
    {
        List<SettingsPropertyInfo> GetAllSettings();
        void SaveSetting(SettingsPropertyInfo setting);
    }
}
