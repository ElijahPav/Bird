using System;

namespace UnityEngine.UI
{
    public class StartButton : Button
    {
        public event Action StartButtonClick;
        public bool isButtonActive = true;

        protected override void Start()
        {
            onClick.AddListener(OnClick);
        }

        protected override void OnDestroy()
        {
            onClick.RemoveAllListeners();
        }

        void OnClick()
        {
            if (isButtonActive)
            {
                StartButtonClick?.Invoke();
            }
        }
    }
}