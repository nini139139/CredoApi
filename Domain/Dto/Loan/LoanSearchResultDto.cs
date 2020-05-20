using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto.Loan
{
    public class LoanSearchResultDto
    {
        public IEnumerable<LoanSearchDto> LoanResult { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
    }
}
