namespace NBC.Algorithms
{
    using System.Collections.Generic;

    /// <summary>
    /// The algorithm.
    /// </summary>
    public abstract class Algorithm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Algorithm"/> class.
        /// </summary>
        /// <param name="allWords">
        /// The all words.
        /// </param>
        /// <param name="chances">
        /// The Chances.
        /// </param>
        /// <param name="categories">
        /// The Categories.
        /// </param>
        protected Algorithm(List<string> allWords, Matrix<Category, string> chances, List<Category> categories)
        {
            this.AllWords = allWords;
            this.Chances = chances;
            this.Categories = categories;
        }

        /// <summary>
        /// Gets or sets the all words.
        /// </summary>
        protected List<string> AllWords { get; set; }

        /// <summary>
        /// Gets or sets the Chances.
        /// </summary>
        protected Matrix<Category, string> Chances { get; set; }

        /// <summary>
        /// Gets or sets the Categories.
        /// </summary>
        protected List<Category> Categories { get; set; }

        /// <summary>
        /// The do work.
        /// </summary>
        /// <param name="documentWords">
        /// The document words.
        /// </param>
        /// <returns>
        /// The <see cref="Category"/>.
        /// </returns>
        public abstract Category Categorize(string[] documentWords);

        /// <summary>
        /// The amortization.
        /// </summary>
        public double Amortization { get; } = 0;//0.001;
    }
}
