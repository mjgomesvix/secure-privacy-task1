using System.Reflection;

namespace Support.Extensions
{
    public static class ObjectExtension
    {
        public static bool IsNull(this object objeto)
        {
            return objeto == null;
        }

        public static bool IsNotNull(this object objeto)
        {
            return objeto != null;
        }

        /// <summary>Fills private members of the Class</summary>
        /// <typeparam name="TClass">Object Type</typeparam>
        /// <typeparam name="T">Value Type</typeparam>
        /// <param name="obj">Object</param>
        /// <param name="privateAttributeName">Object's private attribute name</param>
        /// <param name="value">value to be assigned</param>
        public static TClass ObjectPrivateAttributeFill<TClass, T>(this TClass obj, string privateAttributeName,
            T value)
        {
            var fieldInfo = typeof(TClass).GetField(privateAttributeName, BindingFlags.NonPublic | BindingFlags.Instance);

            fieldInfo?.SetValue(obj, value);

            return obj;
        }

        public static bool AreEquals(this object obj1, object obj2)
        {
            if (obj1 == null) return obj2 == null;

            return obj1.Equals(obj2);
        }
    }
}
