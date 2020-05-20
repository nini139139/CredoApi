using Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto.Loan
{
    public class LoanSearchDto
    {
        public string LoanTypeName { get; set; }
        public string UserName { get; set; }
        public string LoanStatusName { get; set; }
        public string CurrencyName { get; set; }
        public string ValutaName { get; set; }
        public int Id { get; set; }
        public int? LoanTypeId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? UserId { get; set; }
        public int? LastUpdateUserId { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? Valuta { get; set; }
        public int? LoanStatus { get; set; }

    }
}
