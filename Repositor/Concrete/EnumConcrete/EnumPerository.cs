using Domain.Enumerations;
using Repositor.Interface.EnumInterface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enumerations.EnumData;

namespace Repositor.Concrete.EnumConcrete
{
    public class EnumPerository : IEnumRepository
    {
        public async Task<IEnumerable<NameValue>> GetItems<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enum type.");
            }

            var names = Enum.GetNames(typeof(T)).ToArray();
            List<NameValue> list = null;
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                if (list == null) list = new List<NameValue>();
                int intValue = GetValue<T>(value);
                var stringValue = GetStringValue<T>(value);
                list.Add(new NameValue { Id = intValue, Name = stringValue });
            }
            return list;

        }



        public   string  GetItemsByString<T>(int enumValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enum type.");
            }

            var names = Enum.GetNames(typeof(T)).ToArray();
            int intValue = 0;
            string stringValue = "";
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                intValue = GetValue<T>(value);
                stringValue = GetStringValue<T>(value);
                if (intValue == enumValue)
                return stringValue;
            }
            return stringValue;

        }



        public static string GetStringValue<T>(T value) where T : struct, IConvertible
        {
            Type t = typeof(T);
            if (!t.IsEnum)
            {
                throw new ArgumentException("T must be an enum type.");
            }

            string output = null;
            var type = value.GetType();
            var fi = type.GetField(value.ToString());
            var a = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];

            if (a.Length > 0)
            {
                output = a[0].Value;
            }
            return output;
        }


        public static int GetValue<T>(T value) where T : struct, IConvertible
        {
            Type t = typeof(T);
            if (!t.IsEnum)
            {
                throw new ArgumentException("T must be an enum type.");
            }

            return value.ToInt32(CultureInfo.InvariantCulture.NumberFormat);
        }
    }
}
