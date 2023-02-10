using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Retomuub.Common
{
    public class AppSettings{
        public string Key { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
    }

}