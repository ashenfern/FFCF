//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestConsole
{
    using System;
    using System.Collections.Generic;
    
    public partial class BranchesCost
    {
        public int SourceBranchID { get; set; }
        public int DestinationBranchID { get; set; }
        public decimal Cost { get; set; }
    
        public virtual Branch Branch { get; set; }
        public virtual Branch Branch1 { get; set; }
    }
}
