using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EInfrastructure.Core.Config.EnumerationExtensions.Common;

namespace EInfrastructure.Core.Config.EnumerationExtensions.SeedWork.Configurations
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public abstract class Enumeration<T1, T2> : IComparable
        where T1 : IComparable
        where T2 : IComparable
    {
        /// <summary>
        ///
        /// </summary>
        public T1 Id { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public T2 Name { get; private set; }

        protected Enumeration(T1 id, T2 name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() => Name?.ToString() ?? "";

        public static IEnumerable<T> GetAll<T>() where T : Enumeration<T1, T2>
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration<T1, T2>;

            if (otherValue == null)
                return false;

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => UniqueCommon.GetHashCode(Id.ToString());

        public static T FromValue<T>(T1 value) where T : Enumeration<T1, T2>
        {
            var matchingItem = Parse<T, T1>(value, "value", item => item.Id.Equals(value));
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration<T1, T2>
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name.Equals(displayName));
            return matchingItem;
        }

        private static T Parse<T, TK>(TK value, string description, Func<T, bool> predicate)
            where T : Enumeration<T1, T2>
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);
            return matchingItem;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration<T1, T2>) other).Id);
    }
}
