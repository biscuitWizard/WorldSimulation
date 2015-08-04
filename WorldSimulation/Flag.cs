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
        /// Gets or sets a value indicating whether this <see cref="Flag"/> is true or false.
        /// </summary>
        /// <value>
        ///   <c>true</c> if value; otherwise, <c>false</c>.
        /// </value>
        public bool Value { get; set; }

        protected Flag(string name, bool value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Creates the specified name via a static factory method.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static Flag Create(string name, bool value)
        {
            return new Flag(name, value);
        }

        /// <summary>
        /// Creates the specified name via a static factory method.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static Flag Create(string name)
        {
            return new Flag(name, true);
        }
    }
}
