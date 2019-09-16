using System;
using System.Collections;
using System.Collections.Generic;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Editors
{
    internal class QuestionEditorCollection : IQuestionEditorCollection
    {
        private IList<IQuestion> mQuestions;

        public IQuestion this[int index] => mQuestions[index];

        public int Count => mQuestions.Count;

        public IEnumerator<IQuestion> GetEnumerator()
        {
            return mQuestions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return mQuestions.GetEnumerator();
        }

        public OperationResult Add(IQuestion question)
        {
            mQuestions.Add(question);
            return OperationResult.Success();
        }

        public OperationResult DeleteAt(int idx)
        {
            try
            {
                mQuestions.RemoveAt(idx);
            }
            catch(IndexOutOfRangeException)
            {
                return OperationResult.Fail();
            }
            return OperationResult.Success();
        }

        public OperationResult DeleteAll()
        {
            mQuestions.Clear();
            return OperationResult.Success();
        }

        public QuestionEditorCollection(List<IQuestion> initial = null)
        {
            mQuestions = initial;
            if (mQuestions == null)
                mQuestions = new List<IQuestion>();
        }
    }
}
