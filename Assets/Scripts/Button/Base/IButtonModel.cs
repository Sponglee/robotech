using System;

namespace RedAndBlue.Buttons
{
    public interface IButtonModel : IDisposable
    {
        public ButtonType ButtonType { get; }
        public bool IsActive { get; set; }
        public float ActivationTime { get; }
        public int MoneyReward { get; }
    }
}