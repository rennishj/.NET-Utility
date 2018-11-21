using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RJ.DependencyInjection {
    /// <summary>
    /// A simple no-frills IoC container
    /// </summary>
    public partial class SimpleDependencyResolver {
        private readonly Dictionary<ResolutionEntry, Func<SimpleDependencyResolver, object>> _typeToResolver
                       = new Dictionary<ResolutionEntry, Func<SimpleDependencyResolver, object>>();

        /// <summary>
        /// Registers a transient resolver function
        /// </summary>
        /// <typeparam name="T">Type to register</typeparam>
        /// <param name="factory">The function used to create an instance of <typeparamref name="T"/></param>
        /// <exception cref="RegistrationException"></exception>
        public void Register<T>(Func<SimpleDependencyResolver, T> factory) where T : class {
            var entry = new ResolutionEntry(typeof(T));
            if (_typeToResolver.ContainsKey(entry)) {
                throw new RegistrationException(entry.Type, entry.Name);
            }
            _typeToResolver.Add(entry, factory);
        }

        /// <summary>
        /// Registers the specified instance
        /// </summary>
        /// <typeparam name="T">Type the instance is registered under</typeparam>
        /// <param name="instance">The instance</param>
        /// <exception cref="RegistrationException"></exception>
        public void Register<T>(T instance) where T : class {
            var entry = new ResolutionEntry(typeof(T));
            if (_typeToResolver.ContainsKey(entry)) {
                throw new RegistrationException(entry.Type, entry.Name);
            }
            _typeToResolver.Add(entry, c => instance);
        }

        /// <summary>
        /// Registers the specified instance
        /// </summary>
        /// <typeparam name="T">Type the instance is registered under</typeparam>
        /// <param name="instance">The instance</param>
        /// <param name="name">Name of the registration</param>
        /// <exception cref="RegistrationException"></exception>
        public void Register<T>(T instance, string name) where T : class {
            var entry = new ResolutionEntry(typeof(T), name);
            if (_typeToResolver.ContainsKey(entry)) {
                throw new RegistrationException(entry.Type, entry.Name);
            }
            _typeToResolver.Add(entry, c => instance);
        }

        /// <summary>
        /// Registers a transient resolver function
        /// with the specified name
        /// </summary>
        /// <typeparam name="T">Type to register</typeparam>
        /// <param name="factory">The function used to create an instance of <typeparamref name="T"/></param>
        /// <param name="name">A unique name for this registration</param>
        /// <exception cref="RegistrationException"></exception>
        public void Register<T>(Func<SimpleDependencyResolver, T> factory, string name) where T : class {
            var entry = new ResolutionEntry(typeof(T), name);
            if (_typeToResolver.ContainsKey(entry)) {
                throw new RegistrationException(entry.Type, entry.Name);
            }
            _typeToResolver.Add(entry, factory);
        }

        /// <summary>
        /// Registers a transient resolver function for <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Type to create</typeparam>
        /// <exception cref="RegistrationException"></exception>
        public void Register<T>() where T : class {
            Register<T>((i, r) => { });
        }

        /// <summary>
        /// Registers a transient resolver function for <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Type to create</typeparam>
        /// <param name="configure">Configuration function to modify the created instance</param>
        /// <exception cref="RegistrationException"></exception>
        public void Register<T>(Action<T, SimpleDependencyResolver> configure) where T : class {
            var type = typeof(T);
            var entry = new ResolutionEntry(type);
            if (_typeToResolver.ContainsKey(entry)) {
                throw new RegistrationException(entry.Type, entry.Name);
            }
            _typeToResolver.Add(entry, c => {
                var instance = CreateInstance(type);
                configure(instance as T, this);
                return instance;
            });
        }

        /// <summary>
        /// Registers a transient resolver function for <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Type to create</typeparam>
        /// <exception cref="RegistrationException"></exception>
        public void Register<T>(string name) where T : class {
            Register<T>(name, (i, r) => { });
        }

        /// <summary>
        /// Registers a transient resolver function for <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Type to create</typeparam>
        /// <param name="name">Name to register the resolver under</param>
        /// <param name="configure">Configuration function to modify the created instance</param>
        /// <exception cref="RegistrationException"></exception>
        public void Register<T>(string name, Action<T, SimpleDependencyResolver> configure) where T : class {
            var type = typeof(T);
            var entry = new ResolutionEntry(type, name);
            if (_typeToResolver.ContainsKey(entry)) {
                throw new RegistrationException(entry.Type, entry.Name);
            }
            _typeToResolver.Add(entry, c => {
                var instance = CreateInstance(type);
                configure(instance as T, this);
                return instance;
            });
        }

        /// <summary>
        /// Registers a transient resolver on type <typeparamref name="I"/>
        /// where the resolver returns an instance of type <typeparamref name="T" />
        /// </summary>
        /// <typeparam name="T">Concrete type to resolve</typeparam>
        /// <typeparam name="I">Type to register</typeparam>
        /// <exception cref="RegistrationException"></exception>
        public void Register<I, T>() where I : class where T : class, I {
            Register<I, T>(typeof(I).FullName);
        }

        /// <summary>
        /// Registers a transient resolver on type <typeparamref name="I"/>
        /// where the resolver returns an instance of type <typeparamref name="T" />
        /// </summary>
        /// <typeparam name="T">Concrete type to resolve</typeparam>
        /// <typeparam name="I">Type to register</typeparam>
        /// <param name="configure">Configuration function to modify the created instance</param>
        /// <exception cref="RegistrationException"></exception>
        public void Register<I, T>(Action<T, SimpleDependencyResolver> configure) where I : class where T : class, I {
            Register<I, T>(typeof(I).FullName, (i, r) => { });
        }

        /// <summary>
        /// Registers a transient resolver on type <typeparamref name="I"/>
        /// where the resolver returns an instance of type <typeparamref name="T" />
        /// </summary>
        /// <typeparam name="T">Concrete type to resolve</typeparam>
        /// <typeparam name="I">Type to register</typeparam>
        /// <param name="name">Name to register the resolver under</param>
        /// <exception cref="RegistrationException"></exception>
        public void Register<I, T>(string name) where I : class where T : class, I {
            Register<I, T>(name, (i, c) => { });
        }

        /// <summary>
        /// Registers a transient resolver on type <typeparamref name="I"/>
        /// where the resolver returns an instance of type <typeparamref name="T" />
        /// </summary>
        /// <typeparam name="T">Concrete type to resolve</typeparam>
        /// <typeparam name="I">Type to register</typeparam>
        /// <param name="name">Name to register the resolver under</param>
        /// <param name="configure">Configuration function to modify the created instance</param>
        /// <exception cref="RegistrationException"></exception>
        public void Register<I, T>(string name, Action<T, SimpleDependencyResolver> configure) where I : class where T : class, I {
            var iType = typeof(I);
            var tType = typeof(T);
            var entry = new ResolutionEntry(iType, name);
            if (_typeToResolver.ContainsKey(entry)) {
                throw new RegistrationException(entry.Type, entry.Name);
            }
            _typeToResolver.Add(entry, c => {
                var instance = CreateInstance(tType);
                configure(instance as T, this);
                return instance;
            });
        }

        /// <summary>
        /// Registers a resolver that returns the specified instance
        /// </summary>
        /// <param name="instance">The instance to return</param>
        public void Register(object instance) {
            var entry = new ResolutionEntry(instance.GetType());
            if (_typeToResolver.ContainsKey(entry)) {
                throw new RegistrationException(entry.Type, entry.Name);
            }
            _typeToResolver.Add(entry, c => instance);
        }

        /// <summary>
        /// Registers a resolver that returns the specified instance
        /// </summary>
        /// <param name="instance">The instance to return</param>
        /// <param name="name">Registered name of the instance</param>
        public void Register(object instance, string name) {
            var entry = new ResolutionEntry(instance.GetType(), name);
            if (_typeToResolver.ContainsKey(entry)) {
                throw new RegistrationException(entry.Type, entry.Name);
            }
            _typeToResolver.Add(entry, c => instance);
        }

        /// <summary>
        /// Resolves an instance of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type to resolve</typeparam>
        public T Resolve<T>() where T : class {
            return Resolve(typeof(T)) as T;
        }

        /// <summary>
        /// Resolves an instance of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type to resolve</typeparam>
        public T Resolve<T>(string name) where T : class {
            return Resolve(typeof(T), name) as T;
        }

        /// <summary>
        /// Resolves the specified type
        /// </summary>
        /// <param name="type">The type to resolve</param>
        public object Resolve(Type type) {
            if (!TryResolve(type, out var instance)) {
                throw new ResolutionException(type,
                    string.Format(ExceptionResources.ResolveDefaultNamed, type.FullName));
            }
            return instance;
        }

        /// <summary>
        /// Resolves the specified type
        /// </summary>
        /// <param name="type">The type to resolve</param>
        /// <param name="name">Registered name of the resolver</param>
        public object Resolve(Type type, string name) {
            if (!TryResolve(type, name, out var instance)) {
                throw new ResolutionException(type,
                    string.Format(ExceptionResources.ResolveDefaultNamed, type.FullName));
            }
            return instance;
        }

        /// <summary>
        /// Resolve all instances of the specified type
        /// </summary>
        /// <param name="type">The type to resolve</param>
        public IEnumerable<object> ResolveAll(Type type) {
            var resolved = _typeToResolver.Keys
                .Where(e => type.IsAssignableFrom(e.Type))
                .Select(t => Resolve(t.Type, t.Name));
            return resolved;
        }

        /// <summary>
        /// Resolve all instances of the specified type
        /// </summary>
        /// <typeparam name="T">The type to resolve</typeparam>
        public IEnumerable<T> ResolveAll<T>() {
            return ResolveAll(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// Determines whether this instance can resolve the specified type.
        /// </summary>
        /// <param name="type">The type to check</param>
        /// <returns>
        ///   <c>true</c> if this instance can resolve the specified type; otherwise, <c>false</c>.
        /// </returns>
        public bool CanResolve(Type type) {
            var possibleResolutions = _typeToResolver.Keys.Where(e => type.IsAssignableFrom(e.Type)).ToArray();
            if (possibleResolutions.Length > 1) {
                throw new ResolutionException(type, string.Format(ExceptionResources.MultipleRegistrations));
            }
            var resolveEntry = _typeToResolver.Keys.SingleOrDefault(e => type.IsAssignableFrom(e.Type));
            if (resolveEntry == null) {
                return false;
            }
            if (IsRegistered(resolveEntry)) {
                return true;
            }
            return CanResolve(resolveEntry.Type);
        }

        /// <summary>
        /// Determines whether the specified type is registered.
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>
        ///   <c>true</c> if the specified type is registered; otherwise, <c>false</c>.
        /// </returns>
        public bool IsRegistered(Type type) {
            return IsRegistered(new ResolutionEntry(type));
        }

        /// <summary>
        /// Determines whether the specified type is registered.
        /// </summary>
        /// <param name="type">The type</param>
        /// <param name="name">Registered name of the resolver</param>
        /// <returns>
        ///   <c>true</c> if the specified type is registered; otherwise, <c>false</c>.
        /// </returns>
        public bool IsRegistered(Type type, string name) {
            return IsRegistered(new ResolutionEntry(type, name));
        }

        private bool IsRegistered(ResolutionEntry entry) {
            return _typeToResolver.ContainsKey(entry);
        }

        private bool TryResolve(Type type, out object obj) {
            if (IsRegistered(type)) {
                // return the result of the resolver
                obj = _typeToResolver[new ResolutionEntry(type)](this);
                return true;
            }

            var resolveEntry = _typeToResolver.Keys
                .SingleOrDefault(e => e.Type != type && type.IsAssignableFrom(e.Type));
            if (resolveEntry != null) {
                return TryResolve(resolveEntry.Type, out obj);
            }

            obj = null;
            return false;
        }

        private bool TryResolve(Type type, string name, out object obj) {
            if (IsRegistered(type, name)) {
                // return the result of the resolver
                obj = _typeToResolver[new ResolutionEntry(type, name)](this);
                return true;
            }

            var resolveEntry = _typeToResolver.Keys
                .Where(e => e.Type != type && type.IsAssignableFrom(e.Type))
                .SingleOrDefault(e => e.Name == name);
            if (resolveEntry != null) {
                return TryResolve(resolveEntry.Type, resolveEntry.Name, out obj);
            }

            obj = null;
            return false;
        }

        private object CreateInstance(Type type, bool useDefaultCtors = true) {

            if (type.IsAbstract || type.IsInterface) {
                throw new ResolutionException(type, string.Format(ExceptionResources.CantResolveAbstract));
            }

            var ctors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
            if (useDefaultCtors && !type.IsInterface && !type.IsAbstract && !ctors.Any()) {
                // if there's no constructor defined, then we can just create it
                var instance = Activator.CreateInstance(type);
                var withProps = InjectProperties(type, instance);
                return withProps;
            }

            var possibleResolutions = GetPossibleResolutions(ctors);

            if (!possibleResolutions.Any(p => p.CanResolve)) {
                // get the types of the dependencies we can't resolve
                var unresolvableTypes = possibleResolutions
                    .Where(p => !p.CanResolve)
                    .SelectMany(p => p.Entries
                        .Where(e => (!string.IsNullOrEmpty(e.Name) && !IsRegistered(e.Type, e.Name))
                        || !CanResolve(e.Type)));
                throw new ResolutionException(type,
                    string.Format(ExceptionResources.CantResolveDeps,
                        string.Join(",", unresolvableTypes.Select(e => 
                            string.Format("{0} named '{1}'", e.Type, e.Name)))));
            }

            // If we get a parameterless and a parameterized, prefer the parameterized...
            // TODO: is this the behavior we want?
            if (possibleResolutions.Length == 1 && possibleResolutions.All(pc => pc.Entries.Length == 0)) {
                var instance = Activator.CreateInstance(type);
                var withProps = InjectProperties(type, instance);
                return withProps;
            }

            // ...but if we get more than one parameterized, throw.
            if (possibleResolutions.Length > 1) {
                throw new ResolutionException(type, string.Format(ExceptionResources.MultipleCtors));
            }

            // otherwise, it means we can construct this object from the dependencies in the one constructor
            var resolvedCtorParams = possibleResolutions[0].Entries
                .Select(e => string.IsNullOrEmpty(e.Name) ? Resolve(e.Type) : Resolve(e.Type, e.Name))
                .ToArray();
            var constructed = Activator.CreateInstance(type, resolvedCtorParams);
            var injected = InjectProperties(type, constructed);
            return injected;
        }

        internal object InjectProperties(Type type, object instance) {
            var injectProps = type.GetProperties().Select(t => new {
                Info = t,
                InjectAttribute = t.GetCustomAttribute<InjectAttribute>(),
                t.PropertyType
            }).Where(p => p.InjectAttribute != null);

            foreach (var prop in injectProps) {
                prop.Info.SetValue(
                    instance,
                    string.IsNullOrEmpty(prop.InjectAttribute.DependencyName)
                        ? Resolve(prop.PropertyType)
                        : Resolve(prop.PropertyType, prop.InjectAttribute.DependencyName)
                    );
            }
            return instance;
        }

        internal ResolveResult[] GetPossibleResolutions(IEnumerable<ConstructorInfo> ctors) {
            return ctors.Select(ci => {
                var parameterTypes = ci.GetParameters().Select(pi => new ResolutionEntry(
                    pi.ParameterType,
                    pi.GetCustomAttribute<InjectAttribute>()?.DependencyName
                        ?? (pi.GetCustomAttribute<InjectAttribute>() != null ? pi.Name : pi.ParameterType.FullName)
                )).ToArray();
                // tell me if you can resolve them completely
                var result = new ResolveResult {
                    Entries = parameterTypes,
                    CanResolve = parameterTypes.All(e =>
                        (!string.IsNullOrEmpty(e.Name) && IsRegistered(e.Type, e.Name))
                        || CanResolve(e.Type)),
                };
                return result;
            }).ToArray();
        }

        internal class ResolveResult {
            public ResolutionEntry[] Entries { get; set; }
            public bool CanResolve { get; set; }
        }
    }
}
