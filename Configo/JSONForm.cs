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

        private void ExpandConfigTree()
        {

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
        private void TraverseNode<T>(T parentBuild, ConfigNode configNode, bool isAdditionalParallelNode = false, int parallelIdx = 0, ICollection<object> parrallelParentCollection = null)
        {
            if (configNode == null) return;

            configNode.AddNodeData(parentBuild, parallelIdx);

            if (parrallelParentCollection != null)
            {
                configNode.AddNodeToCollection(parrallelParentCollection, parallelIdx);
            }


            if (!isAdditionalParallelNode)
            { // Check for more nodes

                ICollection<object> ppCollection = null;
                int countOfNodesToAdd = configNode.NodesToAddCount;
                
                ConfigNode p = configNode?.Parent as ConfigNode;
                ConfigNode pp = p?.Parent as ConfigNode;

                if (p?.PropertyType == "object"
                    && pp?.PropertyType == "array")
                {
                    ppCollection = pp.BuildObject as ICollection<object>;
                    if (ppCollection != null && ppCollection.Count > configNode.NodesToAddCount)
                    {
                        countOfNodesToAdd = ppCollection.Count; 
                        // If the collection is greater than what the node suggests we need to add
                        // we override with the collection size to make sure each object has the same properties.
                    }
                }

                for (int i = 1; i < countOfNodesToAdd; i++)
                {
                   var clonedNode = (ConfigNode)configNode.Clone();

                   TraverseNode(parentBuild, clonedNode, true, i, ppCollection);
                }
            }

            var childNodes = configNode.Nodes ?? configNode.NodesCloneRef;
            foreach (ConfigNode node in childNodes)
            {
                TraverseNode(configNode.BuildObject, node);
            }
        }
    }
}
