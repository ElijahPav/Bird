using System;

//todo: нужно это в using перемещать
//https://metanit.com/sharp/tutorial/3.25.php
namespace UnityEngine.UI
{
    public class StartButton : Button
    {
        public event Action StartButtonClick;
        //todo: это поле пока стоит оставить приватным, в будущем оно тоже навряд ли будет публичным
        private bool isButtonActive = true;

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