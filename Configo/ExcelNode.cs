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
