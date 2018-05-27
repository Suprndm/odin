using System;
using System.Collections.Generic;
using System.Text;

namespace Odin.Services
{
    public struct OdinSettings
    {
        public OdinSettings(bool logEnabled)
        {
            LogEnabled = logEnabled;
        }

        public bool LogEnabled { get;}
    }
}
