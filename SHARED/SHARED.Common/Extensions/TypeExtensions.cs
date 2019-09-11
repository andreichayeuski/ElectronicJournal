using System;

namespace SHARED.Common.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsClosedGeneric(this Type @this, Type genericType)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            if (genericType == null)
            {
                throw new ArgumentNullException("genericType");
            }

            if (!genericType.IsGenericTypeDefinition)
            {
                throw new ArgumentException();
            }

            if (!@this.IsGenericType || @this.IsGenericTypeDefinition)
            {
                return false;
            }

            var genericDefinitin = @this.GetGenericTypeDefinition();
            var result = genericDefinitin == genericType;

            return result;
        }

        /// <summary>
        ///     Checks a type to see if it derives from a raw generic.
        /// </summary>
        /// <param name="this">Extension method target.</param>
        /// <param name="genericType">Raw generic type, i.e generic type definition.</param>
        /// <returns />
        /// <exception cref="ArgumentNullException"><paramref name="@this" /> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="genericType" /> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="genericType" /> is not generic type definition.</exception>
        public static bool IsSubclassOfRawGeneric(this Type @this, Type genericType)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            if (genericType == null)
            {
                throw new ArgumentNullException("genericType");
            }

            if (!genericType.IsGenericTypeDefinition)
            {
                throw new ArgumentException();
            }

            if (@this == genericType)
            {
                return false;
            }

            var p = @this.BaseType;

            while (p != null)
            {
                // use IsConstructedGenericType in .NET 4.5
                if (p.IsGenericType)
                {
                    var pgeneric = p.GetGenericTypeDefinition();
                    if (pgeneric == genericType)
                    {
                        return true;
                    }
                }

                p = p.BaseType;
            }

            return false;
        }

        /// <summary>
        ///     Checks a type to see if it implements specified interface.
        /// </summary>
        /// <param name="this">Extension method target.</param>
        /// <param name="ifaceType">Interface to check implementation of.</param>
        /// <returns />
        /// <exception cref="ArgumentNullException"><paramref name="@this" /> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="ifaceType" /> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="ifaceType" /> is not interface.</exception>
        public static bool ImplementsInterface(this Type @this, Type ifaceType)
        {
            if (@this == null)
            {
                throw new ArgumentNullException();
            }

            if (ifaceType == null)
            {
                throw new ArgumentNullException();
            }

            if (!ifaceType.IsInterface)
            {
                throw new ArgumentException();
            }

            if (@this == ifaceType)
            {
                return false;
            }

            var t = @this;

            while (t != null)
            {
                var interfaces = @this.GetInterfaces();

                foreach (var implementedInterface in interfaces)
                {
                    if (implementedInterface == ifaceType)
                    {
                        return true;
                    }

                    if (implementedInterface.IsGenericType
                        && ifaceType.IsGenericTypeDefinition
                        && implementedInterface.GetGenericTypeDefinition() == ifaceType)
                    {
                        return true;
                    }

                    if (implementedInterface.ImplementsInterface(ifaceType))
                    {
                        return true;
                    }
                }

                t = t.BaseType;
            }

            return false;
        }
    }
}

