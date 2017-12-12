namespace NBC
{
    using System.Collections.Generic;

    /// <summary>
    /// The matrix.
    /// </summary>
    /// <typeparam name="TKey1">
    /// </typeparam>
    /// <typeparam name="TKey2">
    /// </typeparam>
    public class Matrix<TKey1, TKey2>
    {
        /// <summary>
        /// The inner dictionary.
        /// </summary>
        private readonly Dictionary<TKey1, Dictionary<TKey2, double>> innerDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix{TKey1,TKey2}"/> class.
        /// </summary>
        /// <param name="key1space">
        /// The key 1 space.
        /// </param>
        public Matrix(ICollection<TKey1> key1space)
        {
            this.innerDictionary = new Dictionary<TKey1, Dictionary<TKey2, double>>();
            foreach (var k1 in key1space)
            {
                this.innerDictionary[k1] = new Dictionary<TKey2, double>();
            }
        }

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="key1">
        /// The key 1.
        /// </param>
        /// <param name="key2">
        /// The key 2.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public double this[TKey1 key1, TKey2 key2]
        {
            get
            {
                if (!this.innerDictionary[key1].ContainsKey(key2))
                {
                    this.innerDictionary[key1].Add(key2, 0.0);
                }

                return this.innerDictionary[key1][key2];
            }

            set
            {
                if (!this.innerDictionary[key1].ContainsKey(key2))
                {
                    this.innerDictionary[key1].Add(key2, value);
                }

                this.innerDictionary[key1][key2] = value;
            }
        }

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="key1">
        /// The key 1.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>Dictionary</cref>
        ///     </see>
        ///     .
        /// </returns>
        public Dictionary<TKey2, double> this[TKey1 key1]
        {
            get
            {
                if (this.innerDictionary[key1] == null)
                {
                    this.innerDictionary[key1] = new Dictionary<TKey2, double>();
                }

                return this.innerDictionary[key1];
            }
        }
    }
}