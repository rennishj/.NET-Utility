using System;
using System.Runtime.Serialization;

namespace RJ.DependencyInjection {
    /// <summary>
    /// Represents an exception that occurred when a resolver
    /// or instance could not be registered
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public class RegistrationException : Exception {

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationException"/> class.
        /// </summary>
        /// <param name="type">The type that could not be registered</param>
        /// <param name="name">The name that could not be registered</param>
        public RegistrationException(Type type, string name) :
            base(string.Format(ExceptionResources.IdenticalRegistration, type.FullName, name)) {
            Type = type;
            Name = name;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationException"/> class.
        /// </summary>
        /// <param name="type">The type that could not be registered</param>
        /// <param name="name">The name that could not be registered</param>
        /// <param name="message">The message</param>
        public RegistrationException(Type type, string name, string message) : base(message) {
            Type = type;
            Name = name;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public RegistrationException(string message) : base(message) {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public RegistrationException(string message, Exception innerException) : base(message, innerException) {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected RegistrationException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
        /// <summary>
        /// Gets the type that could not be registered
        /// </summary>
        public Type Type { get; private set; }
        /// <summary>
        /// Gets the name that could not be registered
        /// </summary>
        public string Name { get; private set; }
    }
}
