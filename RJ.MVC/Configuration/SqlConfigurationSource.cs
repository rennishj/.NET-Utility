using RJ.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace RJ.MVC.Configuration {
    /// <summary>
    /// Provides a method for retrieving application properties from
    /// a SQL Server database using the query:<para />
    /// SELECT key,value FROM dbo.AppConfiguration WHERE appId = @appId
    /// </summary>
    /// <seealso cref="IConfigurationSource" />
    public class SqlConfigurationSource : IConfigurationSource {
        private readonly string _appId;
        private readonly Func<IDbConnection> _connFactory;
        private readonly TimeSpan _expiration;
        private DateTime _loadedOn;
        private Dictionary<string, string> _properties;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlConfigurationSource"/> class.
        /// </summary>
        /// <param name="appId">ID of the application.</param>
        /// <param name="connFactory">The connection factory</param>
        /// <param name="expiration">How long before the configuration expires and a new copy is retrieved</param>
        /// <exception cref="ArgumentException">Must provide an app name - appName</exception>
        /// <exception cref="ArgumentNullException">connFactory</exception>
        public SqlConfigurationSource(string appId, Func<IDbConnection> connFactory, TimeSpan expiration) {
            if (string.IsNullOrEmpty(appId)) {
                throw new ArgumentException("Must provide an app name", nameof(appId));
            }

            _appId = appId;
            _connFactory = connFactory ?? throw new ArgumentNullException(nameof(connFactory));
            _expiration = expiration;
        }

        /// <summary>
        /// Loads and caches the properties from the database
        /// </summary>
        private void LoadProperties() {
            using (var conn = _connFactory()) {
                var cmd = conn.CreateCommand();
                var param = cmd.CreateParameter();
                param.Value = _appId;
                param.DbType = DbType.String;
                cmd.Parameters.Add(param);
                cmd.CommandText = "SELECT key,value FROM dbo.AppConfiguration WHERE appId = @appId";
                conn.Open();
                using (var reader = cmd.ExecuteReader()) {
                    var dict = new Dictionary<string, string>();
                    while (reader.Read()) {
                        dict.Add(reader.GetString(0), reader.GetString(1));
                    }
                    _properties = dict;
                }
            }
            _loadedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Return a string representation of a configuration value
        /// found in the database.<para />
        /// If the data has not yet been loaded, loads and caches the data.
        /// </summary>
        /// <param name="key">Key the value is stored under</param>
        public string GetValue(string key) {
            if (Properties.TryGetValue(key, out var value)) {
                return value;
            }
            return null;
        }

        /// <summary>
        /// Gets the specified connection string from the source.
        /// </summary>
        /// <param name="key">The connection string name</param>
        /// <exception cref="MissingConfigurationException">The connection string at this key does not exist</exception>
        public System.Configuration.ConnectionStringSettings GetConnectionString(string key) {
            var connStringKey = string.Format("connectionString.{0}", key);
            var providerNameKey = string.Format("providerName.{0}", key);
            var connString = GetValue(connStringKey);
            var providerName = GetValue(providerNameKey);
            if (string.IsNullOrEmpty(connString) || string.IsNullOrEmpty(providerName)) {
                throw new MissingConfigurationException(new[] { connStringKey, providerNameKey }, 
                    string.Format("Connection string value(s) at key '{0}' cannot be found.", key));
            }
            return new System.Configuration.ConnectionStringSettings(key, connString, providerName);
        }

        /// <summary>
        /// Reload the properties from the database
        /// </summary>
        public void Referesh() {
            LoadProperties();
        }

        private IReadOnlyDictionary<string, string> Properties {
            get {
                if (_properties == null || _loadedOn.Add(_expiration) > DateTime.UtcNow) {
                    Referesh();
                }
                return _properties;
            }
        }
    }
}