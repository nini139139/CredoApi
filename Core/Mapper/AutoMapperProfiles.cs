using AutoMapper;
using Domain.Dto.Loan;
using Domain.Entity.LoanEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            #region loans
            CreateMap<Loans, CreateLoanDto>();
            CreateMap<CreateLoanDto, Loans>();

            CreateMap<Loans, LoanDto>();
            CreateMap<LoanDto, Loans>()
                       .ForMember(dest => dest.CreateDate, opt =>
                       {
                           opt.Ignore();
                       })
                .ForMember(dest => dest.Id, opt =>
                {
                    opt.Ignore();
                });
        }



        #endregion
    }
}
