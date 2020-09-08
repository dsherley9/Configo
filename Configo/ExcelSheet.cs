using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configo
{
    public class ExcelSheet
    {
        public string Name { get; set; }

        private string _display;
        public string Display
        {
            get {
                if (!string.IsNullOrWhiteSpace(_display)) { return _display; }
                SetDisplayValue();
                return _display;
            }
        }

        public DataTable Data { get; set; }


        private void SetDisplayValue()
        {
            if (string.IsNullOrWhiteSpace(Name)) { _display = "Undefined"; return; } // No name??? Stop here
            _display = Name;

            // Remove Excel Special Characters (Beginning and End)
            foreach (string character in Constants.ExcelNameEndWithCharacters)
            {
                if (_display.StartsWith(character))
                {
                    _display = _display.Substring(1, _display.Length - 1);
                }

                if (_display.EndsWith(character))
                {
                    _display = _display.Substring(0, _display.Length - 1);
                }
            }
        }

        public override string ToString()
        {
            return Display;
        }
    }
}
