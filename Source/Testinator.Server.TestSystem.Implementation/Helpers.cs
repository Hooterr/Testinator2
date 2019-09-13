using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation
{
    public static class Helpers
    {
        public static bool AnyFalse(params bool[] values)
        {
            if (values == null)
                return false;

            for (var i = 0; i < values.Length; i++)
                if (values[i] == false)
                    return true;

            return false;
        }

        public static bool AnyTrue(params bool[] values)
        {
            if (values == null)
                return false;

            for (var i = 0; i < values.Length; i++)
                if (values[i] == true)
                    return true;

            return false;
        }

    }
}
