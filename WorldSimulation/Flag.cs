using System;

using WorldSimulation.Flags;

namespace WorldSimulation
{
    /// <summary>
    /// A boolean flag to tell indicate something has happened to an entity.
    /// </summary>
    public class Flag
    {
        /// <summary>
        /// Gets or sets the name of the flag.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the category for the flag.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public FlagCategory Category { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Flag"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="flagCategory">The flag category.</param>
        protected Flag(string name, FlagCategory flagCategory)
        {
            Name = name;
            Category = flagCategory;
        }

        /// <summary>
        /// Creates the specified name via a static factory method.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public static Flag Create(string name, FlagCategory category = FlagCategory.None)
        {
            return new Flag(name, category);
        }

        /// <summary>
        /// Creates the specified name via a static factory method.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static Flag Create(string name)
        {
            return new Flag(name, FlagCategory.None);
        }
    }
}
