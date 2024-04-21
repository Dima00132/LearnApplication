using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnApplication.Extensions
{
    public static class StringExtensions
    {
        public static T ToEnum<T>(this string value)where T:Enum
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch (ArgumentException)
            {

                return default;
            }
            
        }
    }
}
