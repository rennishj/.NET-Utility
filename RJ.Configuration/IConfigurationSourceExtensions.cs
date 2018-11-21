using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RJ.Configuration {
    /// <summary>
    /// Extension methods for <see cref="IConfigurationSource"/> objects
    /// </summary>
    public static class IConfigurationSourceExtensions {
        /// <summary>
        /// Return a list of configuration keys that are missing in the provided <see cref="IConfigurationSource"/>
        /// </summary>
        /// <param name="configurationSource">Source to check</param>
        /// <param name="keys">List of keys to check</param>
        public static IEnumerable<string> VerifyConfigurationValues(this IConfigurationSource configurationSource, params string[] keys) {
            return keys.Select(k => new {
                Key = k,
                Value = configurationSource.GetValue(k)
            }).Where(kvp => kvp.Value == null)
                .Select(kvp => kvp.Key);
        }

        /// <summary>
        /// Retrieve a boolean value 
        /// </summary>
        /// <param name="source">Source to check</param>
        /// <param name="key">Key the value is stored under</param>
        /// <exception cref="FormatException">Thrown if the value cannot be parsed into a boolean</exception>
        public static bool GetBool(this IConfigurationSource source, string key) {
            var val = source.GetValue(key);
            if (bool.TryParse(val, out var result)) {
                return result;
            }
            throw new FormatException(
                string.Format("Configuration value at key '{0}' cannot be parsed as a Boolean value.", key));
        }

        /// <summary>
        /// Retrieve a int value 
        /// </summary>
        /// <param name="source">Source to check</param>
        /// <param name="key">Key the value is stored under</param>
        /// <exception cref="FormatException">Thrown if the value cannot be parsed into a int</exception>
        public static int GetInt32(this IConfigurationSource source, string key) {
            var val = source.GetValue(key);
            if (int.TryParse(val, out var result)) {
                return result;
            }
            throw new FormatException(
                string.Format("Configuration value at key '{0}' cannot be parsed as a Int32 value.", key));
        }

        /// <summary>
        /// Retrieve a DateTime value 
        /// </summary>
        /// <param name="source">Source to check</param>
        /// <param name="key">Key the value is stored under</param>
        /// <exception cref="FormatException">Thrown if the value cannot be parsed into a DateTime</exception>
        public static DateTime GetDateTime(this IConfigurationSource source, string key) {
            var val = source.GetValue(key);
            if (DateTime.TryParse(val, out var result)) {
                return result;
            }
            throw new FormatException(
                string.Format("Configuration value at key '{0}' cannot be parsed as a DateTime value.", key));
        }

        /// <summary>
        /// Retrieve a TimeSpan value 
        /// </summary>
        /// <param name="source">Source to check</param>
        /// <param name="key">Key the value is stored under</param>
        /// <exception cref="FormatException">Thrown if the value cannot be parsed into a TimeSpan</exception>
        public static TimeSpan GetTimeSpan(this IConfigurationSource source, string key) {
            var val = source.GetValue(key);
            if (TimeSpan.TryParse(val, out var result)) {
                return result;
            }
            throw new FormatException(
                string.Format("Configuration value at key '{0}' cannot be parsed as a TimeSpan value.", key));
        }

        /// <summary>
        /// Retrieve a Uri value 
        /// </summary>
        /// <param name="source">Source to check</param>
        /// <param name="key">Key the value is stored under</param>
        /// <param name="uriKind">The type of the Uri</param>
        /// <exception cref="FormatException">Thrown if the value cannot be parsed into a TimeSpan</exception>
        public static Uri GetUri(this IConfigurationSource source, string key, UriKind uriKind) {
            var val = source.GetValueWithTokens(key);
            if (Uri.TryCreate(val, uriKind, out var result)) {
                return result;
            }
            throw new FormatException(
                string.Format("Configuration value at key '{0}' cannot be parsed as a Boolean value.", key));
        }

        /// <summary>
        /// Get a configuration value while resolving token replacement
        /// (A token is another configuration key surrounded by curly braces, like <c>"{some:key}"</c>)
        /// </summary>
        /// <param name="source">Configuration source</param>
        /// <param name="key">Key the value is stored under</param>
        /// <returns></returns>
        public static string GetValueWithTokens(this IConfigurationSource source, string key) {
            var value = source.GetValue(key);
            if (string.IsNullOrEmpty(value)) {
                throw new MissingConfigurationException(new string[] { key }, 
                string.Format("Could not find token {{{0}}}", key));
            }
            return ReplaceTokens(source, value);
        }

        private static readonly Regex _replacePattern = new Regex(@"\{([:\w]+)\}", RegexOptions.Compiled);
        private static string ReplaceTokens(IConfigurationSource source, string input) {
            return _replacePattern.Replace(input, match => {
                return source.GetValueWithTokens(match.Groups[1].Value);
            });
        }

    }
}
