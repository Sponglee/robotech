using System;

namespace RedAndBlue.Buttons
{
    [Serializable]
    public class ButtonData
    {
        public ButtonType ButtonType;
        public ButtonViewBase ButtonView;
        public int RewardAmount;
        public float ActiveTime;
    }
}