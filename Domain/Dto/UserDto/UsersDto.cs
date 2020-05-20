using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Dto.UserDto
{
   public class UsersDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? DateOfBirthday { get; set; }
        public string LastName { get; set; }
        public string PersonalIdNumber { get; set; }
        public string Name { get; set; }
        public int? RoleId { get; set; }
    }
}
