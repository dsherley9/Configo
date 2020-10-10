using Newtonsoft.Json.Linq;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Configo
{
    public class ConfigNode : TreeNode
    {

        #region Constructors

        public ConfigNode()
        {
        }

        public ConfigNode(string text) : base(text)
        {
        }

        public ConfigNode(string text, TreeNode[] children) : base(text, children)
        {
        }

        public ConfigNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex)
        {
        }

        public ConfigNode(string text, int imageIndex, int selectedImageIndex, TreeNode[] children) : base(text, imageIndex, selectedImageIndex, children)
        {
        }

        protected ConfigNode(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
        {
        }

        #endregion

        #region Fields
        private string _property;
        private JSONType _propertyType;
        private string _value;
        #endregion

        #region Properties
        public virtual bool PropertyIsBound { get; set; } = false;
        public virtual bool ValueIsBound { get; set; } = false;
        public virtual string Property
        {
            get => _property;
            set
            {
                _property = value;
                UpdateText();
            }
        }
        public virtual JSONType PropertyType
        {
            get => _propertyType;
            set
            {
                _propertyType = value;
                UpdateText();
            }
        }
        public virtual string Value
        {
            get => _value;
            set
            {
                _value = value;
                UpdateText();
            }
        }

        public ConfigNode Root { get; set; }
        public bool IsRoot => Root != null;
        public virtual IDictionary<int, object> BuildObjects { get; set; } = new Dictionary<int, object>();
        public virtual int NodesToAddCount { get; set; } = 1;
        public bool IsAParentType => Constants.ParentJSONTypes.Any(t => t == PropertyType);
        #endregion

        #region Methods

        protected virtual void UpdateText()
        {
            string property = "";
            if (!ConfigHelper.IsGuid(Property))
            {
                property = PropertyIsBound ? $"{{{_property}}}" : _property;
            }
            Text = $"[{_propertyType}] {property}";
            if (!string.IsNullOrWhiteSpace(_value))
            {
                Text += $": {{{_value}}}";
            }
        }

        public virtual bool IsAValidParent()
        {
            return Constants.ParentJSONTypes.Any(t => t == PropertyType);
        }

        public virtual object CreateParentType()
        {
            if (!IsAParentType)
            {
                throw new Exception($"CreateParentType - Unable to create parent type of {PropertyType}.");
            }

            return ConfigHelper.CreateDefaultJSONType(PropertyType);
        }

        public void Build()
        {
            if (IsAParentType)
            {
                AddParentType();
            }
            else
            {
                AddNodeData();
            }
        }

        public virtual void AddParentType()
        {
            for (int i = 0; i < NodesToAddCount; i++)
            {
                AddParentType(i);
                ConnectToParent(i);
            }
        }

        public virtual void AddParentType(int index)
        {
            if (BuildObjects.ContainsKey(index)) { return; }

            var parentType = CreateParentType();
            var kv = new KeyValuePair<string, object>(Property, parentType);
            BuildObjects[index] = kv;
        }

        public virtual void AddNodeData()
        {
            for (int i = 0; i < NodesToAddCount; i++)
            {
                AddNodeData(i);
                ConnectToParent(i);
            }
        }

        public virtual void AddNodeData(int index)
        {
            var args = new object[2]
            {
                Property,
                null
            };

            switch (PropertyType)
            {
                case JSONType.@string:
                    args[1] = (string)Value;
                    break;
                case JSONType.number:
                    args[1] = int.Parse(Value);
                    break;
                case JSONType.boolean:
                    args[1] = bool.Parse(Value);
                    break;
            }

            var obj = ConfigHelper.CreateDefaultJSONType(PropertyType, args);
            BuildObjects[index] = obj;
        }

        public virtual void ConnectToParent(int index)
        {
            var parent = Parent as ConfigNode ?? Root;
            parent.AddChildBuildItemReference(index, this);
        }

        public virtual void AddChildBuildItemReference(int index, ConfigNode child)
        {
            if (!IsAParentType) { throw new Exception("AddChildBuildItemReference - Not a parent!"); }

            var childBuildItem = child?.BuildObjects?[index];
            if (childBuildItem == null) throw new Exception($"AddChildBuildItemReference - Unable to add a null child build item reference.");
            if (childBuildItem is KeyValuePair<string, object> ckv) // Doesn't work with value type == string.. need to figure out how to use with generic. Maybe pass the Type via a ref <TInProp, TInValue>
            {
                childBuildItem = (PropertyType == JSONType.array) ? ckv.Value : ckv; // Array's don't have properties ;)
            }

            var buildItem = BuildObjects?[index];
            if (buildItem == null)
            {
                AddParentType(index);
                buildItem = BuildObjects[index];
            }

            if (buildItem is KeyValuePair<string, object> kv)
            {
                buildItem = kv.Value;
            }

            
            if (buildItem is IDictionary<string, object> dict)
            {
                var childKeyValue = new KeyValuePair<string, object>(((dynamic)childBuildItem).Key, ((dynamic)childBuildItem).Value);
                dict.Add(childKeyValue);
            } else if (buildItem is ICollection<object> col)
            {
                col.Add(childBuildItem);            
            }
        }

        #endregion

    }
}
