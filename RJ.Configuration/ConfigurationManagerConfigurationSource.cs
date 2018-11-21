using System.Configuration;

namespace RJ.Configuration {
    /// <summary>
    /// <see cref="IConfigurationSource"/> that reads from the default
    /// <see cref="ConfigurationManager.AppSettings"/> collection
    /// </summary>
    /// <seealso cref="IConfigurationSource" />
    public class ConfigurationManagerConfigurationSource : IConfigurationSource {

        /// <summary>
        /// Return a <see cref="ConfigurationManager.AppSettings"/> value
        /// </summary>
        /// <param name="key">Key the value is stored under</param>
        public string GetValue(string key) {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// Gets the specified connection string from <see cref="ConfigurationManager.ConnectionStrings"/>
        /// </summary>
        /// <param name="key">The connection string name</param>
        public ConnectionStringSettings GetConnectionString(string key) {
            return ConfigurationManager.ConnectionStrings[key];
        }
    }
}
