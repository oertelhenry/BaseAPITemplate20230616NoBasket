using System.Reflection;

namespace System
{
    public static class ObjectExtensions
    {
        private static readonly MethodInfo CloneMethod = typeof(Object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

        public static Object DeepClone(this Object obj)
        {
            return DeepClone_Internal(obj, new Dictionary<Object, Object>(new Mobalyz.Extensions.ReferenceEqualityComparer()));
        }

        public static T DeepClone<T>(this T obj)
        {
            return (T)DeepClone((Object)obj);
        }

        public static bool IsPrimitive(this Type type)
        {
            if (type == typeof(String))
            {
                return true;
            }

            return (type.IsValueType && type.IsPrimitive);
        }

        public static bool PerformValueTypeChecks<T>(this object obj)
        {
            if (typeof(T) == typeof(int) && int.TryParse(obj.ToString(), out int intCast))
            {
                return true;
            }

            if (typeof(T) == typeof(long) && long.TryParse(obj.ToString(), out long longCast))
            {
                return true;
            }

            if (typeof(T) == typeof(double) && double.TryParse(obj.ToString(), out double doubleCast))
            {
                return true;
            }

            if (typeof(T) == typeof(decimal) && decimal.TryParse(obj.ToString(), out decimal decimalCast))
            {
                return true;
            }

            if (typeof(T) == typeof(bool) && bool.TryParse(obj.ToString(), out bool boolCast))
            {
                return true;
            }

            if (typeof(T) == typeof(DateTime) && DateTime.TryParse(obj.ToString(), out DateTime datetimeCast))
            {
                return true;
            }

            return false;
        }

        public static object PerformValueTypeConversion<T>(this object obj)
        {
            if ((typeof(T) == typeof(int) || typeof(T) == typeof(int?)) && int.TryParse(obj.ToString(), out int intCast))
            {
                return intCast;
            }

            if ((typeof(T) == typeof(long) || typeof(T) == typeof(long?)) && long.TryParse(obj.ToString(), out long longCast))
            {
                return longCast;
            }

            if ((typeof(T) == typeof(double) || typeof(T) == typeof(double?)) && double.TryParse(obj.ToString(), out double doubleCast))
            {
                return doubleCast;
            }

            if ((typeof(T) == typeof(decimal) || typeof(T) == typeof(decimal?)) && decimal.TryParse(obj.ToString(), out decimal decimalCast))
            {
                return decimalCast;
            }

            if ((typeof(T) == typeof(bool) || typeof(T) == typeof(bool?)) && bool.TryParse(obj.ToString(), out bool boolCast))
            {
                return boolCast;
            }

            if ((typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime?)) && DateTime.TryParse(obj.ToString(), out DateTime datetimeCast))
            {
                return datetimeCast;
            }

            return null;
        }

        public static T Reset<T>(this T value)
        {
            value = default;
            return value;
        }

        private static void CopyFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> filter = null)
        {
            foreach (FieldInfo fieldInfo in typeToReflect.GetFields(bindingFlags))
            {
                if (filter != null && filter(fieldInfo) == false) continue;
                if (IsPrimitive(fieldInfo.FieldType)) continue;
                var originalFieldValue = fieldInfo.GetValue(originalObject);
                var clonedFieldValue = DeepClone_Internal(originalFieldValue, visited);
                fieldInfo.SetValue(cloneObject, clonedFieldValue);
            }
        }

        private static Object DeepClone_Internal(Object obj, IDictionary<Object, Object> visited)
        {
            //if (obj == null) return null;
            //var typeToReflect = obj.GetType();
            //if (IsPrimitive(typeToReflect)) return obj;
            //if (visited.ContainsKey(obj)) return visited[obj];
            //if (typeof(Delegate).IsAssignableFrom(typeToReflect)) return null;
            //var cloneObject = CloneMethod.Invoke(obj, null);
            //if (typeToReflect.IsArray)
            //{
            //    var arrayType = typeToReflect.GetElementType();
            //    if (IsPrimitive(arrayType) == false)
            //    {
            //        Array clonedArray = (Array)cloneObject;
            //        clonedArray.ForEach((array, indices) => array.SetValue(DeepClone_Internal(clonedArray.GetValue(indices), visited), indices));
            //    }
            //}
            //visited.Add(obj, cloneObject);
            //CopyFields(obj, visited, cloneObject, typeToReflect);
            //RecursiveCopyBaseTypePrivateFields(obj, visited, cloneObject, typeToReflect);
            //return cloneObject;
            return default;
        }

        private static void RecursiveCopyBaseTypePrivateFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect)
        {
            if (typeToReflect.BaseType != null)
            {
                RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
                CopyFields(originalObject, visited, cloneObject, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate);
            }
        }
    }
}