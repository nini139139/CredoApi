using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity.LoanEntity
{
    public class LoanType
    {
        public LoanType()
        {
            Loans = new HashSet<Loans>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Loans> Loans { get; set; }
    }
}
