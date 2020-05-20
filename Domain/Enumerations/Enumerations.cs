using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enumerations
{
    public enum Currency
    {
   
        [StringValue("GEL")]
        GEL = 1,
        [StringValue("EUR")]
        EUR = 2,
        [StringValue("USD")]
        USD = 3,
    }

    public enum RoleType
    {
        [StringValue("Admin")]
        Admin = 1,
        [StringValue("WebUser")]
        WebUser = 2
    }
    public enum LoanStatuses
    {

        [StringValue("გაგზავნილია")]
        Sent = 1,

        [StringValue("მუშაობის პროცესში")]
        InWorkingProccess = 3,

        [StringValue("შენახული")]
        Saved = 4,
        [StringValue("დამტკიცებული")]
        Approved = 5,

        [StringValue("უარყოფილი")]
        Rejected = 6,

      

    }


}
