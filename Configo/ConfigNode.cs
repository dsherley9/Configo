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
            var property = PropertyIsBound ? $"{{{_property}}}" : _property;
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
            BuildObjects[index] = parentType;
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
            parent.AddChildBuildItemReference(index, BuildObjects[index]);
        }

        public virtual void AddChildBuildItemReference<T>(int index, T o)
        {
            if (!IsAParentType) { throw new Exception("AddChildBuildItemReference - Not a parent!"); }

            var buildItem = (PropertyType == JSONType.array) ? BuildObjects?[0] : BuildObjects?[index];
            if (buildItem == null)
            {
                AddParentType(index);
                buildItem = BuildObjects[index];
            }

            if (buildItem is ICollection<T> col)
            {
                col.Add((T)o);
            }
            else if (buildItem is IDictionary<string, object> dict)
            {
                var kv = new KeyValuePair<string, object>(((dynamic)o).Key, ((dynamic)o).Value);
                dict.Add(kv);
            }
        }

        #endregion

    }
}
