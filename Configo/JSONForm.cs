using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Configo
{
    public partial class JSONForm : Form
    {
        private Main _parentForm;
        public JSONForm(Main parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
            BuildJsonTree();
        }

        private void BuildJsonTree()
        {
            var obj = TransformTree();
            txtJSON.Text = JsonConvert.SerializeObject(obj, Formatting.Indented);
        }


        // Used to hit each root node
        private object TransformTree()
        {
            var nodes = _parentForm?.ConfigTree?.Nodes;
            var root = new ExpandoObject() as IDictionary<string, object>;
            foreach (TreeNode n in nodes)
            {
                var configNode = (ConfigNode)n;
                TraverseNode(root, configNode);
            }

            return root;
        }

        // Used to traverse each root node recursively
        private void TraverseNode<T>(T inputBuildObject, ConfigNode configNode, bool isAClonedNode = false, int cloneIndex = 0)
        {
            if (configNode == null) return;

            object newParentObj = null;
            bool isAParentType = Constants.ParentJSONTypes.Any(t => t == configNode?.PropertyType);
            if (isAParentType)
            {
                newParentObj = configNode.AddParentType(inputBuildObject);
            }
            else 
            {
                newParentObj = configNode.AddNodeData(inputBuildObject, cloneIndex);

                if (!isAClonedNode && configNode.NodesToAddCount > 1)
                {
                    ExpandNode(inputBuildObject, configNode);
                }
            }

            var childNodes = configNode.Nodes ?? configNode.NodesCloneRef;
            foreach (ConfigNode node in childNodes)
            {
                TraverseNode(newParentObj, node, isAClonedNode, cloneIndex);
            }
        }

        private void ExpandNode<T>(T inputBuildObject, ConfigNode configNode)
        {
            var pNode = configNode.Parent as ConfigNode ?? configNode.ParentCloneRef as ConfigNode;
            if (!Constants.ParentJSONTypes.Any(x => x == pNode?.PropertyType))
            {
                return;
            }

            var ppNode = pNode?.Parent as ConfigNode ?? pNode.ParentCloneRef as ConfigNode;

            for (int i = 1; i < configNode.NodesToAddCount; i++)
            {
                var parentNodeClone = pNode.Clone() as ConfigNode;
                TraverseNode(ppNode.BuildObject, parentNodeClone, true, i);
            }
            return;
        }

    }
}
