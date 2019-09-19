namespace Testinator.TestSystem.Abstractions
{
    public interface IGrading
    {
        /// <summary>
        /// The maximum point score someone can get for this test
        /// </summary>
        int MaxPointScore { get; }
        
        /// <summary>
        /// Gets grade based on the point score 
        /// </summary>
        /// <param name="score">The score</param>
        /// <returns>Corresponding grade</returns>
        // TODO replace int with complex type with question categories etc.
        IGrade GetGrade(int score);
    }
}
