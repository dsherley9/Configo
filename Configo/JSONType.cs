using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configo
{
    public enum JSONType
    {
        none,
        @string,
        number,
        boolean,
        @object,
        array
    }

    public enum NodeType
    {
        none,
        Text,
        Excel
    }
}
