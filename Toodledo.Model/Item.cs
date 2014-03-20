namespace Toodledo.Model
{
    /// <summary>
    /// Represents any Toodledo entity.
    /// </summary>
    public abstract class Item
    {
        /// <summary>
        /// Gets or sets the unique id.
        /// </summary>
        /// <value>The id.</value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name of this item.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
    }
}