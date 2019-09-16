namespace Testinator.TestSystem.Editors
{
    public static class Versions
    {
        // Bum it up with every change to any test system related component
        public const int Highest = 1;
        public const int Lowest = 1;

        public static bool InRange(int version) => version >= Lowest && version <= Highest;
        public static bool NotInRange(int version) => !InRange(version);
    }

}
