using System;
using System.Collections.Generic;

namespace Configo
{
    public static class Constants
    {
        public static readonly string[] ExcelNameEndWithCharacters = { "'", "$" };
        public static readonly JSONType[] ParentJSONTypes = { JSONType.@object , JSONType.array };
        public static readonly Dictionary<JSONType, Type> JSONDefaultTypesMap = new Dictionary<JSONType, Type> {
            { JSONType.@string, typeof(KeyValuePair<string, string>) },
            { JSONType.number,  typeof(KeyValuePair<string, int>)    },
            { JSONType.boolean, typeof(KeyValuePair<string, bool>)   },
            { JSONType.@object, typeof(Dictionary<string, object>)   },
            { JSONType.array,   typeof(List<object>)}
        };
        public static readonly JSONType[] AcceptsValues = { JSONType.@string, JSONType.number, JSONType.boolean };

        public static readonly string[] AcceptableExtensions = { ".xls", ".xlsx" };
        public static readonly Dictionary<NodeType, Type> NodeTypesMap = new Dictionary<NodeType, Type> {
            { NodeType.Text, typeof(ConfigNode) },
            { NodeType.Excel, typeof(ExcelNode) }
        };
    }
}
