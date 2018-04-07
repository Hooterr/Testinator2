﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Testinator.Core;

namespace Testinator.Server.Core
{
    /// <summary>
    /// The view model for the test list
    /// </summary>
    public class QuestionListViewModel : BaseViewModel
    {
        #region Singleton

        /// <summary>
        /// Single instance of this view model
        /// </summary>
        public static QuestionListViewModel Instance { get; private set; } = new QuestionListViewModel();

        #endregion

        #region Public Constants 
        
        /// <summary>
        /// Indicates no selection
        /// </summary>
        public const int NothingSelected = -1;

        #endregion

        #region Privates Members

        /// <summary>
        /// Indicates currently selected item's index
        /// </summary>
        private int mCurrentlySelectedItemIndex = NothingSelected;

        /// <summary>
        /// Questions is this list
        /// </summary>
        private List<Question> mQuestions;

        #endregion

        #region Public Properties

        /// <summary>
        /// List of items (tests) in this test list
        /// </summary>
        public ObservableCollection<QuestionListItemViewModel> Items { get; private set; }

        /// <summary>
        /// Indicates if there is any test selected
        /// </summary>
        public bool IsAnySelected => SelectedItem != null;

        /// <summary>
        /// Indicates currently selected item
        /// NOTE: null if nothing is selected
        /// </summary>
        public Question SelectedItem { get; private set; }

        #endregion

        /// <summary>
        /// Fired when selection in this list chages
        /// NOTE: not invoked if this same item is selected multiple times
        /// </summary>
        public event Action SelectionChanged = () => { };

        /// <summary>
        /// Fired when item gets selected 
        /// </summary>
        public event Action<Question> ItemSelected = (o) => { };

        #region Commands

        /// <summary>
        /// The command to select an item
        /// </summary>
        public ICommand SelectItemCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public QuestionListViewModel()
        {
            // Create commands
            SelectItemCommand = new RelayParameterizedCommand((param) => SelectItem(param));

            // Create defaults
            Items = new ObservableCollection<QuestionListItemViewModel>();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Select an item from the list
        /// </summary>
        private void SelectItem(object param)
        {
            // Cast the parameter
            var newSelectedItemIndex = int.Parse(param.ToString());

            // If the same item got selected there is no more to do
            if (mCurrentlySelectedItemIndex == newSelectedItemIndex)
                return;

            // Unselect last item if there was any selected
            if (mCurrentlySelectedItemIndex != NothingSelected)
                Items[mCurrentlySelectedItemIndex].IsSelected = false;

            // Select the one that has been clicked
            Items[newSelectedItemIndex].IsSelected = true;

            // Save new selected item index
            mCurrentlySelectedItemIndex = newSelectedItemIndex;

            // Set selected item
            SelectedItem = mQuestions[newSelectedItemIndex];

            // Fire the events
            SelectionChanged.Invoke();
            ItemSelected.Invoke(SelectedItem);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads this viewmodel with given questions
        /// </summary>
        /// <param name="items">Items to load</param>
        public void LoadItems(List<Question> items)
        {
            mQuestions = items;
            Items = new ObservableCollection<QuestionListItemViewModel>();
            SelectedItem = null;

            for (var i = 0; i < items.Count; i++)
            {
                Items.Add(new QuestionListItemViewModel()
                {
                    ID = i,
                    Task = mQuestions[i].Task.StringContent,
                    Type = mQuestions[i].Type,
                });
            }
        }

        /// <summary>
        /// Removes question from the list
        /// </summary>
        /// <param name="questionToRemove">Question to remove</param>
        public void RemoveQuestion(Question questionToRemove)
        {
            var questionIndex = mQuestions.IndexOf(questionToRemove);

            if (questionIndex == -1)
                return;

            mQuestions.Remove(questionToRemove);

            Items.RemoveAt(questionIndex);

            // Update the rest questions ids
            for (var i = questionIndex; i < Items.Count; i++)
            {
                Items[i].ID = i;
            }
        }

        /// <summary>
        /// Appends question to the list 
        /// </summary>
        /// <param name="questionToAdd">Question to add to the list</param>
        public void AppendQuestion(Question questionToAdd)
        {
            mQuestions.Add(questionToAdd);

            Items.Add(new QuestionListItemViewModel()
            {
                ID = Items.Count > 0 ? Items.Last().ID + 1 : 0,
                Type = questionToAdd.Type,
                Task = questionToAdd.Task.StringContent,
            });
        }

        /// <summary>
        /// Unchecks all selected items
        /// </summary>
        public void UnCheckAll()
        {
            if (mCurrentlySelectedItemIndex != NothingSelected)
            {
                Items[mCurrentlySelectedItemIndex].IsSelected = false;
                SelectionChanged.Invoke();
            }
        }

        #endregion
    }
}
