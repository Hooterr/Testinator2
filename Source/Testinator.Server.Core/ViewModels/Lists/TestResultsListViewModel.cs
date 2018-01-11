﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Testinator.Core;

namespace Testinator.Server.Core
{
    /// <summary>
    /// View model for a test results list
    /// </summary>
    public class TestResultsListViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// The test results this viewmodel is loaded with
        /// </summary>
        private List<TestResults> mResults = new List<TestResults>();

        #endregion

        #region Public Properties

        /// <summary>
        /// All test results found on the machine
        /// </summary>
        public ObservableCollection<TestResultsListItemViewModel> Items { get; private set; } = new ObservableCollection<TestResultsListItemViewModel>();

        #endregion

        #region Public Events

        /// <summary>
        /// Fired when an item is selected
        /// </summary>
        public event Action<TestResults> ItemSelected = (x) => { };

        #endregion

        #region Public Methods

        /// <summary>
        /// Load the control with item read from the disk
        /// </summary>
        public void LoadItems()
        {
            mResults = FileReaders.BinReader.ReadAllResults();
            Items = new ObservableCollection<TestResultsListItemViewModel>();

            var indexer = 0;
            foreach (var result in mResults)
            {
                Items.Add(new TestResultsListItemViewModel(result, indexer));
                indexer++;
            }
        }

        /// <summary>
        /// Check if there is any item selected in the list
        /// </summary>
        /// <returns></returns>
        public bool IsAnySelected()
        {
            foreach (var item in Items)
            {
                if (item.IsSelected)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Check which item is currently selected
        /// </summary>
        /// <returns>Null if there isn't any item selected,
        /// otherwise return the currently selected item</returns>
        public TestResults SelectedItem()
        {
            foreach(var item in Items)
            {
                if (item.IsSelected)
                {
                    var idx = item.Index;
                    return mResults[idx];
                }
            }

            return null;
        }

        #endregion

        #region Public Commands

        /// <summary>
        /// The command to select item from the list
        /// </summary>
        public ICommand SelectCommand { get; private set; }

        #endregion

        #region Command Methods

        /// <summary>
        /// Selects and item from the list
        /// </summary>
        /// <param name="index">The index of the item (intiger)</param>
        private void Select(object index)
        {
            int indexInt;

            try
            {
                indexInt = (int)index;
            }
            catch
            {
                return;
            }

            SelectItem(indexInt);

            var selectedItem = mResults[indexInt];

            ItemSelected.Invoke(selectedItem);
        }


        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestResultsListViewModel()
        {
            SelectCommand = new RelayParameterizedCommand(Select);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Marks all the items not selected
        /// </summary>
        private void UncheckAll()
        {
            foreach (var item in Items)
                item.IsSelected = false;
        }            

        /// <summary>
        /// Selects an item
        /// </summary>
        /// <param name="index">The index of the item</param>
        private void SelectItem(int index)
        {
            if (index >= Items.Count)
                return;

            UncheckAll();

            Items[index].IsSelected = true;
        }

        #endregion
    }
}