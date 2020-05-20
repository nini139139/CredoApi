using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enumerations
{
    public class StringValue : Attribute
    {
        private readonly string _value;

        public StringValue(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }

    /// <summary>
    /// თუ წვდომა აკრძალულია, რა მესიჯი გამოვიდეს
    /// </summary>
    public class AccessDeniedText : Attribute
    {
        private readonly string _value;

        public AccessDeniedText(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }

    public static class EnumHelper<T>
    {
        public static T GetValueFromName(string name, out int enumValue)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();

            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(StringValue)) as StringValue;
                if (attribute != null)
                {
                    if (attribute.Value == name)
                    {
                        enumValue = Convert.ToInt32((T)field.GetValue(null));
                        return (T)field.GetValue(null);
                    }
                }
                else
                {
                    if (field.Name == name)
                    {
                        enumValue = Convert.ToInt32((T)field.GetValue(null));
                        return (T)field.GetValue(null);
                    }
                }
            }
            throw new ArgumentOutOfRangeException("name");
        }



        public static string GetAccessDeniedText<T>(T value) where T : struct, IConvertible
        {
            Type t = typeof(T);
            if (!t.IsEnum)
            {
                throw new ArgumentException("T must be an enum type.");
            }

            string output = null;
            var type = value.GetType();
            var fi = type.GetField(value.ToString());
            var a = fi.GetCustomAttributes(typeof(AccessDeniedText), false) as AccessDeniedText[];

            if (a.Length > 0)
            {
                output = a[0].Value;
            }
            return output;
        }



    }
}
