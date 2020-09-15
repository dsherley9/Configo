using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
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
        private ConfigNode _root = new ConfigNode()
        {
            Text = "Root",
            PropertyType = JSONType.@object
        };
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
            var rootObj = new Dictionary<string, object>();
            _root.BuildObjects.Add(0, rootObj);

            foreach (TreeNode n in nodes)
            {
                var configNode = (ConfigNode)n;
                configNode.Root = _root;
                TraverseNode(configNode);
            }

            return rootObj;
        }

        // Used to traverse each root node recursively
        private void TraverseNode(ConfigNode node)
        {
            if (node == null) return;
            node.Build();

            foreach (ConfigNode childNode in node?.Nodes)
            {
                TraverseNode(childNode);
            }
        }
    }
}
