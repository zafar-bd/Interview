using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Domain.Helpers
{
    public class GlobalEnum
    {
        [Flags]
        public enum MyEnum
        {
            NONE = 0,
            A = 1,
            B = 2,
            C = 4,
            D = 8,
            E = 16,
            All = ~NONE
        }
    }
}
