using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Configo
{
    public abstract class ConfigNode : TreeNode
    {


        // Because I am cloning, I need a way to keep track of the parent;
        // When you clone, you can't change the parent of the cloned node. 
        // I'll just keep a ref to the cloned parent
        public virtual TreeNode ParentCloneRef { get; set; }
        public virtual TreeNodeCollection NodesCloneRef { get; set; }

        public virtual string Property { get; set; }
        public virtual string PropertyType { get; set; }
        public virtual string Value { get; set; }
        public virtual object BuildObject { get; set; }
        public virtual int NodesToAddCount { get; } = 1;

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

        public virtual bool IsAValidParent()
        {
            return Constants.ParentJSONTypes.Any(t => t == PropertyType);
        }

        public override object Clone()
        {
            var configNode = (ConfigNode)base.Clone();
            configNode.Property = Property;
            configNode.PropertyType = PropertyType;
            configNode.Value = Value;
            configNode.BuildObject = null;
            configNode.ParentCloneRef = Parent;
            configNode.NodesCloneRef = Nodes;
            return configNode;
        }

        public virtual object GetDefaultParentType()
        {
            switch (PropertyType)
            {
                case "array":
                    return new List<object>();
                case "object":
                    return new ExpandoObject() as IDictionary<string, object>;
                default:
                    return null;
            }
        }
        public abstract void AddNodeData<T>(T parentBuild, int rowIdx);
        public abstract void AddNodeToCollection(ICollection<object> list, int rowIdx);
        public abstract void AddNodeToObject(object obj, int rowIdx);
    }
}
