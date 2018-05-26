using System;

namespace Odin.UIElements.Popups
{
    public class PopupService
    {
        private static PopupService _instance;
        public event Action<DialogPopup> OnNewPopup;

        private PopupService()
        {
        }

        public void ShowPopup(DialogPopup dialogPopup)
        {
            OnNewPopup?.Invoke(dialogPopup);
            dialogPopup.Show();
        }

        public static PopupService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PopupService();
                }
                return _instance;
            }
        }

    }
}
