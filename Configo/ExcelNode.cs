using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
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

        private string BuildJsonItem(DataRow optDataRow = null)
        {
            optDataRow = optDataRow != null ? optDataRow : ExcelSheet?.Data?.Rows?[0];
            var columnIndex = ExcelSheet.Data.Columns.IndexOf(_boundToColumn);
            return $"\"{Property}\": \"{optDataRow[columnIndex]}\"";

            //switch (_propertyType)
            //{
            //    case "string":
            //        return $"\"{Property}\": \"{optDataRow[columnIndex]}\"";
            //        break;
            //    case "number":
            //        return "";
            //        break;
            //    case "boolean":
            //        return "";
            //        break;
            //    case "object":
            //        return "";
            //        break;
            //    case "array":
            //        return "";
            //        break;
            //    default:
            //        return null;
            //        break;
            //}
        }

        private string BuildJsonItems(ExcelNode parent)
        {
            JContainer jItems;
            if (parent.PropertyType == "object")
            {
                jItems = new JObject();
            }
            else
            {
                jItems = new JArray();
            }

            foreach (DataRow row in ExcelSheet.Data.Rows)
            {
                var data = BuildJsonItem(row);
                jItems.Add(data);
            }

            return jItems.ToString(Newtonsoft.Json.Formatting.None);
        }

        public string GetBoundData()
        {
            if(Constants.ParentJSONTypes.Any(t => t == PropertyType)) { return null; }

            var parent = (ExcelNode)Parent;
            return Constants.ParentJSONTypes.Any(t => t == parent?.PropertyType) ?
                BuildJsonItems(parent) :
                BuildJsonItem();
        }

        public override void AddNodeData(out JToken json)
        { 
            throw new NotImplementedException();
        }

        private void UpdateText()
        {
            var property = PropertyIsBoundToColumn ? $"{{{_property}}}" : _property;
            Text = $"[{_propertyType}] {property}";
            if (!string.IsNullOrWhiteSpace(_boundToColumn))
            {
                Text += $": {{{_boundToColumn}}}";
            }
        }
    }
}
