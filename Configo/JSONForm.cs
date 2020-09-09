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
        private void TraverseNode<T>(T parentBuild, ConfigNode configNode)
        {
            if (configNode == null) return;
            configNode.AddNodeData(parentBuild);
            foreach (ConfigNode node in configNode.Nodes)
            {
                switch (configNode.PropertyType)
                {
                    case "object":
                    case "array":
                        if (parentBuild is ExpandoObject parentBuildObj)
                        {
                            var parentDict = parentBuildObj as IDictionary<string, object>;
                            TraverseNode(parentDict?[configNode.Property] ?? parentBuildObj, node);
                        }

                        if (parentBuild is List<object> parentBuildList && parentBuildList.ElementAtOrDefault(0) is ExpandoObject firstElement)
                        {
                            TraverseNode(firstElement, node);
                        }
                        break;
                    default:
                        TraverseNode(parentBuild, node);
                        break;
                }
            }
        }
    }
}
