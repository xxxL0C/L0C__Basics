using System;
using System.Collections;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace XXXL0C.ExBasics.Editor
{
    internal static class ReflectionUtility
    {
        private const BindingFlags AllInstance =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        public static bool GetMemberBoolValue(object target, string memberName)
        {
            if (target == null || string.IsNullOrEmpty(memberName)) return false;
            Type type = target.GetType();

            var field = type.GetField(memberName, AllInstance);
            if (field != null && field.FieldType == typeof(bool))
                return (bool)field.GetValue(target);

            var prop = type.GetProperty(memberName, AllInstance);
            if (prop != null && prop.PropertyType == typeof(bool) && prop.CanRead)
                return (bool)prop.GetValue(target);

            var method = type.GetMethod(memberName, AllInstance, null, Type.EmptyTypes, null);
            if (method != null && method.ReturnType == typeof(bool))
                return (bool)method.Invoke(target, null);

            Debug.LogWarning($"[XXXL0C] Member '{memberName}' not found or not bool on {type.Name}.");
            return false;
        }

        public static void InvokeMethod(object target, string methodName)
        {
            if (target == null || string.IsNullOrEmpty(methodName)) return;
            Type type   = target.GetType();
            var  method = type.GetMethod(methodName, AllInstance, null, Type.EmptyTypes, null);
            if (method != null)
                method.Invoke(target, null);
            else
                Debug.LogWarning($"[XXXL0C] Method '{methodName}' not found on {type.Name}.");
        }

        public static object GetPropertyOwner(SerializedProperty property)
        {
            string path  = property.propertyPath.Replace(".Array.data[", "[");
            string[] parts = path.Split('.');
            object obj   = property.serializedObject.targetObject;

            for (int i = 0; i < parts.Length - 1; i++)
            {
                string part = parts[i];
                if (part.Contains("["))
                {
                    string name  = part.Substring(0, part.IndexOf('['));
                    int    index = int.Parse(part.Substring(part.IndexOf('[') + 1).TrimEnd(']'));
                    var    fi    = obj?.GetType().GetField(name, AllInstance);
                    var    list  = fi?.GetValue(obj) as IList;
                    obj = list?[index];
                }
                else
                {
                    var fi = obj?.GetType().GetField(part, AllInstance);
                    obj = fi?.GetValue(obj);
                }
                if (obj == null) return null;
            }
            return obj;
        }
    }
}
