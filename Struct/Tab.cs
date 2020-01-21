using System;
using System.Collections.Generic;
using System.Text;

namespace NPage.Struct
{
    public struct Tab
    {
        public string Header { get; set; }
        public Action Body { get; set; }
    }
}
