using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            TransformTree();
        }

        // Used to hit each root node
        private void TransformTree()
        {
            var nodes = _parentForm?.ConfigTree?.Nodes;
            dynamic json = new JObject();
            foreach (TreeNode n in nodes)
            {
               TraverseNode(json, n);
            }
        }

        // Used to traverse each node recursively
        private void TraverseNode(JToken json, TreeNode treeNode)
        {
            if (treeNode == null) return;
            var configNode = (ConfigNode)treeNode;
            configNode.AddNodeData(out json);
            foreach (TreeNode tn in treeNode.Nodes)
            {
                TraverseNode(json[configNode.Property], tn);
            }
        }
    }
}
