﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Testinator.TestSystem.Abstractions.Questions.Task
{
    public interface IImageContent
    {
        // Get rid of the methods that alter the object (delete, add etc.)
        // Constructing the object should be done by a builder
        IList<Image> GetAll(); 
    }
}
