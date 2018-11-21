using RJ.Configuration;
using RJ.DependencyInjection;
using RJ.MVC.Configuration;
using RJ.MVC.Data;
using System;

namespace RJ.MVC.Dependencies {
    public static class DependencyResolver {
        private const string ConfigDbConnStringKey = "ConfigDb";
        private static Lazy<SimpleDependencyResolver> _resolverLazy =
            new Lazy<SimpleDependencyResolver>(() => new SimpleDependencyResolver());

        public static SimpleDependencyResolver Instance => _resolverLazy.Value;

        static DependencyResolver() {
            SetupIoc(Instance);
        }

        private static void SetupIoc(SimpleDependencyResolver resolver) {
            var webConfigSource = new ConfigurationManagerConfigurationSource();
            var connFactory = new SqlConnectionFactory(webConfigSource);
            var dbAppConfigSource = new SqlConfigurationSource(webConfigSource.GetValue("self:appId"),
                () => connFactory.CreateConnection(ConfigDbConnStringKey), webConfigSource.GetTimeSpan("config:refreshSpan"));
            var apiGlobalConfigSource = new ConfigurationSourceCache(
                new ApiConfigurationSource(webConfigSource.GetUri("config:apiUrl", UriKind.Absolute)),
                webConfigSource.GetTimeSpan("config:refreshSpan"));
            var configSource = new CompositeConfigurationSource(webConfigSource, dbAppConfigSource, apiGlobalConfigSource) {
                ThrowOnMissing = true
            };
            
            resolver.Register<IConfigurationSource>(configSource);
        }
    }
}
