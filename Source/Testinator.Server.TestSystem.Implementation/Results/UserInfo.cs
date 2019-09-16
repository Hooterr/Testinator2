using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions.Results;

namespace Testinator.TestSystem.Implementation.Results
{
    public class UserInfo : IUserInfo
    {
        public string FistName { get; set; }
        public string LastName { get; set; }
        public string Class { get; set; }
    }
}
