namespace NBC
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    using NBC.Algorithms;

    using NUglify;

    /// <summary>
    /// The category manager.
    /// </summary>
    public class CategoryManager
    {
        /// <summary>
        /// The root.
        /// </summary>
        private const string Root = @"C:\Users\Jakub\Desktop\httpwww.sportowapolitechnika.pl";

        /// <summary>
        /// The Categories.
        /// </summary>
        private readonly List<Category> categories = new List<Category>();

        /// <summary>
        /// The words.
        /// </summary>
        private Matrix<Category, string> Words;

        /// <summary>
        /// The Chances.
        /// </summary>
        private Matrix<Category, string> Chances;

        private int numberOfDocuments = 0;
        private int nbc1Hits = 0;
        private int nbc2Hits = 0;
        private int nbc3Hits = 0;

        private List<string> allWords = new List<string>();

        /// <summary>
        /// The load Categories.
        /// </summary>
        public void DoWork()
        {
            // Każdy podkatalog traktujemy jako nową kategorię.
            foreach (var directory in Directory.GetDirectories(Root))
            {
                this.categories.Add(ParseCategory(directory));
            }

            this.Learn();
            this.CalculateWordsChances();

            this.numberOfDocuments = 0;
            this.RunAlgorithms(Root);

            Debug.WriteLine("NBC1");
            {
                foreach (var cate in this.categories)
                {
                    Debug.WriteLine($"{cate.PositiveHits}\t{cate.NegativeHits}");
                    cate.PositiveHits = 0;
                    cate.NegativeHits = 0;
                }
            }

            //for (int i = 0; i < this.categories.Count; ++i)
            //{
            //    Debug.WriteLine($"{i}\t{this.categories.ElementAt(i).Name}");
            //}

            int a = 0;
        }

        /// <summary>
        /// The run algorithms.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        private void RunAlgorithms(string path)
        {
            ++this.numberOfDocuments;
            foreach (var file in Directory.GetFiles(path))
            {
                this.CalculateWordsChances();
               // this.CalculateNBC1(file);
             
               // this.CalculateNBC2(file);

                this.CalculateWordsChances2();
                this.CalculateNBC3(file);
            }

            foreach (var directory in Directory.GetDirectories(path))
            {
                this.RunAlgorithms(directory);
            }
        }

        

        /// <summary>
        /// The calculate words chances 2.
        /// </summary>
        private void CalculateWordsChances2()
        {
            this.Chances = new Matrix<Category, string>(this.categories);

            foreach (var word in this.allWords)
            {
                double count = 0;

                foreach (var category in this.categories)
                {
                    count += this.Words[category, word];
                }

                foreach (var category in this.categories)
                {
                    this.Chances[category, word] = this.Words[category, word] / count;
                }
            }
        }

        /// <summary>
        /// The calculate nb c 3.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        private void CalculateNBC3(string file)
        {
            var nbc1 = new NBC_II_(this.allWords, this.Chances, this.categories);

            var html = File.ReadAllText(file);
            var text = Uglify.HtmlToText(html).Code;
            var words = text.Split(' ');
            var result = nbc1.Categorize(words);
            if (this.IsSubfolder(file, result))
            {
                result.PositiveHits++;
                ++this.nbc2Hits;
                // Debug.WriteLine(result.Name);
            }
            else
            {
                foreach (var categ in this.categories)
                {
                    if (this.IsSubfolder(file, categ))
                    {
                        result.NegativeHits++;
                    }
                }
            }

        }

        private bool IsSubfolder(string file, Category result)
        {
            if (file.Contains(result.Name))
            {
                return true;
            }
            return false;
           
        }

        /// <summary>
        /// The calculate nb c 2.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        private void CalculateNBC2(string file)
        {
            var nbc1 = new NBC_II_(this.allWords, this.Chances, this.categories);

            var html = File.ReadAllText(file);
            var text = Uglify.HtmlToText(html).Code;
            var words = text.Split(' ');
            var result = nbc1.Categorize(words);
            if (this.IsSubfolder(file, result))
            {
                result.PositiveHits++;
                ++this.nbc2Hits;
                // Debug.WriteLine(result.Name);
            }
            else
            {
                foreach (var categ in this.categories)
                {
                    if (this.IsSubfolder(file, categ))
                    {
                        result.NegativeHits++;
                    }
                }
            }
        }

        /// <summary>
        /// The calculate nb c 1.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        private void CalculateNBC1(string file)
        {
            var nbc1 = new NBC_I_(this.allWords, this.Chances, this.categories);
           
            var html = File.ReadAllText(file);
            var text = Uglify.HtmlToText(html).Code;
            var words = text.Split(' ');
            var result = nbc1.Categorize(words);
            if (this.IsSubfolder(file, result))
            {
                result.PositiveHits++;
                ++this.nbc1Hits;
                // Debug.WriteLine(result.Name);
            }
            else
            {
                foreach (var categ in this.categories)
                {
                    if (this.IsSubfolder(file, categ))
                    {
                        result.NegativeHits++;
                    }
                }
            }
        }

        /// <summary>
        /// The calculate words chances.
        /// </summary>
        private void CalculateWordsChances()
        {
            this.Chances = new Matrix<Category, string>(this.categories);

            foreach (var category in this.categories)
            {
                double count = 0;

                foreach (var word in this.allWords)
                {
                    count += this.Words[category, word];
                }

                foreach (var word in this.allWords)
                {
                    this.Chances[category, word] = this.Words[category,word]/count;
                }



            }
        }

        /// <summary>
        /// The learn.
        /// </summary>
        private void Learn()
        {
            this.Words = new Matrix<Category, string>(this.categories);
            foreach (var category in this.categories)
            {
                var html = File.ReadAllText(category.File);
                var text = Uglify.HtmlToText(html).Code;
                var words = text.Split(' ');
                foreach (var word in words)
                {
                    if (!string.IsNullOrEmpty(word) && !string.IsNullOrWhiteSpace(word))
                    {
                        ++this.Words[category, word.ToLowerInvariant()];
                    }
                }
            }

            foreach (var category in this.categories)
            {
                // słowo
                foreach (var word in this.Words[category])
                {
                    if (!this.allWords.Contains(word.Key))
                    {
                        this.allWords.Add(word.Key);
                    }
                }
            }
        }

        /// <summary>
        /// The parse category.
        /// </summary>
        /// <param name="directory">
        /// The directory.
        /// </param>
        /// <returns>
        /// The <see cref="Category"/>.
        /// </returns>
        private Category ParseCategory(string directory)
        {
            var files = Directory.GetFiles(directory);

            while (!files.Any())
            {
                foreach (var dir in Directory.GetDirectories(directory))
                {
                    files = Directory.GetFiles(dir);
                }
            }

            Debug.WriteLine(directory);
            return new Category(directory, files.First());
        }
    }
}
