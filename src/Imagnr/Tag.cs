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
        /// Minimum Similarity percent result of the Levenshtein distance to recognize as found tag
        /// 0 to 1
        /// </summary>
        public double MinimumSimilarity { get; set; }

        /// <summary>
        /// Score to ponderate the tag
        /// </summary>
        public double Score = 1;

        /// <summary>
        /// Indicates this as mandatory string to recognize as found tag
        /// </summary>
        public bool Required { get; set; }
    }
}
