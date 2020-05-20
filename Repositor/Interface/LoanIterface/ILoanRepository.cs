using Domain.Dto.Loan;
using Domain.Entity.LoanEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositor.Interface.LoanIterface
{
    public interface ILoanRepository
    {
        Task<LoanSearchResultDto> GetLoans(LoanFilterDto filters, bool isDownload = false);
        Task<IEnumerable<LoanType>> GetLoanTypes();
        Task<Loans> RegisterLoan(CreateLoanDto loanForCreate);
        Task<string> UpdateLoan(int id, LoanDto loanForUpdate);
        Task<Loans> GetLoanById(int id);
    }
}
