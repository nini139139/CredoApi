using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enumerations.EnumData;

namespace Repositor.Enumerations
{
    public interface IEnumRepo
    {
        Task<IEnumerable<NameValue>> GetItems<T>() where T : struct, IConvertible;

        //public static List<Domain.Model.EntityData.NameValue> GetItems<T>() where T : struct, IConvertible;
    }
}
