using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositor.Interface
{
    public interface IcommonRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        int CurrentUserId();
        int CurrentRole();
        string CurrentUser();
    }
}
