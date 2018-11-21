using System.Configuration;

namespace RJ.Configuration {
    /// <summary>
    /// Provides a way of creating a composite <see cref="IConfigurationSource"/>
    /// that "falls back" to each successive source until none return a value.
    /// <see cref="GetValue(string)"/> will return when the first source returns
    /// a value.
    /// </summary>
    /// <seealso cref="IConfigurationSource" />
    public class CompositeConfigurationSource : IConfigurationSource {
        private readonly IConfigurationSource[] _sources;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeConfigurationSource"/> class.
        /// </summary>
        /// <param name="sources">The set of sources to read from</param>
        public CompositeConfigurationSource(params IConfigurationSource[] sources) {
            _sources = sources;
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not to throw
        /// a <see cref="MissingConfigurationException"/> when no source
        /// returns a value
        /// </summary>
        public bool ThrowOnMissing { get; set; }

        /// <summary>
        /// Gets the specified connection string from the source.
        /// </summary>
        /// <param name="key">The connection string name</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ConnectionStringSettings GetConnectionString(string key) {
            foreach (var source in _sources) {
                var value = source.GetConnectionString(key);
                if (value != null) {
                    return value;
                }
            }
            if (ThrowOnMissing) {
                throw new MissingConfigurationException(key);
            }
            return null;
        }

        /// <summary>
        /// Check each source in order, when the first source returns a 
        /// non-null value, return that.
        /// </summary>
        /// <param name="key">Key the value is stored under</param>
        /// <exception cref="MissingConfigurationException"></exception>
        public string GetValue(string key) {
            foreach (var source in _sources) {
                var value = source.GetValue(key);
                if (value != null) {
                    return value;
                }
            }
            if (ThrowOnMissing) {
                throw new MissingConfigurationException(key);
            }
            return null;
        }
    }
}
