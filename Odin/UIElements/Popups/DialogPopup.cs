﻿using System;
using System.Threading.Tasks;
using Odin.Core;

namespace Odin.UIElements.Popups
{
    public class DialogPopup : OView
    {
        protected Popup Popup;
        protected PopupBackground Background;
        protected bool CanEscape;


        protected int DelayBeforeCommandExecution;

        public Action BackCommand { get; set; }
        public Action NextCommand { get; set; }
        public float ContentWidth { get; set; }
        public float ContentHeight { get; set; }

        public DialogPopup(float contentWidth, float contentHeight, bool canEscape = false)
        {
            ContentWidth = contentWidth;
            ContentHeight = contentHeight;
            Background = new PopupBackground(Width, Height);

            Popup = new Popup(contentWidth, contentHeight);

            CanEscape = canEscape;
        }

        private async void Background_Activated()
        {
            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(DelayBeforeCommandExecution);
                BackCommand?.Invoke();
            });
            Popup.HideLeft();
            await Background.Hide();
            Dispose();
        }

        private async void Popup_BackAction()
        {
            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(DelayBeforeCommandExecution);
                BackCommand?.Invoke();
            });
            await Background.Hide();
            Dispose();
        }

        private async void Popup_NextAction()
        {
            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(DelayBeforeCommandExecution);
                NextCommand?.Invoke();
            });
            await Background.Hide();
            Dispose();
        }

        public override void Dispose()
        {
            Popup.NextAction -= Popup_NextAction;
            Popup.BackAction -= Popup_BackAction;

            if (CanEscape)
            {
                Background.Activated -= Background_Activated;
            }

            base.Dispose();
        }


        public async Task Show()
        {
            AddChild(Background);
            AddChild(Popup);

            Background.Show();
            Popup.Show();

            await Task.Delay(700);

            Popup.NextAction += Popup_NextAction;
            Popup.BackAction += Popup_BackAction;

            if (CanEscape)
            {
                Background.Activated += Background_Activated;
            }
        }
    }
}
