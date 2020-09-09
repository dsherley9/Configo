using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Configo
{
    public abstract class ConfigNode : TreeNode
    {
        public virtual string Property { get; set; }
        public virtual string PropertyType { get; set; }
        public virtual string Value { get; set; }
        public virtual object BuildObject { get; set; }

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

        public abstract void AddNodeData<TIn>(TIn key);
    }
}
