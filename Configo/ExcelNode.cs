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

        public override object AddNodeData(object inputBuildObject, int rowIdx)
        {
            // For testing
            var p = Parent as ConfigNode ?? ParentCloneRef as ConfigNode;
            var pp = p?.Parent as ConfigNode ?? p.ParentCloneRef as ConfigNode;
            var pBO = p?.BuildObject as IDictionary<string, object>;
            var ppBO = pp?.BuildObject as IDictionary<string, object>;
            var n = ppBO != null && ppBO.ContainsKey("n") ? ppBO?["n"] : null;

            var parentBuild = inputBuildObject as IDictionary<string, object>;
            var columnIdx = ExcelSheet.Data.Columns.IndexOf(_boundToColumn);
            var value = (columnIdx > -1 && rowIdx > -1) ?
                ExcelSheet?.Data?.Rows?[rowIdx]?[columnIdx] : null;
            parentBuild[Property] = value;

           
            return value;
        }
    }
}
