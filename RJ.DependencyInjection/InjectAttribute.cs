using System;

namespace RJ.DependencyInjection {
    /// <summary>
    /// Represents an attribute signaling to the dependency resolver to inject an instance of this type
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class InjectAttribute : Attribute {
        /// <summary>
        /// Initializes a new instance of the <see cref="InjectAttribute"/> class.
        /// </summary>
        public InjectAttribute() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InjectAttribute"/> class.
        /// </summary>
        /// <param name="dependencyName">Registered name of the dependency</param>
        public InjectAttribute(string dependencyName) {
            DependencyName = dependencyName;
        }

        /// <summary>
        /// Gets the name of the dependency
        /// </summary>
        public string DependencyName { get; }
    }
}
