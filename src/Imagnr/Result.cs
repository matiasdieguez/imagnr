using System.Collections.Generic;

namespace Imagnr
{
    /// <summary>
    /// Result of the process
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The list of Recognized entities with percent
        /// </summary>
        public List<RecognizedEntity> RecognizedEntities { get; set; }
    }
}
