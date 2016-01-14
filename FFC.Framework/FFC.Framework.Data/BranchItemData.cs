using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC.Framework.Data
{
    public class BranchItemData
    {
        public Branch Branch { get; set; }
        public int Now { get; set; }
        public int Forecasted { get; set; }
        public int ExpectedBalance { get; set; }
    }
}
