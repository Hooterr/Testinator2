using System.Collections.Generic;

namespace Testinator.TestSystem.Abstractions
{
    /// <summary>
    /// The interface to manage everything related to question pools
    /// </summary>
    public interface IPoolManager
    {
        ICollection<IQuestion> GetQuestionPoolForCurrentUser();
        ICollection<ICollection<IQuestion>> GetOtherUserPools(); // TODO: Return collection of objects (name, usedid, questions) instead of collection of collections
        void AddQuestionToThePool(IQuestion question); // This should probably apply changes for existing questions, or if not, just make another method to edit question
        void RemoveQuestionFromPool(IQuestion question);
    }
}
