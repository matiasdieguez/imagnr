using System.Collections.Generic;

namespace Imagnr
{
    /// <summary>
    /// Represents an Entity of Recognizer's Catalog
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Name of the Entity
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// If true, the library splits every word in Name and uses each one as a tag
        /// </summary>
        public bool UseNameAsTag { get; set; }

        /// <summary>
        /// Tags list
        /// </summary>
        public List<Tag> Tags { get; set; }
    }
}
