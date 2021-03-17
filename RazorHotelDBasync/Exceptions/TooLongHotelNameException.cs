using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorHotelDBasync.Exceptions
{
    public class TooLongHotelNameException: Exception
    {
        public TooLongHotelNameException(string message):base(message)
        {
            
        }
    }
}
