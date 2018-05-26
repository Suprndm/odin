using System;

namespace Odin.Logging
{
    public interface ILogger
    {
        event Action<string> OnLogged;
        event Action<string> OnPermanentText1Updated;
        event Action<string> OnPermanentText2Updated;
        event Action<string> OnPermanentText3Updated;

        void Log(string message);

        void UpdatePermanentText1(string message);

        void UpdatePermanentText2(string message);

        void UpdatePermanentText3(string message);
    }
}
