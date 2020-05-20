using AutoMapper;
using Domain.Dto.Loan;
using Domain.Entity.LoanEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositor.Interface;
using Repositor.Interface.EnumInterface;
using Repositor.Interface.LoanIterface;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enumerations.EnumData;

namespace Repositor.Concrete.LoanConcrete
{
    public class LoanRepository : ILoanRepository
    {
        private readonly DataContext _context;
        private readonly IEnumRepository _enumRepo;
        private readonly IMapper _mapper;
        private readonly IcommonRepository _commonRepo;

        public LoanRepository(DataContext context, IEnumRepository enumRepo, IMapper mapper, IcommonRepository commonRepo)
        {
            _context = context;
            _enumRepo = enumRepo;
            _mapper = mapper;
            _commonRepo = commonRepo;
        }

        public async Task<Loans> GetLoanById(int id)
        {
            var loan = await _context.Loans
                .Include(c => c.LoanType)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);

            return loan;
        }

        public async Task<IEnumerable<LoanType>> GetLoanTypes()
        {
            var loanTypes = await _context.LoanType.ToListAsync();
            return loanTypes;
        }


        public async Task<LoanSearchResultDto> GetLoans(LoanFilterDto filter, bool isDownload = false)
        {
            var currentUserRole = _commonRepo.CurrentRole();
            IEnumerable<NameValue> loanStatuses = _enumRepo.GetItems<Domain.Enumerations.RoleType>().Result;
            LoanSearchResultDto loanSearchResult = new LoanSearchResultDto();
            IQueryable<LoanSearchDto> result = Enumerable.Empty<LoanSearchDto>().AsQueryable();

            if (currentUserRole == (int)Domain.Enumerations.RoleType.Admin)
            {

                result = _context.Loans.Where(c => c.LoanStatus != (int)Domain.Enumerations.LoanStatuses.Saved)
               .Include(c => c.LoanType)
               .Include(c => c.User)
           .Select(c => new LoanSearchDto
           {
               Id = c.Id,
               Amount = c.Amount,
               LoanTypeId = c.LoanTypeId,
               LoanTypeName = c.LoanType.Name,
               StartDate = c.StartDate,
               EndDate = c.EndDate,
               UserName = c.User.Name,
               CreateDate = c.CreateDate,
               LastUpdateDate = c.LastUpdateDate,
               ValutaName = _enumRepo.GetItemsByString<Domain.Enumerations.Currency>((int)c.Valuta),
               LoanStatus = c.LoanStatus,
               LoanStatusName = _enumRepo.GetItemsByString<Domain.Enumerations.LoanStatuses>((int)c.LoanStatus),
           });
            }
            else
            {
                result = _context.Loans
               .Include(c => c.LoanType)
               .Include(c => c.User)
           .Select(c => new LoanSearchDto
           {
               Id = c.Id,
               Amount = c.Amount,
               LoanTypeId = c.LoanTypeId,
               LoanTypeName = c.LoanType.Name,
               StartDate = c.StartDate,
               EndDate = c.EndDate,
               UserName = c.User.Name,
               CreateDate = c.CreateDate,
               LastUpdateDate = c.LastUpdateDate,
               ValutaName = _enumRepo.GetItemsByString<Domain.Enumerations.Currency>((int)c.Valuta),
               LoanStatus = c.LoanStatus,
               LoanStatusName = _enumRepo.GetItemsByString<Domain.Enumerations.LoanStatuses>((int)c.LoanStatus),
           });
            }


            if (result == null)
            {
                return null;
            }

            loanSearchResult.TotalRecords = result.Count();
            if (filter.PageNumber > 1)
            {
                int skipRecord = (filter.PageNumber - 1) * filter.PageSize;
                result = result.Skip(skipRecord).Take(filter.PageSize);
            }
            else
            {
                result = result.Take(filter.PageSize == 0 ? 10 : filter.PageSize);
            }

            loanSearchResult.PageNumber = filter.PageNumber;
            loanSearchResult.PageSize = filter.PageSize;

            loanSearchResult.LoanResult = loanSearchResult.TotalRecords == 0 ? new List<LoanSearchDto>() : result.ToList();




            return loanSearchResult;
        }



        public async Task<Loans> RegisterLoan(CreateLoanDto loanForCreate)
        {
            var currentUser = _commonRepo.CurrentUser();
            var currentUserId = _commonRepo.CurrentUserId();
            loanForCreate.UserId = currentUserId;
            loanForCreate.CreateDate = DateTime.Now;
            loanForCreate.LastUpdateDate = DateTime.Now;
            loanForCreate.LastUpdateUserId = currentUserId;
            var addLoan = _mapper.Map<Loans>(loanForCreate);

            await _context.Loans.AddAsync(addLoan);
            await _context.SaveChangesAsync();

            return addLoan;
        }

        public async Task<string> UpdateLoan(int id, LoanDto loanForUpdate)
        {
            var loanFromRepo = await GetLoanById(id);
            var currentUser = _commonRepo.CurrentUser();
            var currentUserRole = _commonRepo.CurrentRole();
            if (currentUserRole == (int)Domain.Enumerations.RoleType.WebUser && 
              (loanFromRepo.LoanStatus == (int)Domain.Enumerations.LoanStatuses.Rejected
                || loanFromRepo.LoanStatus == (int)Domain.Enumerations.LoanStatuses.Approved))
            {
                return "არ გაქვთ ამ სესხთან მუშაობის უფლება";
            }

                loanForUpdate.LastUpdateDate = DateTime.Now;
            loanForUpdate.LastUpdateUserId = _commonRepo.CurrentUserId();
            var updateLoan = _mapper.Map(loanForUpdate, loanFromRepo);

            await _context.SaveChangesAsync();

            return "წარმატებით განახლდა";
        }

    }
}
