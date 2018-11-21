using System;
using System.Linq;
using System.Reflection;

namespace RJ.DependencyInjection {
    /// <summary>
    /// Set of extension methods for extending the functionality of <see cref="SimpleDependencyResolver"/>
    /// </summary>
    public static class SimpleDependencyResolverExtensions {

        /// <summary>
        /// Registers a transient resolver function
        /// </summary>
        /// <typeparam name="T">Type to register</typeparam>
        /// <param name="resolver">Resolver to register this factory to</param>
        /// <param name="factory">The function used to create an instance of <typeparamref name="T"/></param>
        /// <exception cref="RegistrationException"></exception>
        public static void RegisterTransient<T>(this SimpleDependencyResolver resolver, Func<SimpleDependencyResolver, T> factory)
            where T : class {
            resolver.Register(factory);
        }

        /// <summary>
        /// Registers the specified instance
        /// </summary>
        /// <typeparam name="T">Type the instance is registered under</typeparam>
        /// <param name="resolver">Resolver to register this instance to</param>
        /// <param name="instance">The instance</param>
        /// <exception cref="RegistrationException"></exception>
        public static void RegisterSingleton<T>(this SimpleDependencyResolver resolver, T instance)
            where T : class {
            resolver.Register(instance);
        }

        /// <summary>
        /// Resolves an instance of the specified type, or constructs the specified type
        /// by resolving it's dependencies.<para />
        /// </summary>
        /// <param name="resolver">The resolver</param>
        /// <param name="type">The type to resolve</param>
        /// <param name="useDefaultCtors">If true, uses the default constructor of the type to create an instance</param>
        /// <returns><c>null</c> if an instance cannot be created.</returns>
        public static object CreateInstance(this SimpleDependencyResolver resolver, Type type, bool useDefaultCtors = true) {

            if (resolver.IsRegistered(type)) {
                return resolver.Resolve(type);
            }

            if (type.IsAbstract || type.IsInterface) {
                return null;
            }

            var ctors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
            if (useDefaultCtors && !type.IsInterface && !type.IsAbstract && !ctors.Any()) {
                // if there's no constructor defined, then we can just create it
                var instance = Activator.CreateInstance(type);
                var withProps = resolver.InjectProperties(type, instance);
                return withProps;
            }

            var possibleResolutions = resolver.GetPossibleResolutions(ctors);

            if (!possibleResolutions.Any(p => p.CanResolve)) {
                // get the types of the dependencies we can't resolve
                var unresolvableTypes = possibleResolutions
                    .Where(p => !p.CanResolve)
                    .SelectMany(p => p.Entries
                        .Where(e => resolver.IsRegistered(e.Type, e.Name)));
                throw new ResolutionException(type,
                    string.Format(ExceptionResources.CantResolveDeps,
                        string.Join(",", unresolvableTypes.Select(e => 
                        string.Format("{0} named '{1}'", e.Type, e.Name)))));
            }

            // If we get a parameterless and a parameterized, prefer the parameterized...
            // TODO: is this the behavior we want?
            if (possibleResolutions.Length == 1 && possibleResolutions.All(pc => pc.Entries.Length == 0)) {
                var instance = Activator.CreateInstance(type);
                var withProps = resolver.InjectProperties(type, instance);
                return withProps;
            }

            // ...but if we get more than one parameterized, throw.
            if (possibleResolutions.Length > 1) {
                throw new ResolutionException(type, ExceptionResources.MultipleCtors);
            }

            // otherwise, it means we can construct this object from the dependencies in the one constructor
            var resolvedCtorParams = possibleResolutions[0].Entries
                .Select(e => string.IsNullOrEmpty(e.Name) ? resolver.Resolve(e.Type) : resolver.Resolve(e.Type, e.Name))
                .ToArray();
            var constructed = Activator.CreateInstance(type, resolvedCtorParams);
            var injected = resolver.InjectProperties(type, constructed);
            return injected;
        }

        /// <summary>
        /// Resolves an instance of the specified type, or constructs the specified type
        /// by resolving it's dependencies
        /// </summary>
        /// <typeparam name="T">The type to resolve</typeparam>
        /// <param name="resolver">The resolver</param>
        /// <param name="useDefaultCtors">If true, uses the default constructor of the type to create an instance</param>
        public static T CreateInstance<T>(this SimpleDependencyResolver resolver, bool useDefaultCtors = true) {
            var type = typeof(T);
            return (T)CreateInstance(resolver, type, useDefaultCtors);
        }

        /// <summary>
        /// Resolves an instance of the specified type, or constructs the specified type
        /// by resolving it's dependencies
        /// </summary>
        /// <typeparam name="T">The type to resolve</typeparam>
        /// <param name="resolver">The resolver</param>
        /// <param name="useDefaultCtors">If true, uses the default constructor of the type to create an instance</param>
        /// <param name="type">The type to resolve</param>
        /// <returns>An instance of the specified type, casted to type <typeparamref name="T"/></returns>
        public static T CreateInstance<T>(this SimpleDependencyResolver resolver, Type type, bool useDefaultCtors = true) {
            return (T)CreateInstance(resolver, type, useDefaultCtors);
        }
    }
}
