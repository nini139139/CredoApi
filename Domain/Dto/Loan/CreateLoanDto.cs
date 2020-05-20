using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Dto.Loan
{
    public class CreateLoanDto
    {
        public int? LoanTypeId { get; set; }
        [Required(ErrorMessage = "თანხა აუცილებელია")]
        public decimal? Amount { get; set; }
        [Required(ErrorMessage = "დაწყების თარიღი აუცილებელია")]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "დასრულების თარიღი აუცილებელია")]
        public DateTime? EndDate { get; set; }
        [Required(ErrorMessage = "სესხის სტატუსი აუცილებელია")]
        public int? LoanStatus { get; set; }
        public int? UserId { get; set; }
        [Required(ErrorMessage = "ვალუტა აუცილებელია")]
        public int? Valuta { get; set; }
        public int? LastUpdateUserId { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
