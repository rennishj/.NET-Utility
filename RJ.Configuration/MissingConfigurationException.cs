using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RJ.Configuration {
    /// <summary>
    /// Represents an exception indicating that a set of configuration values are missing
    /// from an <see cref="IConfigurationSource"/> instance
    /// </summary>
    [Serializable]
    public class MissingConfigurationException : Exception {

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingConfigurationException"/> class
        /// </summary>
        /// <param name="keys"></param>
        public MissingConfigurationException(params string[] keys)
            : base(string.Format("The key(s) {0} have missing associated values", string.Join(", ", keys))) {
            Keys = keys;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingConfigurationException"/> class
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="message"></param>
        public MissingConfigurationException(IEnumerable<string> keys, string message) : base(message) {
            Keys = keys;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingConfigurationException"/> class
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MissingConfigurationException(string message, Exception innerException) : base(message, innerException) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingConfigurationException"/> class
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected MissingConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }

        /// <summary>
        /// Keys that are missing from the configuration
        /// </summary>
        public IEnumerable<string> Keys { get; private set; }
    }
}
