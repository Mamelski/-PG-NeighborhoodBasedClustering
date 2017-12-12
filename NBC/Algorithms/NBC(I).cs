namespace NBC.Algorithms
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The nb c_ i_.
    /// </summary>
    public class NBC_I_ : Algorithm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NBC_I_"/> class.
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
        public NBC_I_(List<string> allWords, Matrix<Category, string> chances, List<Category> categories)
            : base(allWords, chances, categories)
        {
        }

        /// <summary>
        /// The categorize.
        /// </summary>
        /// <param name="documentWords">
        /// The document words.
        /// </param>
        /// <returns>
        /// The <see cref="Category"/>.
        /// </returns>
        public override Category Categorize(string[] documentWords)
        {
            // wartości 1 albo 0
            var results = new Dictionary<Category, double>();

            for (int i = 0; i < documentWords.Count(); ++i)
            {
                documentWords[i] = documentWords[i].ToLowerInvariant();
            }

            foreach (var category in this.Categories)
            {
                results[category] = 0;
            }

            foreach (var word in this.AllWords)
            {
                foreach (var category in this.Categories)
                {
                    if (documentWords.Contains(word))
                    {
                        if (this.Chances[category, word] > 0)
                        {
                            ++results[category];
                        }
                    }
                    else
                    {
                        if (this.Chances[category, word] == 0)
                        {
                            ++results[category];
                        }
                    }
                }
            }

            var max = double.NegativeInfinity;
            Category resultCategory = null;
            foreach (var result in results)
            {
                if (result.Value > max)
                {
                    max = result.Value;
                    resultCategory = result.Key;
                }
            }

            return resultCategory;
        }
    }
}