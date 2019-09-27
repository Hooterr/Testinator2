using System;

namespace Testinator.TestSystem.Abstractions
{
    /// <summary>
    /// The category string that implements One-Way-Linked list for subcategories
    /// </summary>
    [Serializable]
    public class Category
    {
        /// <summary>
        /// The name of this category
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The subcategory that is a child of this category
        /// Works as One-Way-Linked list
        /// If it's null, current category is the lowest subcategory
        /// If it's not, current category has lower subcategory
        /// </summary>
        public Category SubCategory { get; set; }
    }
}
