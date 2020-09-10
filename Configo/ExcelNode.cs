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
        public override int NodesToAddCount => GetCountOfBoundItems();
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

        private int GetCountOfBoundItems()
        {
            if (ExcelSheet?.Data?.Columns == null) { return 1; }

            // If column doesn't exist, it's only 1;
            var columnIdx = ExcelSheet.Data.Columns.IndexOf(_boundToColumn);
            if (columnIdx < 0) { return 1; }

            return ExcelSheet.Data.Rows.Count;
        }

        public override object Clone()
        {
            var excelNode = (ExcelNode)base.Clone();
            excelNode.ExcelSheet = ExcelSheet;
            excelNode.PropertyIsBoundToColumn = PropertyIsBoundToColumn;
            return excelNode;
        }


        private void AddParentType<TIn>(TIn parentBuild)
        {
            var parentDict = parentBuild as IDictionary<String, Object>;
            var parentList = parentBuild as List<object>;

            if (parentDict != null)
            {
                BuildObject = GetDefaultParentType();
                parentDict[Property] = BuildObject;
            }

            if (parentList != null)
            {
                BuildObject = GetDefaultParentType();
                parentList.Add(BuildObject);
            }
        }

        public override void AddNodeData<T>(T parentBuild, int rowIdx)
        {
            if (Constants.ParentJSONTypes.Any(p => p == PropertyType))
            {
                AddParentType(parentBuild);
                return;
            }

            if (parentBuild is ICollection<object> collection)
            {
                AddNodeToCollection(collection, rowIdx);
                return;
            }

            AddNodeToObject(parentBuild, rowIdx);
        }

        public override void AddNodeToCollection(ICollection<object> list, int rowIdx)
        {
            var parentBuild = list as List<object>;
            var parent = Parent as ConfigNode ?? ParentCloneRef as ConfigNode;
            if (parent?.PropertyType == "object")
            {
                var existingItem = parentBuild.ElementAtOrDefault(rowIdx) as ExpandoObject;
                var itemObj = existingItem ?? new ExpandoObject();
                AddNodeToObject(itemObj, rowIdx);
                if (existingItem == null) { parentBuild.Add(itemObj); }
                return;
            }

            var columnIdx = ExcelSheet.Data.Columns.IndexOf(_boundToColumn);
            var value = (columnIdx > -1 && rowIdx > -1) ?
                ExcelSheet?.Data?.Rows?[rowIdx]?[columnIdx] : null;

            parentBuild.Add(value);        
        }

        public override void AddNodeToObject(object obj, int rowIdx)
        {
            var parentBuild = obj as IDictionary<string, object>;
            var columnIdx = ExcelSheet.Data.Columns.IndexOf(_boundToColumn);
            var value = (columnIdx > -1 && rowIdx > -1) ?
                ExcelSheet?.Data?.Rows?[rowIdx]?[columnIdx] : null;
            parentBuild[Property] = value;
        }
    }
}
