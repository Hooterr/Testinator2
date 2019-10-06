namespace Testinator.TestSystem.Implementation.Questions.ScoringStrategy
{
    public static class ScoringStrategyFactory
    {
        public static T GetStrategy<T>()
            where T : IScoringStrategy, new()
        {
            return new T();
        }
    }
}
