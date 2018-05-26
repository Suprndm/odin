﻿using System.Threading.Tasks;
using Odin.Core;

namespace Odin.VisualEffects
{
    public abstract class PoppedElement : OView
    {
        protected readonly int DisplayDuration;
        protected readonly int AppearingDuration;
        protected readonly int DisappearingDuration;

        protected PoppedElement(float x, float y, float height, float width, int displayDuration, int appearingDuration, int disappearingDuration) : base(x, y, height, width)
        {
            DisplayDuration = displayDuration;
            AppearingDuration = appearingDuration;
            DisappearingDuration = disappearingDuration;
        }


        public async Task Pop()
        {
            await Appear();
            await Task.Delay(DisplayDuration);
            await Disappear();
            Dispose();
        }

        protected abstract Task Appear();

        protected abstract Task Disappear();

    }
}
