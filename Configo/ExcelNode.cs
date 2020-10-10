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

        #region Properties
        public ExcelSheet ExcelSheet { get; set; }
        public override int NodesToAddCount => GetCountOfBoundItems();

        #endregion

        public override void AddNodeData(int index)
        {
            var args = new object[2]
            {
                Property,
                null
            };

            var columnIdx = ExcelSheet.Data.Columns.IndexOf(Value);
            var value = (columnIdx > -1 && index > -1) ?
                ExcelSheet?.Data?.Rows?[index]?[columnIdx] : null;

            switch (PropertyType)
            {
                case JSONType.@string:
                    args[1] = (string)value;
                    break;
                case JSONType.number:
                    args[1] = int.Parse(value?.ToString());
                    break;
                case JSONType.boolean:
                    args[1] = bool.Parse(value?.ToString());
                    break;
            }

            var obj = ConfigHelper.CreateDefaultJSONType(PropertyType, args);
            BuildObjects[index] = obj;
        }

        private int GetCountOfBoundItems()
        {
            if (ExcelSheet?.Data?.Columns == null) { return 1; }

            // If column doesn't exist, it's only 1;
            var columnIdx = ExcelSheet.Data.Columns.IndexOf(Value);
            if (columnIdx < 0) { return 1; }

            return ExcelSheet.Data.Rows.Count;
        }
    }
}
