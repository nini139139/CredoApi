using Microsoft.AspNetCore.Http;
using Repositor.Interface;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repositor.Concrete
{
    public class CommonRepository : IcommonRepository
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommonRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            var result = await _context.SaveChangesAsync() > 0;
            return result;
        }

        public int CurrentRole()
        {
            int currentRole = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(i => i.Type == "UserRole").Value);
            return currentRole;
        }
        public int CurrentUserId()
        {
            int currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return currentUserId;
        }

        public string CurrentUser()
        {
            string currentUserName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            return currentUserName;
        }
    }
}
