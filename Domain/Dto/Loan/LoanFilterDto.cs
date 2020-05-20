using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto.Loan
{
    public class LoanFilterDto
    {
        public int Id { get; set; }
        public int? LoanTypeId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? LoanStatus { get; set; }
        public int? LastUpdateUserId { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public int UserId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
