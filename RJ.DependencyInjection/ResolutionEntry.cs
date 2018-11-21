using System;
using System.Collections.Generic;

namespace RJ.DependencyInjection {
    public partial class SimpleDependencyResolver {
        internal class ResolutionEntry : IEquatable<ResolutionEntry> {

            public ResolutionEntry(Type type)
                : this(type, type.FullName) { }
            public ResolutionEntry(Type type, string name) {
                Type = type ?? throw new ArgumentNullException(nameof(type));

                if (string.IsNullOrEmpty(name)) {
                    throw new ArgumentException("name cannot be null", nameof(name));
                }

                Name = name;
            }

            public Type Type { get; set; }
            public string Name { get; set; }

            public override bool Equals(object obj) {
                return Equals((ResolutionEntry)obj);
            }

            public bool Equals(ResolutionEntry other) {
                return EqualityComparer<Type>.Default.Equals(Type, other.Type)
                       && Name == other.Name;
            }

            public override int GetHashCode() {
                var hashCode = -1979447941;
                hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(Type);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
                return hashCode;
            }

            public static bool operator ==(ResolutionEntry entry1, ResolutionEntry entry2) {
                return EqualityComparer<ResolutionEntry>.Default.Equals(entry1, entry2);
            }

            public static bool operator !=(ResolutionEntry entry1, ResolutionEntry entry2) {
                return !(entry1 == entry2);
            }

            public override string ToString() {
                return string.Format("{0} ({1})", Name, Type.FullName);
            }
        }
    }
}
