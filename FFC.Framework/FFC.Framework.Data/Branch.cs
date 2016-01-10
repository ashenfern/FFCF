//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FFC.Framework.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Branch
    {
        public Branch()
        {
            this.Transactions = new HashSet<Transaction>();
            this.BranchesCosts = new HashSet<BranchesCost>();
            this.BranchesCosts1 = new HashSet<BranchesCost>();
        }
    
        public int BranchID { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
    
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<BranchesCost> BranchesCosts { get; set; }
        public virtual ICollection<BranchesCost> BranchesCosts1 { get; set; }
    }
}
