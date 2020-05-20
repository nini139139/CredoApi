using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dto.Loan;
using Domain.Entity.LoanEntity;
using Domain.Enumerations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositor.Interface;
using Repositor.Interface.EnumInterface;
using Repositor.Interface.LoanIterface;
using Repositor.Interface.LogerInterface;
using static Domain.Enumerations.EnumData;

namespace CredoVacancy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanRepository _loanRepo;
        private readonly IEnumRepository _enumRepo;
        private readonly IMapper _mapper;
        private readonly IcommonRepository _commonRepo;
        private readonly ILoggerManager _logger;

        public LoanController(ILoanRepository loanRepo, IEnumRepository enumRepo, IMapper mapper, IcommonRepository commonRepo, ILoggerManager logger)
        {
            _loanRepo = loanRepo;
            _enumRepo = enumRepo;
            _mapper = mapper;
            _commonRepo = commonRepo;
            _logger = logger;
        }

        [HttpGet("getLoanTypes")]
        public async Task<IActionResult> Get()
        {
            _logger.LogInfo("request contorller:Loan. request func: getLoanTypes ");
      
            var loanTypes = await _loanRepo.GetLoanTypes();

            _logger.LogInfo("responce controller:Loan. responce func getLoanTypes ");
            return Ok(loanTypes);
        }


        [HttpGet("GetCurrencies")]
        public async Task<IActionResult> GetCurrencies()
        {
            _logger.LogInfo("request contorller:Loan. request func: GetCurrencies ");
            var currencies =await _enumRepo.GetItems<Currency>();
            _logger.LogInfo("responce controller:Loan. responce func GetCurrencies ");
            return Ok(currencies);

        }



        [HttpPost("registerLoan")]
        public async Task<IActionResult> RegisterLoan(CreateLoanDto loanForCreate)
        {
            _logger.LogInfo("request contorller:Loan. request func: RegisterLoan ");
            var createdRetrieval = await _loanRepo.RegisterLoan(loanForCreate);

            _logger.LogInfo("responce controller:Loan. responce func RegisterLoan ");
            return Ok(createdRetrieval);

        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLoan(int id, LoanDto loanForUpdate)
        {
            _logger.LogInfo("request contorller:Loan. request func: UpdateLoan ");
            var result =  await _loanRepo.UpdateLoan(id, loanForUpdate);
            _logger.LogInfo("responce controller:Loan. responce func UpdateLoan ");
            return Ok(new { message = result });
        }


        // GET api/values
        [HttpGet("getLoan")]
        public async Task<IActionResult> GetLoan(int pageSize = 10, int pageNumber = 1)
        {
            _logger.LogInfo("request contorller:Loan. request func: GetLoan ");
            LoanFilterDto filterRetrievals = new LoanFilterDto();
            filterRetrievals.PageNumber = pageNumber;
            filterRetrievals.PageSize = pageSize;
            _logger.LogInfo("responce controller:Loan. responce func GetLoan ");
            var loanToReturn = await _loanRepo.GetLoans(filterRetrievals);
            return Ok(loanToReturn);
        }



        [HttpGet("getloan/{id}")]
        public async Task<IActionResult> GetLoan(int id)
        {
            _logger.LogInfo("request contorller:Loan. request func: GetLoan by id ");
            var loan = await _loanRepo.GetLoanById(id);
            _logger.LogInfo("responce controller:Loan. responce func GetLoan by id");
            return Ok(loan);
        }



        [HttpPost("PostSearchRetrievals")]
        public async Task<IActionResult> PostSearchRetrievals(LoanFilterDto loans)
        {
            var loan = await _loanRepo.GetLoans(loans);
            return Ok(loan);
        }


    }
}
