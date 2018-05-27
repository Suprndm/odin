using System;

namespace Odin.Services
{
    public class Logger
    {
        private readonly bool _logEnabled;

        public Logger(bool logEnabled)
        {
            _logEnabled = logEnabled;
        }

        public event Action<string> OnLogged;
        public event Action<string> OnPermanentText1Updated;
        public event Action<string> OnPermanentText2Updated;
        public event Action<string> OnPermanentText3Updated;

        public void Log(string message)
        {
            if (_logEnabled)
                OnLogged?.Invoke(message);
        }

        public void UpdatePermanentText1(string message)
        {
            if (_logEnabled)
                OnPermanentText1Updated?.Invoke(message);
        }

        public void UpdatePermanentText2(string message)
        {
            if (_logEnabled)
                OnPermanentText2Updated?.Invoke(message);
        }

        public void UpdatePermanentText3(string message)
        {
            if (_logEnabled)
                OnPermanentText3Updated?.Invoke(message);
        }


    }
}
