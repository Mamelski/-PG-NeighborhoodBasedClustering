namespace NBC.Catgories
{
    using System.Collections.Generic;

    /// <summary>
    /// The category.
    /// </summary>
    public abstract class Category
    {
        /// <summary>
        /// The sample files.
        /// </summary>
        protected List<string> SampleFiles { get; }= new List<string>();

        protected string Root { get; } = @"C:\Users\Jakub\Desktop\httpwww.sportowapolitechnika.pl";

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        public List<string> Terms { get; } = new List<string>();

        public abstract void Learn();
    }
}
