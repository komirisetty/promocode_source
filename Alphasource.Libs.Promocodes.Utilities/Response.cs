using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alphasource.Libs.Promocodes.Utilities
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public dynamic data { get; set; }
        public int Count { get; set; }
    }
}
