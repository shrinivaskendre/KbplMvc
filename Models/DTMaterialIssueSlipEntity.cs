using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KbplMvc.Controllers
{
    class DTMaterialIssueSlipEntity
    {
        public string Itemcode { get; set; }
        public string Itemdesc { get; set; }
        public string Unit { get; set; }
        public double AvailableQty { get; set; }
        public double IssuedQty { get; set; }
        public string IssuePurpose { get; set; }
    }
}
