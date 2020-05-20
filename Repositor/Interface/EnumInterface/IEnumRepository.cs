using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enumerations.EnumData;

namespace Repositor.Interface.EnumInterface
{
    public interface IEnumRepository
    {
        Task<IEnumerable<NameValue>> GetItems<T>() where T : struct, IConvertible;
        string GetItemsByString<T>(int enumValue) where T: struct, IConvertible; 
    }
}
