using Domain.Entity.LoanEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity.UserEntity
{
    public partial class Users
    {
        public Users()
        {
            InverseLastUpdateUser = new HashSet<Users>();
            Loans = new HashSet<Loans>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime? DateOfBirthday { get; set; }
        public string LastName { get; set; }
        public string PersonalIdNumber { get; set; }
        public string Name { get; set; }
        public int? RoleId { get; set; }
        public int? LastUpdateUserId { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual Users LastUpdateUser { get; set; }
        public virtual ICollection<Users> InverseLastUpdateUser { get; set; }
        public virtual ICollection<Loans> Loans { get; set; }
    }
}
