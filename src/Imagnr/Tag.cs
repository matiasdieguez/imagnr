namespace Imagnr
{
    /// <summary>
    /// Represents a Tag word used to define an Entity
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// The string value of the Tag
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Minimum Similarity result of the Levenshtein distance to recognize as found tag
        /// </summary>
        public double MinimumSimilarity { get; set; }
        
        /// <summary>
        /// Indicates this as mandatory string to recognize as found tag
        /// </summary>
        public bool Required { get; set; }
    }
}
