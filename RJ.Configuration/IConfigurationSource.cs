using System.Configuration;

namespace RJ.Configuration {

    /// <summary>
    /// Provides methods for accessing configuration values
    /// </summary>
    public interface IConfigurationSource {
        /// <summary>
        /// Return a string representation of a configuration value
        /// </summary>
        /// <param name="key">Key the value is stored under</param>
        string GetValue(string key);
        /// <summary>
        /// Gets the specified connection string from the source.
        /// </summary>
        /// <param name="key">The connection string name</param>
        ConnectionStringSettings GetConnectionString(string key);
    }
}
