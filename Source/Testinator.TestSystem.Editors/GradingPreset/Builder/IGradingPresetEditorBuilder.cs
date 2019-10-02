using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Editors
{
    public interface IGradingPresetEditorBuilder
    {
        IGradingPresetEditor Build();

        IGradingPresetEditorBuilder SetInitialPreset(Implementation.GradingPreset preset);

        IGradingPresetEditorBuilder NewPreset();

        IGradingPresetEditorBuilder SetVersion(int version);

        IGradingPresetEditorBuilder UseNewestVersion();
    }
}
