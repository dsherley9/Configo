using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configo
{
    public static class ConfigHelper
    {

        public static Type GetTypeMapping<TIn>(TIn t, IDictionary<TIn, Type> mapping) where TIn : struct
        {
            mapping.TryGetValue(t, out Type type);
            return type;
        }

        public static object CreateTypeFromMapping<TIn>(TIn t, IDictionary<TIn, Type> mapping, object[] args) where TIn : struct
        {
            var type = GetTypeMapping(t, mapping);
            if (type == null)
            {
                throw new Exception($"CreateTypeFromMapping - Unable to create a type of '{t}'. Could not identify type from {nameof(mapping)}.");
            }

            return Activator.CreateInstance(type, args);
        }

        public static Type GetDefaultJSONType(JSONType jsonType)
        {
            return GetTypeMapping(jsonType, Constants.JSONDefaultTypesMap);
        }

        public static object CreateDefaultJSONType(JSONType jsonType, object[] args = null)
        {
            return CreateTypeFromMapping(jsonType, Constants.JSONDefaultTypesMap, args);
        }

        public static Type GetNodeType(NodeType jsonType)
        {
            return GetTypeMapping(jsonType, Constants.NodeTypesMap);
        }

        public static T CreateNodeType<T>(NodeType jsonType, object[] args = null) where T : ConfigNode
        {
            return (T)CreateTypeFromMapping(jsonType, Constants.NodeTypesMap, args);
        }

        public static T GetEnumValueByString<T>(string value) where T : struct
        {
            return (T)Enum.Parse(typeof(T), value);
        }
    }
}
