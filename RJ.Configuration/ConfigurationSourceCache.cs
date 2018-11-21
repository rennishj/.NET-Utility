using System;
using System.Collections.Generic;
using System.Configuration;

namespace RJ.Configuration {
    /// <summary>
    /// Provides a decorator for adding caching to configuration values retrieved from an <see cref="IConfigurationSource"/>
    /// </summary>
    /// <seealso cref="Fnf.Common.IConfigurationSource" />
    public class ConfigurationSourceCache : IConfigurationSource {

        private readonly Dictionary<string, ConfigurationValue<string>> _properties;
        private readonly Dictionary<string, ConfigurationValue<ConnectionStringSettings>> _connStrings;

        private readonly IConfigurationSource _configurationSource;
        private readonly TimeSpan _expirationSpan;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationSourceCache"/> class.
        /// </summary>
        /// <param name="configurationSource">The configuration source</param>
        /// <param name="expirationSpan">The expiration span</param>
        /// <exception cref="System.ArgumentNullException">configurationSource</exception>
        public ConfigurationSourceCache(IConfigurationSource configurationSource, TimeSpan expirationSpan) {
            _configurationSource = configurationSource ?? throw new ArgumentNullException(nameof(configurationSource));
            _expirationSpan = expirationSpan;
            _properties = new Dictionary<string, ConfigurationValue<string>>();
            _connStrings = new Dictionary<string, ConfigurationValue<ConnectionStringSettings>>();
        }

        /// <summary>
        /// Gets the specified connection string from the source.
        /// </summary>
        /// <param name="key">The connection string name</param>
        public ConnectionStringSettings GetConnectionString(string key) {
            if (_connStrings.TryGetValue(key, out var value) && value.Loaded.Add(_expirationSpan) <= DateTime.UtcNow) {
                return value.Value;
            }
            var retreivedValue = _configurationSource.GetConnectionString(key);
            _connStrings.Add(key, new ConfigurationValue<ConnectionStringSettings>(retreivedValue));
            return retreivedValue;
        }

        /// <summary>
        /// Return a string representation of a configuration value
        /// </summary>
        /// <param name="key">Key the value is stored under</param>
        public string GetValue(string key) {
            if (_properties.TryGetValue(key, out var value) && value.Loaded.Add(_expirationSpan) <= DateTime.UtcNow) {
                return value.Value;
            }
            var retreivedValue = _configurationSource.GetValue(key);
            _properties.Add(key, new ConfigurationValue<string>(retreivedValue));
            return retreivedValue;
        }

        private class ConfigurationValue<T> {

            public ConfigurationValue(T value) {
                Value = value;
                Loaded = DateTime.UtcNow;
            }
            public T Value { get; }
            public DateTime Loaded { get; }
        }
    }
}