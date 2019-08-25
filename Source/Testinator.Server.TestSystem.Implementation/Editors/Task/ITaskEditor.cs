﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation
{ 
    public interface ITaskEditor
    {
        ITextEditor Text { get; }

        IImageEditor Images { get; }
    }
}