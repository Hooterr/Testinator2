using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Abstractions.Results
{
    public interface IUserInfo
    {
        string FistName { get; set; }
        string LastName { get; set; }

        // Or something...
        string Class { get; set; }
    }
}
