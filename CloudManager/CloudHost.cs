using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudManager
{
    interface CloudHost
    {
        bool CreateDevice();
        bool EditDevice();
        bool DeleteDevice();

    }
}
