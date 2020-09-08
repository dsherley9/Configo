using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configo
{
    public class ExcelWorkbook
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public List<ExcelSheet> ExcelSheets { get; set; }

        public bool IsValid()
        {
            return Constants.AcceptableExtensions.Any(ext => ext == Extension);
        }
    }
}
