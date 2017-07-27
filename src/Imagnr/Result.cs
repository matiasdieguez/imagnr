using System.Collections.Generic;

namespace Imagnr
{
    /// <summary>
    /// Result of the process
    /// </summary>
    public class Result
    {
        /// <summary>
        /// The list of Recognized entities with percent
        /// </summary>
        public List<RecognizedEntity> RecognizedEntities = new List<RecognizedEntity>();
    }
}
