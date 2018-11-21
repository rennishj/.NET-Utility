using System;
using System.Runtime.Serialization;

namespace RJ.DependencyInjection {
    /// <summary>
    /// Represents an exception that occurred when a type could not be resolved
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public class ResolutionException : Exception {

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionException"/> class.
        /// </summary>
        /// <param name="type">The type that could not be resolved</param>
        public ResolutionException(Type type)
            : this(type, string.Format(ExceptionResources.ResolveDefault, type.FullName)) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionException"/> class.
        /// </summary>
        /// <param name="type">The type</param>
        /// <param name="message">The message</param>
        public ResolutionException(Type type, string message)
            : base(string.Format("{0}{1}{2}", 
                string.Format(ExceptionResources.ResolveDefault, type.FullName),
                Environment.NewLine,
                message)) {
            Type = type;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionException"/> class.
        /// </summary>
        /// <param name="type">The type</param>
        /// <param name="message">The message</param>
        /// <param name="innerException">The inner exception</param>
        public ResolutionException(Type type, string message, Exception innerException)
            : base(string.Format("{0}{1}{2}", 
                string.Format(ExceptionResources.ResolveDefault, type.FullName),
                Environment.NewLine,
                message),
                innerException) {
            Type = type;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected ResolutionException(SerializationInfo info, StreamingContext context) : base(info, context) {

        }

        /// <summary>
        /// Gets the type that could not be resolved
        /// </summary>
        public Type Type { get; }
    }
}
