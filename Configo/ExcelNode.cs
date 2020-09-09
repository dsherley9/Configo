using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Configo
{
    public class ExcelNode : ConfigNode
    {
        #region Fields
        private string _property;
        private string _propertyType;
        private string _boundToColumn;
        #endregion

        #region Properties
        public ExcelSheet ExcelSheet { get; set; }
        public bool PropertyIsBoundToColumn { get; set; } = false;
        public override string Property {
            get => _property;
            set {
                _property = value;
                UpdateText();
            }
        }
        public override string PropertyType
        {
            get => _propertyType;
            set
            {
                _propertyType = value;
                UpdateText();
            }
        }
        public override string Value
        {
            get => _boundToColumn;
            set
            {
                _boundToColumn = value;
                UpdateText();
            }
        }
        #endregion

        private void UpdateText()
        {
            var property = PropertyIsBoundToColumn ? $"{{{_property}}}" : _property;
            Text = $"[{_propertyType}] {property}";
            if (!string.IsNullOrWhiteSpace(_boundToColumn))
            {
                Text += $": {{{_boundToColumn}}}";
            }
        }

        private void AddToObject(ExpandoObject parent)
        {
            var parentDict = parent as IDictionary<String, Object>;
            var columnIdx = ExcelSheet.Data.Columns.IndexOf(_boundToColumn);
            var ParentNode = (ConfigNode)Parent;
            var ParentsParentNode = (ConfigNode)Parent?.Parent;
            var isListBinding = ParentsParentNode.BuildObject is List<object>;
            var count = 0;

            foreach (DataRow row in ExcelSheet?.Data.Rows)
            {
                if (count == 0)
                {
                    parentDict[Property] = row[columnIdx];
                    if (!isListBinding) { break; } // Not a list, stop here
                    count++;
                    continue;
                }                

                if (ParentNode.PropertyType == "object")
                {
                    var parentList = ParentsParentNode.BuildObject as List<object>;
                    var itemObj = parentList.ElementAtOrDefault(count) as ExpandoObject ?? new ExpandoObject();
                    var itemDict = itemObj as IDictionary<string, object>;
                    itemDict[Property] = row[columnIdx];
                    parentList.Add(itemObj);
                    count++;
                    continue;
                }
            }
            return;
        }

        private void AddToList(List<object> parent)
        {
            var parentList = parent as List<object>;
            foreach (DataRow row in ExcelSheet?.Data.Rows)
            {
                var columnIdx = ExcelSheet.Data.Columns.IndexOf(_boundToColumn);
                parentList.Add(row[columnIdx]);
            }
            return;
        }

        private void AddParentType<TIn>(TIn key)
        {
            var parentDict = key as IDictionary<String, Object>;
            var parentList = key as List<object>;

            if (parentDict != null && PropertyType == "array")
            {
                BuildObject = new List<object>();
                parentDict[Property] = BuildObject;
            }

            if (parentDict != null && PropertyType == "object")
            {
                BuildObject = new ExpandoObject() as IDictionary<string, object>;
                parentDict[Property] = BuildObject;
            }

            if (parentList != null && PropertyType == "array")
            {
                BuildObject = new List<object>();
                parentList.Add(BuildObject);
            }

            if (parentList != null && PropertyType == "object")
            {
                BuildObject = new ExpandoObject() as IDictionary<string, object>;
                parentList.Add(BuildObject);
            }
        }

        public override void AddNodeData<TIn>(TIn key)
        {
            if (Constants.ParentJSONTypes.Any(p => p == PropertyType))
            {
                AddParentType(key);
                return;
            }

            if (key is ExpandoObject)
            {
                AddToObject(key as ExpandoObject);
            }

            if (key is List<object>)
            {
                AddToList(key as List<object>);
            }
        }



    }
}
