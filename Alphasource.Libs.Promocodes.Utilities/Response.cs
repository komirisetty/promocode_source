using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaSource.Lib.PromoCode.Helpers
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public dynamic data { get; set; }
        public int Count { get; set; }
    }
}
