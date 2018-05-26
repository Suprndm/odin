using System;

namespace Odin.UIElements.Buttons
{
    public interface IButton
    {
        event Action Activated;

        //gérer les states, disabled, reactive,
        //gérer les clicks
        //command pattern
    }
}
